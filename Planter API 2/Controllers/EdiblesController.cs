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
        {   //Get everything from edibles
            return await _context.Edibles.ToListAsync();
        }

        // GET: api/edible/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Edible>> GetEdible(int id)
        {   // Get everything from edibles based on the id
            var edible = await _context.Edibles.FindAsync(id);

            if (edible == null)
            {
                return NotFound();
            }

            return edible;
        }

        // GET: api/edible
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EdibleDto>>> GetEdibleDto()
        {   //Get all DTO of edibles
            var query = _context.Edibles.Select(e => new EdibleDto()
            {
                id = e.EdibleID,
                info = e.EdibleS
            });

            var edibles = await query.ToListAsync();

            return edibles;
        }

        // GET: api/edible/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EdibleDto>> GetEdibleDtoId(int id)
        {   //Get everything about the edible DTO at the ID
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
        public async Task<IActionResult> PutEdible(int id, EdibleDto edible)
        {   //Update an edible value, at the Id with the information provided
            var result = await _context.Edibles.SingleOrDefaultAsync(e => e.EdibleID == id);

            if (result != null)
            {
                result.EdibleS = edible.info;
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        // POST: api/edible
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Edible>> PostEdible(EdibleDto edible)
        {   //Create a new edible value based on the DTO provided
            Edible newEdible = new Edible();
            newEdible.EdibleS = edible.info;

            _context.Edibles.Add(newEdible);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEdible", new { id = newEdible.EdibleID }, newEdible);
        }

        // DELETE: api/edible/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Edible>> DeleteEdible(int id)
        {   //Delete an edible value at the ID
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
