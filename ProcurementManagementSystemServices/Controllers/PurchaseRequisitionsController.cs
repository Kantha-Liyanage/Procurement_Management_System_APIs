using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcurementManagementSystemServices.Business;
using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;

namespace ProcurementManagmentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseRequisitionsController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public PurchaseRequisitionsController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult PostPurchaseRequisition(PurchaseRequisitionDTO purchaseRequisitionDTO)
        {
            PurchaseRequisition purchaseRequisition = this.mapper.Map<PurchaseRequisition>(purchaseRequisitionDTO);
            //server side values
            purchaseRequisition.CreatedBy = User.Identity.Name;
            purchaseRequisition.CreatedDate = System.DateTime.Today;
            purchaseRequisition.IsOpen = 1;

            foreach (PurchaseRequisitionItemDTO item in purchaseRequisitionDTO.items)
            {
                PurchaseRequisitionItem purchaseRequisitionItem = this.mapper.Map<PurchaseRequisitionItem>(item);
                //server side values
                purchaseRequisitionItem.CreatedDate = System.DateTime.Today;
                purchaseRequisition.PurchaseRequisitionItems.Add(purchaseRequisitionItem);
                purchaseRequisitionItem.ApprovedQuantity = 0;
                purchaseRequisitionItem.Status = "Pending";
                item.Status = "Pending";

                //Pending, Returned to Originator, Declined, Referred, Partially Approved, Approved, Placed
            }

            this.context.PurchaseRequisitions.Add(purchaseRequisition);
            this.context.SaveChanges();

            purchaseRequisitionDTO.Id = purchaseRequisition.Id;
            purchaseRequisitionDTO.OverallStatus = "Open";

            return Ok(purchaseRequisitionDTO);
        }

        [HttpGet("GetOpenPurchaseRequisitions")]
        public ActionResult GetOpenPurchaseRequisitions()
        {
            //Me
            string loggedOnUser = User.Identity.Name;

            //My sites
            var innerJoinQuery =
                from pr in this.context.PurchaseRequisitions
                where pr.IsOpen == 1
                join userSites in this.context.UserSites.Where(userSite => userSite.Username == loggedOnUser) on pr.SiteId equals userSites.Site
                select new { pr.Id, pr.SiteId, pr.CreatedBy, pr.CreatedDate };

            //Result
            List<PurchaseRequisitionDTO> listDTO = new List<PurchaseRequisitionDTO>();
            foreach (var row in innerJoinQuery)
            {
                PurchaseRequisitionDTO dto = new PurchaseRequisitionDTO();
                dto.Id = row.Id;
                dto.SiteId = row.SiteId;
                dto.CreatedBy = row.CreatedBy;
                dto.CreatedDate = row.CreatedDate;
                listDTO.Add(dto);
            }


            //Site name and Total value
            listDTO.ForEach(dto =>
            {
                Site site = this.context.Sites.Find(dto.SiteId);
                dto.SiteName = site.Name;
                List <PurchaseRequisitionItem> items = this.context.PurchaseRequisitionItems.Where(item => item.PurchaseRequisitionId == dto.Id).ToList<PurchaseRequisitionItem>();
                items.ForEach(pri =>
                {
                    //Get material
                    Material material = this.context.Materials.Find(pri.MaterialId);
                    dto.TotalValue += (double)(pri.RequiredQuantity * material.UnitPrice);
                });
            });

            return Ok(listDTO);
        }

        [HttpGet("{id}")]
        public ActionResult GetPurchaseRequisition(int id)
        {
            string loggedOnUser = User.Identity.Name;
            PurchaseRequisition header = this.context.PurchaseRequisitions.Find(id);
            if (header == null)
            {
                return NotFound();
            }
            List<PurchaseRequisitionItem> items = this.context.PurchaseRequisitionItems.Where(item => item.PurchaseRequisitionId == header.Id).ToList<PurchaseRequisitionItem>();

            PurchaseRequisitionDTO headerDTO = this.mapper.Map<PurchaseRequisitionDTO>(header);
            foreach (PurchaseRequisitionItem item in items)
            {
                PurchaseRequisitionItemDTO dto = this.mapper.Map<PurchaseRequisitionItemDTO>(item);
                Material material = this.context.Materials.Find(item.MaterialId);
                MaterialCategory category = this.context.MaterialCategories.Find(material.CategoryId);
                UnitOfMeasure uom = this.context.UnitOfMeasures.Find(material.UnitOfMeasureId);
                Supplier supplier = this.context.Suppliers.Find(material.SupplierId);

                dto.MaterialName = material.Name;
                dto.MaterialCategory = category.Name;
                dto.Uom = uom.Name;
                dto.PriceUnit = material.PriceUnit;
                dto.UnitPrice = material.UnitPrice;
                dto.SubTotal = dto.RequiredQuantity * dto.UnitPrice;
                dto.SupplierName = supplier.Name;
                dto.LeadTimeDays = (double)material.LeadTimeDays;
                headerDTO.items.Add(dto);
            }

            return Ok(headerDTO);
        }

        [HttpPut("Approve")]
        public ActionResult ApprovePurchaseRequisition(PurchaseRequisitionDTO purchaseRequisitionDTO)
        {
            if (!PurchaseRequisitionExists(purchaseRequisitionDTO.Id))
            {
                return NotFound();
            }

            string loggedOnUser = User.Identity.Name;

            //Chain-Of-Responsibility
            CostApprover costApprover = new CostApprover(this.context, loggedOnUser);
            if (!costApprover.canApprove(purchaseRequisitionDTO))
            {
                return Unauthorized(new ApprovalResult("Deficit of budget"));
            }

            //Close PR
            PurchaseRequisition header = this.context.PurchaseRequisitions.Find(purchaseRequisitionDTO.Id);
            header.IsOpen = 0;
            this.context.Entry(header).Property(prop => prop.IsOpen).IsModified = true;

            //Update items.
            List<PurchaseRequisitionItem> items = this.context.PurchaseRequisitionItems.Where(x => x.PurchaseRequisitionId == purchaseRequisitionDTO.Id).ToList<PurchaseRequisitionItem>();
            foreach (PurchaseRequisitionItem item in items)
            {
                item.Status = "Approved";
                item.ApprovedBy = loggedOnUser;
                item.ApprovedDate = System.DateTime.Today;
                item.ApprovedQuantity = purchaseRequisitionDTO.items.Where(x => x.ItemId == item.ItemId).First<PurchaseRequisitionItemDTO>().ApprovedQuantity;

                this.context.Entry(item).Property(prop => prop.Status).IsModified = true;
                this.context.Entry(item).Property(prop => prop.ApprovedBy).IsModified = true;
                this.context.Entry(item).Property(prop => prop.ApprovedDate).IsModified = true;
                this.context.Entry(item).Property(prop => prop.ApprovedQuantity).IsModified = true;
            }

            this.context.SaveChanges();
            return Ok(new ApprovalResult("Approved"));
        }

        private bool PurchaseRequisitionExists(int id)
        {
            return this.context.PurchaseRequisitions.Any(e => e.Id == id);
        }
    }
}
