using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planter_API_2.Models;

namespace Planter_API_2.Controllers
{
    [Route("api/approvedtypes")]
    [ApiController]
    public class ApprovedTypesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public ApprovedTypesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/approvedtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovedType>>> GetApprovedTypes()
        {
            return await _context.ApprovedTypes.ToListAsync();
        }

        // GET: api/approvedtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApprovedType>> GetApprovedType(int id)
        {
            var approvedType = await _context.ApprovedTypes.FindAsync(id);

            if (approvedType == null)
            {
                return NotFound();
            }

            return approvedType;
        }

        // PUT: api/approvedtypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApprovedType(int id, ApprovedType approvedType)
        {
            if (id != approvedType.ApprovedTypeID)
            {
                return BadRequest();
            }

            _context.Entry(approvedType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovedTypeExists(id))
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

        // POST: api/approvedtypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ApprovedType>> PostApprovedType(ApprovedType approvedType)
        {
            _context.ApprovedTypes.Add(approvedType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApprovedType", new { id = approvedType.ApprovedTypeID }, approvedType);
        }

        // DELETE: api/approvedtypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApprovedType>> DeleteApprovedType(int id)
        {
            var approvedType = await _context.ApprovedTypes.FindAsync(id);
            if (approvedType == null)
            {
                return NotFound();
            }

            _context.ApprovedTypes.Remove(approvedType);
            await _context.SaveChangesAsync();

            return approvedType;
        }

        private bool ApprovedTypeExists(int id)
        {
            return _context.ApprovedTypes.Any(e => e.ApprovedTypeID == id);
        }
    }
}
