using System.Collections.Generic;
using System.Linq;
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
    public class DepotsController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public DepotsController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Depots
        [HttpGet("GetDeportsOf/{supplierId}")]
        public ActionResult GetDepots(int supplierId)
        {
            List<Depot> list = this.context.Depots.Where(deport => deport.SupplierId == supplierId).ToList<Depot>();
            List<DepotDTO> listDTO = new List<DepotDTO>();
            foreach (Depot depot in list) {
                DepotDTO dto = this.mapper.Map<DepotDTO>(depot);
                listDTO.Add(dto);
            }

            return Ok(listDTO); 
        }
    }
}
