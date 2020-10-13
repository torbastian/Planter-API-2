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
        {   //get everything from plant types
            return await _context.PlantTypes.ToListAsync();
        }

        // GET: api/planttypes/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<PlantType>> GetPlantType(int id)
        {   //get everything from plant types at the id
            var plantType = await _context.PlantTypes.FindAsync(id);

            if (plantType == null)
            {
                return NotFound();
            }

            return plantType;
        }

        // GET: api/planttypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantTypeDto>>> GetPlantTypeDtos()
        {   //Get all plant type DTO
            var query = _context.PlantTypes.Select(p => new PlantTypeDto()
            {
                id = p.PlantTypeID,
                info = p.PType
            });

            var plantTypes = await query.ToListAsync();

            return plantTypes;
        }

        // GET: api/planttypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTypeDto>> GetPlantTypeDtoId(int id)
        {   //Get the plant type DTO at the id
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
        public async Task<IActionResult> PutPlantType(int id, PlantTypeDto plantType)
        {   //Update the plant type 
            var result = await _context.PlantTypes.SingleOrDefaultAsync(t => t.PlantTypeID == id);

            if (result != null)
            {
                result.PType = plantType.info;
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        // POST: api/planttypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlantType>> PostPlantType(PlantTypeDto plantType)
        {   //Create a new plant type based on the provided plant type DTO
            PlantType newType = new PlantType();
            newType.PType = plantType.info;

            _context.PlantTypes.Add(newType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantType", new { id = newType.PlantTypeID }, newType);
        }

        // DELETE: api/planttypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlantType>> DeletePlantType(int id)
        {   //Delete a plant type at the ID
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
