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

        // GET: api/approvedtypes/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<ApprovedType>>> GetApprovedTypes()
        {   //Get everything in approvedTypes
            return await _context.ApprovedTypes.ToListAsync();
        }

        // GET: api/approvedtypes/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<ApprovedType>> GetApprovedType(int id)
        {   //Get everything from approvedtypes at the id
            var approvedType = await _context.ApprovedTypes.FindAsync(id);

            if (approvedType == null)
            {
                return NotFound();
            }

            return approvedType;
        }

        // GET: api/approvedtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovedTypeDto>>> GetApprovedTypeDtos()
        {   //Get the all DTO of approved types
            var query = _context.ApprovedTypes.Select(at => new ApprovedTypeDto()
            {
                id = at.ApprovedTypeID,
                info = at.AType
            });

            var approvedTypes = await query.ToListAsync();

            return approvedTypes;
        }

        // GET: api/approvedtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApprovedTypeDto>> GetApprovedTypeDtoID(int id)
        {   //Get the DTO of approved types at the id
            var approvedType = await _context.ApprovedTypes.FindAsync(id);

            if (approvedType == null)
            {
                return NotFound();
            }

            return new ApprovedTypeDto() { id = approvedType.ApprovedTypeID, info = approvedType.AType };
        }

        // PUT: api/approvedtypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApprovedType(int id, ApprovedType approvedType)
        {   //Update approved type based on the id and object provided
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
        public async Task<ActionResult<ApprovedType>> PostApprovedType(ApprovedTypeDto approvedType)
        {   //Create a new approved type based on the information provided

            ApprovedType newAT = new ApprovedType();
            newAT.AType = approvedType.info;
            _context.ApprovedTypes.Add(newAT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApprovedType", new { id = newAT.ApprovedTypeID }, newAT);
        }

        // DELETE: api/approvedtypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApprovedType>> DeleteApprovedType(int id)
        {   //delete an approved type based on the ID
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
