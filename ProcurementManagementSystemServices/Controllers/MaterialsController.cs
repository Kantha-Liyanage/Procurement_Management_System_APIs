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
    public class MaterialsController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public MaterialsController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Materials
        [HttpGet("GetMaterialsOfCategory/{categoryId}")]
        public ActionResult GetMaterialsOfCategory(int categoryId)
        {
            List<Material> list = this.context.Materials.Where(material => material.CategoryId == categoryId).ToList<Material>();
            List<MaterialDTO> listDTO = new List<MaterialDTO>();
            foreach (Material mat in list)
            {
                MaterialDTO dto = this.mapper.Map<MaterialDTO>(mat);
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        // GET: api/Materials
        [HttpGet("SearchMaterialsByName/{name}")]
        public ActionResult SearchMaterialsByName(string name)
        {
            List<Material> list = this.context.Materials.Where(material => material.Name.Contains(name)).ToList<Material>();
            List<MaterialDTO> listDTO = new List<MaterialDTO>();
            foreach (Material mat in list)
            {
                MaterialDTO dto = this.mapper.Map<MaterialDTO>(mat);
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public ActionResult GetMaterial(int id)
        {
            var material = this.context.Materials.Find(id);

            if (material == null)
            {
                return NotFound();
            }

            MaterialDTO dto = this.mapper.Map<MaterialDTO>(material);

            return Ok(dto);
        }
    }
}
