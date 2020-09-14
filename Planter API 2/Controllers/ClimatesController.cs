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
    [Route("api/climates")]
    [ApiController]
    public class ClimatesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public ClimatesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/climates/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Climates>>> GetClimates()
        {
            return await _context.Climates.ToListAsync();
        }

        // GET: api/climates/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Climates>> GetClimates(int id)
        {
            var climates = await _context.Climates.FindAsync(id);

            if (climates == null)
            {
                return NotFound();
            }

            return climates;
        }

        // GET: api/climates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClimatesDto>>> GetClimatesDto()
        {
            var query = _context.Climates.Select(c => new ClimatesDto()
            {
                id = c.ClimateID,
                info = c.Climate
            });

            var climates = await query.ToListAsync();

            return climates;
        }

        // GET: api/climates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClimatesDto>> GetClimatesDtoId(int id)
        {
            var climates = await _context.Climates.FindAsync(id);

            if (climates == null)
            {
                return NotFound();
            }

            return new ClimatesDto() { id = climates.ClimateID, info = climates.Climate };
        }


        // PUT: api/climates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClimates(int id, Climates climates)
        {
            if (id != climates.ClimateID)
            {
                return BadRequest();
            }

            _context.Entry(climates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClimatesExists(id))
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

        // POST: api/climates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Climates>> PostClimates(Climates climates)
        {
            _context.Climates.Add(climates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClimates", new { id = climates.ClimateID }, climates);
        }

        // DELETE: api/climates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Climates>> DeleteClimates(int id)
        {
            var climates = await _context.Climates.FindAsync(id);
            if (climates == null)
            {
                return NotFound();
            }

            _context.Climates.Remove(climates);
            await _context.SaveChangesAsync();

            return climates;
        }

        private bool ClimatesExists(int id)
        {
            return _context.Climates.Any(e => e.ClimateID == id);
        }
    }
}
