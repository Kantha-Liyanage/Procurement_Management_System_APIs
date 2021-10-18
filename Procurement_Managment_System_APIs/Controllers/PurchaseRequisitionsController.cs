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
    public class PurchaseRequisitionsController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public PurchaseRequisitionsController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseRequisitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRequisition>>> GetPurchaseRequisitions()
        {
            return await _context.PurchaseRequisitions.ToListAsync();
        }

        // GET: api/PurchaseRequisitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequisition>> GetPurchaseRequisition(int id)
        {
            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null)
            {
                return NotFound();
            }

            return purchaseRequisition;
        }

        // PUT: api/PurchaseRequisitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRequisition(int id, PurchaseRequisition purchaseRequisition)
        {
            if (id != purchaseRequisition.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRequisition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseRequisitionExists(id))
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

        // POST: api/PurchaseRequisitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseRequisition>> PostPurchaseRequisition(PurchaseRequisition purchaseRequisition)
        {
            _context.PurchaseRequisitions.Add(purchaseRequisition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseRequisition", new { id = purchaseRequisition.Id }, purchaseRequisition);
        }

        // DELETE: api/PurchaseRequisitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseRequisition(int id)
        {
            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);
            if (purchaseRequisition == null)
            {
                return NotFound();
            }

            _context.PurchaseRequisitions.Remove(purchaseRequisition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseRequisitionExists(int id)
        {
            return _context.PurchaseRequisitions.Any(e => e.Id == id);
        }
    }
}
