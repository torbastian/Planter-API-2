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
    [Route("api/plants")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly PlantsContext _context;

        public PlantsController(PlantsContext context)
        {
            _context = context;
        }

        // GET: apiplants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plants>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        // GET: api/plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plants>> GetPlants(int id)
        {
            var plants = await _context.Plants.FindAsync(id);

            if (plants == null)
            {
                return NotFound();
            }

            return plants;
        }

        // PUT: api/plants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlants(int id, Plants plants)
        {
            if (id != plants.PlantID)
            {
                return BadRequest();
            }

            _context.Entry(plants).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantsExists(id))
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

        // POST: api/plants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Plants>> PostPlants(Plants plants)
        {
            _context.Plants.Add(plants);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlants", new { id = plants.PlantID }, plants);
        }

        // DELETE: api/plants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plants>> DeletePlants(int id)
        {
            var plants = await _context.Plants.FindAsync(id);
            if (plants == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plants);
            await _context.SaveChangesAsync();

            return plants;
        }

        private bool PlantsExists(int id)
        {
            return _context.Plants.Any(e => e.PlantID == id);
        }
    }
}
