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
    public class CountriesController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public CountriesController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public ActionResult GetCountry(string id)
        {
            var country = this.context.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }

            CountryDTO dto = this.mapper.Map<CountryDTO>(country);

            return Ok(dto);
        }
    }
}
