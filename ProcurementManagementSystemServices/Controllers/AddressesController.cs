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
    public class AddressesController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;
        public AddressesController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public ActionResult GetAddress(int id)
        {
            var address = this.context.Addresses.Find(id);

            if (address == null)
            {
                return NotFound();
            }

            AddressDTO dto = this.mapper.Map<AddressDTO>(address);

            return Ok(dto);
        }
    }
}
