using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;

namespace ProcurementManagmentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseRequisitionStatusController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public PurchaseRequisitionStatusController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/PurchaseRequisitionStatus/5
        [HttpGet("{id}")]
        public ActionResult GetPurchaseRequisitionStatus(int id)
        {
            var purchaseRequisitionStatus = this.context.PurchaseRequisitionStatuses.Find(id);

            if (purchaseRequisitionStatus == null)
            {
                return NotFound();
            }

            PurchaseRequisitionStatusDTO dto = this.mapper.Map<PurchaseRequisitionStatusDTO>(purchaseRequisitionStatus);

            return Ok(dto);
        }
    }
}
