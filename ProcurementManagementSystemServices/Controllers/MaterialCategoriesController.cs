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
    public class MaterialCategoriesController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public MaterialCategoriesController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/MaterialCategories
        [HttpGet]
        public ActionResult GetMaterialCategories()
        {
            List<MaterialCategory> list = this.context.MaterialCategories.ToList();
            List<MaterialCategoryDTO> listDTO = new List<MaterialCategoryDTO>();
            foreach (MaterialCategory matcat in list)
            {
                MaterialCategoryDTO dto = this.mapper.Map<MaterialCategoryDTO>(matcat);
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }
    }
}
