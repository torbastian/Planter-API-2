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
    [Route("api/edible")]
    [ApiController]
    public class EdiblesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public EdiblesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/edible
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Edible>>> GetEdibles()
        {
            return await _context.Edibles.ToListAsync();
        }

        // GET: api/edible/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Edible>> GetEdible(int id)
        {
            var edible = await _context.Edibles.FindAsync(id);

            if (edible == null)
            {
                return NotFound();
            }

            return edible;
        }

        // GET: api/edible
        [HttpGet]
        public IQueryable<EdibleDto> GetEdibleDto()
        {
            IQueryable<EdibleDto> edibles = _context.Edibles.Select(e => new EdibleDto()
            {
                id = e.EdibleID,
                info = e.EdibleS
            });

            return edibles;
        }

        // GET: api/edible/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EdibleDto>> GetEdibleDtoId(int id)
        {
            var edible = await _context.Edibles.FindAsync(id);

            if (edible == null)
            {
                return NotFound();
            }

            return new EdibleDto() { id = edible.EdibleID, info = edible.EdibleS};
        }

        // PUT: api/edible/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdible(int id, Edible edible)
        {
            if (id != edible.EdibleID)
            {
                return BadRequest();
            }

            _context.Entry(edible).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EdibleExists(id))
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

        // POST: api/edible
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Edible>> PostEdible(Edible edible)
        {
            _context.Edibles.Add(edible);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEdible", new { id = edible.EdibleID }, edible);
        }

        // DELETE: api/edible/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Edible>> DeleteEdible(int id)
        {
            var edible = await _context.Edibles.FindAsync(id);
            if (edible == null)
            {
                return NotFound();
            }

            _context.Edibles.Remove(edible);
            await _context.SaveChangesAsync();

            return edible;
        }

        private bool EdibleExists(int id)
        {
            return _context.Edibles.Any(e => e.EdibleID == id);
        }
    }
}
