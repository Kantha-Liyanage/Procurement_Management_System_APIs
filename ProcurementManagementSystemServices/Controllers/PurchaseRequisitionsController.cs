using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetPurchaseRequisitions")]
        public ActionResult GetPurchaseRequisitionsOf()
        {
            string loggedOnUser = User.Identity.Name;
            List<PurchaseRequisition> list = this.context.PurchaseRequisitions.Where(pr => pr.CreatedBy == loggedOnUser).ToList<PurchaseRequisition>();
            List<PurchaseRequisitionDTO> listDTO = new List<PurchaseRequisitionDTO>();
            foreach (PurchaseRequisition pr in list)
            {
                PurchaseRequisitionDTO dto = this.mapper.Map<PurchaseRequisitionDTO>(pr);
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        [HttpGet("GetOpenPurchaseRequisitions")]
        public ActionResult GetOpenPurchaseRequisitionsOf()
        {
            string loggedOnUser = User.Identity.Name;
            List<PurchaseRequisition> list = this.context.PurchaseRequisitions.Where(pr => (pr.CreatedBy == loggedOnUser && pr.IsOpen == 1)).ToList<PurchaseRequisition>();
            List<PurchaseRequisitionDTO> listDTO = new List<PurchaseRequisitionDTO>();
            foreach (PurchaseRequisition pr in list)
            {
                PurchaseRequisitionDTO dto = this.mapper.Map<PurchaseRequisitionDTO>(pr);
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        [HttpGet("{id}")]
        public ActionResult GetPurchaseRequisition(int id)
        {
            string loggedOnUser = User.Identity.Name;
            PurchaseRequisition header = this.context.PurchaseRequisitions.Where( (pr => pr.Id == id && pr.CreatedBy == loggedOnUser) ).First();
            if (header == null)
            {
                return NotFound();
            }
            List<PurchaseRequisitionItem> items = this.context.PurchaseRequisitionItems.Where( item => item.PurchaseRequisitionId == header.Id).ToList<PurchaseRequisitionItem>();

            PurchaseRequisitionDTO headerDTO = this.mapper.Map<PurchaseRequisitionDTO>(header);
            foreach (PurchaseRequisitionItem item in items) {
                PurchaseRequisitionItemDTO dto = this.mapper.Map<PurchaseRequisitionItemDTO>(item);
                headerDTO.items.Add(dto);
            }

            return Ok(headerDTO);
        }

        [HttpPost]
        public ActionResult PostPurchaseRequisition(PurchaseRequisitionDTO purchaseRequisitionDTO)
        {
            PurchaseRequisition purchaseRequisition = this.mapper.Map<PurchaseRequisition>(purchaseRequisitionDTO);
            foreach (PurchaseRequisitionItemDTO item in purchaseRequisitionDTO.items) {
                PurchaseRequisitionItem purchaseRequisitionItem = this.mapper.Map<PurchaseRequisitionItem>(item);
                purchaseRequisition.PurchaseRequisitionItems.Add(purchaseRequisitionItem);
            }

            //Header
            this.context.PurchaseRequisitions.Add(purchaseRequisition);
            if (this.context.SaveChanges() > 0)
            {
                //Items
                foreach (PurchaseRequisitionItem item in purchaseRequisition.PurchaseRequisitionItems) {
                    this.context.PurchaseRequisitionItems.Add(item);
                }
                this.context.SaveChanges();
            }

            return CreatedAtAction("GetPurchaseRequisition", new { id = purchaseRequisition.Id }, purchaseRequisition);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePurchaseRequisition(int id)
        {
            string loggedOnUser = User.Identity.Name;
            PurchaseRequisition purchaseRequisition = this.context.PurchaseRequisitions.Where( pr => pr.Id == id && pr.CreatedBy == loggedOnUser).First();
            if (purchaseRequisition == null)
            {
                return NotFound();
            }

            this.context.PurchaseRequisitions.Remove(purchaseRequisition);
            this.context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult PutPurchaseRequisition(int id, PurchaseRequisitionDTO purchaseRequisitionDTO)
        {
            if (id != purchaseRequisitionDTO.Id)
            {
                return BadRequest();
            }

            if (!PurchaseRequisitionExists(id))
            {
                return NotFound();
            }

            PurchaseRequisition purchaseRequisition = this.mapper.Map<PurchaseRequisition>(purchaseRequisitionDTO);
            foreach (PurchaseRequisitionItemDTO item in purchaseRequisitionDTO.items)
            {
                PurchaseRequisitionItem purchaseRequisitionItem = this.mapper.Map<PurchaseRequisitionItem>(item);
                purchaseRequisition.PurchaseRequisitionItems.Add(purchaseRequisitionItem);
            }

            //Header
            this.context.Entry(purchaseRequisition).State = EntityState.Modified;
            //Items
            foreach (PurchaseRequisitionItem item in purchaseRequisition.PurchaseRequisitionItems) {
                this.context.Entry(item).State = EntityState.Modified;
            }

            this.context.SaveChanges();
            return NoContent();
        }

        private bool PurchaseRequisitionExists(int id)
        {
            return this.context.PurchaseRequisitions.Any(e => e.Id == id);
        }
    }
}
