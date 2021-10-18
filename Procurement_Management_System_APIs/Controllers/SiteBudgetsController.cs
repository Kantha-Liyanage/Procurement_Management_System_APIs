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
    public class SiteBudgetsController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public SiteBudgetsController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/SiteBudgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteBudget>>> GetSiteBudgets()
        {
            return await _context.SiteBudgets.ToListAsync();
        }

        // GET: api/SiteBudgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteBudget>> GetSiteBudget(int id)
        {
            var siteBudget = await _context.SiteBudgets.FindAsync(id);

            if (siteBudget == null)
            {
                return NotFound();
            }

            return siteBudget;
        }

        // PUT: api/SiteBudgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiteBudget(int id, SiteBudget siteBudget)
        {
            if (id != siteBudget.Id)
            {
                return BadRequest();
            }

            _context.Entry(siteBudget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteBudgetExists(id))
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

        // POST: api/SiteBudgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SiteBudget>> PostSiteBudget(SiteBudget siteBudget)
        {
            _context.SiteBudgets.Add(siteBudget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSiteBudget", new { id = siteBudget.Id }, siteBudget);
        }

        // DELETE: api/SiteBudgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiteBudget(int id)
        {
            var siteBudget = await _context.SiteBudgets.FindAsync(id);
            if (siteBudget == null)
            {
                return NotFound();
            }

            _context.SiteBudgets.Remove(siteBudget);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteBudgetExists(int id)
        {
            return _context.SiteBudgets.Any(e => e.Id == id);
        }
    }
}
