using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcurementManagmentSystemAPIs.Models;

namespace ProcurementManagmentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialCategoriesController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public MaterialCategoriesController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/MaterialCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialCategory>>> GetMaterialCategories()
        {
            return await _context.MaterialCategories.ToListAsync();
        }

        // GET: api/MaterialCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialCategory>> GetMaterialCategory(int id)
        {
            var materialCategory = await _context.MaterialCategories.FindAsync(id);

            if (materialCategory == null)
            {
                return NotFound();
            }

            return materialCategory;
        }

        // PUT: api/MaterialCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialCategory(int id, MaterialCategory materialCategory)
        {
            if (id != materialCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(materialCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MaterialCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MaterialCategory>> PostMaterialCategory(MaterialCategory materialCategory)
        {
            _context.MaterialCategories.Add(materialCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialCategory", new { id = materialCategory.Id }, materialCategory);
        }

        // DELETE: api/MaterialCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialCategory(int id)
        {
            var materialCategory = await _context.MaterialCategories.FindAsync(id);
            if (materialCategory == null)
            {
                return NotFound();
            }

            _context.MaterialCategories.Remove(materialCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialCategoryExists(int id)
        {
            return _context.MaterialCategories.Any(e => e.Id == id);
        }
    }
}
