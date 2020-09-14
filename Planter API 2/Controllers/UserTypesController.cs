﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planter_API_2.Models;

namespace Planter_API_2.Controllers
{
    [Route("api/usertypes")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public UserTypesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/usertypes/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUsertypes()
        {
            return await _context.Usertypes.ToListAsync();
        }

        // GET: api/usertypes/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<UserType>> GetUserType(int id)
        {
            var userType = await _context.Usertypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        // GET: api/usertype
        [HttpGet]
        public IQueryable<UserTypeDto> GetUserTypeDtos()
        {
            IQueryable<UserTypeDto> userTypes = _context.Usertypes.Select(u => new UserTypeDto()
            {
                id = u.UserTypeID,
                type = u.UType
            });

            return userTypes;
        }

        // GET: api/usertype/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDto>> GetUserTypeDtoId(int id)
        {
            var userType = await _context.Usertypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return new UserTypeDto() { id = userType.UserTypeID, type = userType.UType };
        }

        // PUT: api/usertypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(int id, UserType userType)
        {
            if (id != userType.UserTypeID)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/usertypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserType(UserType userType)
        {
            _context.Usertypes.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.UserTypeID }, userType);
        }

        // DELETE: api/usertypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserType>> DeleteUserType(int id)
        {
            var userType = await _context.Usertypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.Usertypes.Remove(userType);
            await _context.SaveChangesAsync();

            return userType;
        }

        private bool UserTypeExists(int id)
        {
            return _context.Usertypes.Any(e => e.UserTypeID == id);
        }
    }
}
