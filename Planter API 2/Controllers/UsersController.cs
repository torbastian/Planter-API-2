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

        //none of this is implemented and is not expected to work

        // GET: api/users
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {   //Get everything from users
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {   //Get everything from users at the ID
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsersDto()
        {   //Get all users as DTO
            var query = _context.Users
                .Include(u => u.UserType)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    username = u.Username,
                    password = u.Password,
                    type = u.UserType.UType
                });

            var userList = await query.ToListAsync();

            return userList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDto>> GetUsersDtoId(int id)
        {   //Get a User DTO at the ID
            var query = _context.Users.Where(u => u.UserID == id)
                .Include(u => u.UserType)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    username = u.Username,
                    password = u.Password,
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
        [HttpPost("auth")]
        public async Task<ActionResult<UsersDto>> AuthenticateUser(UsersDto userDetails)
        {
            //Recives User with only Username and Password Values
            //Get the user that matches these and return it
            if (userDetails.username == null || userDetails.password == null)
            {
                return BadRequest();
            }

            var query = _context.Users.Where(u => u.Username == userDetails.username)
                .Select(u => new UsersDto
                {
                    id = u.UserID,
                    username = u.Username,
                    password = u.Password,
                    type = u.UserType.UType
                });

            var user = await query.FirstOrDefaultAsync();

            //Return the user
            //Password validation (TEMPORARY)
            if (user == null)
            {
                return NotFound();
            }
            else if (user.password == userDetails.password)
            {
                return user;
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, UsersDto user)
        {   //Update a user at the ID
            var result = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id);

            if (result != null)
            {
                result.Username = user.username;
                result.Password = user.password;
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        // POST: api/users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {   //Create a new user
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserID }, users);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {   //Delete a user at the ID
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
