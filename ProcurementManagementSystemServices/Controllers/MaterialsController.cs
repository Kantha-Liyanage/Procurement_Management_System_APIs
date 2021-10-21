using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        [HttpGet("SearchMaterialsByCategoryAndName")]
        public ActionResult SearchMaterialsByCategoryAndName()
        {
            int categoryId = 0;
            if (Request.Query.ContainsKey("category")) {
                StringValues categoryPara = "0";
                Request.Query.TryGetValue("category", out categoryPara);
                categoryId = int.Parse(categoryPara);
            }

            string name = "";
            if (Request.Query.ContainsKey("name"))
            {
                StringValues namePara = "0";
                Request.Query.TryGetValue("name", out namePara);
                name = namePara;
            }

            List<Material> list = this.context.Materials.Where(material => ( material.CategoryId == categoryId && material.Name.Contains(name) ) ).ToList<Material>();
            List<MaterialDTO> listDTO = new List<MaterialDTO>();
            foreach (Material mat in list)
            {
                MaterialDTO dto = this.mapper.Map<MaterialDTO>(mat);

                MaterialCategory cat = this.context.MaterialCategories.Find(mat.CategoryId);
                dto.CategoryName = cat.Name;

                Supplier sup = this.context.Suppliers.Find(mat.SupplierId);
                dto.SupplierName = sup.Name;

                UnitOfMeasure uom = this.context.UnitOfMeasures.Find(mat.UnitOfMeasureId);
                dto.UnitOfMeasureName = uom.Name;

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

            MaterialCategory cat = this.context.MaterialCategories.Find(material.CategoryId);
            dto.CategoryName = cat.Name;

            Supplier sup = this.context.Suppliers.Find(material.SupplierId);
            dto.SupplierName = sup.Name;

            UnitOfMeasure uom = this.context.UnitOfMeasures.Find(material.UnitOfMeasureId);
            dto.UnitOfMeasureName = uom.Name;

            return Ok(dto);
        }
    }
}
