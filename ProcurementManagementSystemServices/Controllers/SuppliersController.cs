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
    public class SuppliersController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public SuppliersController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public ActionResult GetSupplier(int id)
        {
            var supplier = this.context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            SupplierDTO dto = this.mapper.Map<SupplierDTO>(supplier);

            return Ok(dto);
        }
    }
}
