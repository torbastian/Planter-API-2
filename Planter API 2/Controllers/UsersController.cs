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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PlantsContext _context;

        public UsersController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsersDto()
        {
            var query = _context.Users
                .Include(u => u.UserType)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    username = u.Username,
                    type = u.UserType.UType
                });

            var userList = await query.ToListAsync();

            return userList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDto>> GetUsersDtoId(int id)
        {
            var query = _context.Users.Where(u => u.UserID == id)
                .Include(u => u.UserType)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    username = u.Username,
                    type = u.UserType.UType
                });

            var user = await query.FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //HttpPost
        [HttpPost("/authenticate")]
        public async Task<ActionResult<UsersDto>> AuthenticateUser(UsersDto user)
        {
            //Recives User with only Username and Password Values
            var query = _context.Users
                .Include(u => u.UserType)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    password = u.Password,
                    type = u.UserType.UType
                });
            //Get the user that matches these and return it
            var login = await query.FirstOrDefaultAsync();

            if (login == null)
            {
                return NotFound();
            }
            //Return the user
            return login;
            //The website then stores this user locally, so that they can stay signed in
        }

        // PUT: api/users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.UserID)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserID }, users);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
