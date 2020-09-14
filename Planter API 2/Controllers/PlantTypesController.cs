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

        // GET: api/planttypes/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<PlantType>>> GetPlantTypes()
        {
            return await _context.PlantTypes.ToListAsync();
        }

        // GET: api/planttypes/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<PlantType>> GetPlantType(int id)
        {
            var plantType = await _context.PlantTypes.FindAsync(id);

            if (plantType == null)
            {
                return NotFound();
            }

            return plantType;
        }

        // GET: api/planttypes
        [HttpGet]
        public IQueryable<PlantTypeDto> GetPlantTypeDtos()
        {
            IQueryable<PlantTypeDto> plantType = _context.PlantTypes.Select(p => new PlantTypeDto()
            {
                id = p.PlantTypeID,
                info = p.PType
            });

            return plantType;
        }

        // GET: api/planttypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTypeDto>> GetPlantTypeDtoId(int id)
        {
            var plantType = await _context.PlantTypes.FindAsync(id);

            if (plantType == null)
            {
                return NotFound();
            }

            return new PlantTypeDto() { id = plantType.PlantTypeID, info = plantType.PType };
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
