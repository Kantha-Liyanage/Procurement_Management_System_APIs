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
    public class UnitOfMeasuresController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public UnitOfMeasuresController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/UnitOfMeasures/5
        [HttpGet("{id}")]
        public ActionResult GetUnitOfMeasure(int id)
        {
            var unitOfMeasure = this.context.UnitOfMeasures.Find(id);

            if (unitOfMeasure == null)
            {
                return NotFound();
            }

            UnitOfMeasureDTO dto = this.mapper.Map<UnitOfMeasureDTO>(unitOfMeasure);

            return Ok(dto);
        }
    }
}
