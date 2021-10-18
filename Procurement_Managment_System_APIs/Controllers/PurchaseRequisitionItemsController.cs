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
    public class PurchaseRequisitionItemsController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public PurchaseRequisitionItemsController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseRequisitionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRequisitionItem>>> GetPurchaseRequisitionItems()
        {
            return await _context.PurchaseRequisitionItems.ToListAsync();
        }

        // GET: api/PurchaseRequisitionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequisitionItem>> GetPurchaseRequisitionItem(int id)
        {
            var purchaseRequisitionItem = await _context.PurchaseRequisitionItems.FindAsync(id);

            if (purchaseRequisitionItem == null)
            {
                return NotFound();
            }

            return purchaseRequisitionItem;
        }

        // PUT: api/PurchaseRequisitionItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRequisitionItem(int id, PurchaseRequisitionItem purchaseRequisitionItem)
        {
            if (id != purchaseRequisitionItem.PurchaseRequisitionId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRequisitionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseRequisitionItemExists(id))
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

        // POST: api/PurchaseRequisitionItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseRequisitionItem>> PostPurchaseRequisitionItem(PurchaseRequisitionItem purchaseRequisitionItem)
        {
            _context.PurchaseRequisitionItems.Add(purchaseRequisitionItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseRequisitionItemExists(purchaseRequisitionItem.PurchaseRequisitionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseRequisitionItem", new { id = purchaseRequisitionItem.PurchaseRequisitionId }, purchaseRequisitionItem);
        }

        // DELETE: api/PurchaseRequisitionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseRequisitionItem(int id)
        {
            var purchaseRequisitionItem = await _context.PurchaseRequisitionItems.FindAsync(id);
            if (purchaseRequisitionItem == null)
            {
                return NotFound();
            }

            _context.PurchaseRequisitionItems.Remove(purchaseRequisitionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseRequisitionItemExists(int id)
        {
            return _context.PurchaseRequisitionItems.Any(e => e.PurchaseRequisitionId == id);
        }
    }
}
