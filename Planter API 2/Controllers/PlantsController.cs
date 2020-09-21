using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // GET: api/plants/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Plants>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        // GET: api/plants/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Plants>> GetPlants(int id)
        {
            var plants = await _context.Plants.FindAsync(id);

            if (plants == null)
            {
                return NotFound();
            }

            return plants;
        }

        // GET: api/plants
        // OBS! Returns PlantsDto WITHOUT AN IMAGE, To get an image Use the GetPlantImage method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantsDto>>> GetPlantsDto()
        {
            var query = _context.Plants
                .Include(p => p.PlantType)
                .Include(p => p.Climates)
                .Include(p => p.Edible)
                .Include(P => P.Users)
                .Include(p => p.ApprovedType)
                .Select(p => new PlantsDto
                {
                    id = p.PlantID,
                    info = p.PlantName,
                    type = p.PlantType.PType,
                    climate = p.Climates.Climate,
                    edible = p.Edible.EdibleS,
                    username = p.Users.Username,
                    approved = p.ApprovedType.AType
                });

            var plantList = await query.ToListAsync();

            return plantList;
        }

        // GET: api/plants/5
        // OBS! Returns PlantsDTO WITHOUT AN IMAGE, To get an image Use the GetPlantImage method
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantsDto>> GetPlantsDto(int id)
        {
            var query = _context.Plants.Where(p => p.PlantID == id)
                .Include(p => p.PlantType)
                .Include(p => p.Climates)
                .Include(p => p.Edible)
                .Include(P => P.Users)
                .Include(p => p.ApprovedType)
                .Select(p => new PlantsDto
                {
                    id = p.PlantID,
                    info = p.PlantName,
                    type = p.PlantType.PType,
                    climate = p.Climates.Climate,
                    edible = p.Edible.EdibleS,
                    username = p.Users.Username,
                    approved = p.ApprovedType.AType
                });

            var plant = await query.FirstOrDefaultAsync();

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
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

        [HttpGet("image/{id}")]
        public async Task<ActionResult<imagePlant>> GetPlantImage(int id)
        {
            var plant = await _context.Plants.Where(p => p.PlantID == id).FirstOrDefaultAsync();

            if (plant == null)
            {
                return NotFound();
            }

            imagePlant rPlant = new imagePlant()
            {
                id = id,
                image = Convert.ToBase64String(plant.Image)
            };

            return rPlant;
        }

        [HttpPut("image/{id}")]
        public async Task<IActionResult> PutPlantImage(int id, imagePlant iPlant)
        {
            var plant = _context.Plants.Where(p => p.PlantID == id).FirstOrDefault();

            if (plant == null || iPlant == null)
            {
                return BadRequest();
            }

            byte[] byteImage = Convert.FromBase64String(iPlant.image);

            plant.Image = byteImage;
            await _context.SaveChangesAsync();

            return Ok();
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

public class imagePlant
{
    public int id { get; set; }
    public string image { get; set; }
}