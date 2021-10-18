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
    public class PurchaseRequisitionStatusController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public PurchaseRequisitionStatusController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseRequisitionStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRequisitionStatus>>> GetPurchaseRequisitionStatuses()
        {
            return await _context.PurchaseRequisitionStatuses.ToListAsync();
        }

        // GET: api/PurchaseRequisitionStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequisitionStatus>> GetPurchaseRequisitionStatus(int id)
        {
            var purchaseRequisitionStatus = await _context.PurchaseRequisitionStatuses.FindAsync(id);

            if (purchaseRequisitionStatus == null)
            {
                return NotFound();
            }

            return purchaseRequisitionStatus;
        }

        // PUT: api/PurchaseRequisitionStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRequisitionStatus(int id, PurchaseRequisitionStatus purchaseRequisitionStatus)
        {
            if (id != purchaseRequisitionStatus.Status)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRequisitionStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseRequisitionStatusExists(id))
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

        // POST: api/PurchaseRequisitionStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseRequisitionStatus>> PostPurchaseRequisitionStatus(PurchaseRequisitionStatus purchaseRequisitionStatus)
        {
            _context.PurchaseRequisitionStatuses.Add(purchaseRequisitionStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseRequisitionStatusExists(purchaseRequisitionStatus.Status))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseRequisitionStatus", new { id = purchaseRequisitionStatus.Status }, purchaseRequisitionStatus);
        }

        // DELETE: api/PurchaseRequisitionStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseRequisitionStatus(int id)
        {
            var purchaseRequisitionStatus = await _context.PurchaseRequisitionStatuses.FindAsync(id);
            if (purchaseRequisitionStatus == null)
            {
                return NotFound();
            }

            _context.PurchaseRequisitionStatuses.Remove(purchaseRequisitionStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseRequisitionStatusExists(int id)
        {
            return _context.PurchaseRequisitionStatuses.Any(e => e.Status == id);
        }
    }
}
