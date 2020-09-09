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
    [Route("api/planttypes")]
    [ApiController]
    public class PlantTypesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public PlantTypesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/planttypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantType>>> GetPlantTypes()
        {
            return await _context.PlantTypes.ToListAsync();
        }

        // GET: api/planttypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantType>> GetPlantType(int id)
        {
            var plantType = await _context.PlantTypes.FindAsync(id);

            if (plantType == null)
            {
                return NotFound();
            }

            return plantType;
        }

        // PUT: api/planttypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantType(int id, PlantType plantType)
        {
            if (id != plantType.PlantTypeID)
            {
                return BadRequest();
            }

            _context.Entry(plantType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantTypeExists(id))
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

        // POST: api/planttypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlantType>> PostPlantType(PlantType plantType)
        {
            _context.PlantTypes.Add(plantType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantType", new { id = plantType.PlantTypeID }, plantType);
        }

        // DELETE: api/planttypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlantType>> DeletePlantType(int id)
        {
            var plantType = await _context.PlantTypes.FindAsync(id);
            if (plantType == null)
            {
                return NotFound();
            }

            _context.PlantTypes.Remove(plantType);
            await _context.SaveChangesAsync();

            return plantType;
        }

        private bool PlantTypeExists(int id)
        {
            return _context.PlantTypes.Any(e => e.PlantTypeID == id);
        }
    }
}
