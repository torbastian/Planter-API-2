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
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly PlantsContext _context;

        public CommentsController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/comments/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {   //Get everything from comments
            return await _context.Comments.ToListAsync();
        }

        // GET: api/comments/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Comments>> GetComments(int id)
        {   //Get everything from comments based on the ID
            var comments = await _context.Comments.FindAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        [HttpGet]
        public ActionResult GetCommentsDto()
        {   //Dont get anything, why would you need to display ALL comments?
            return NotFound();
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CommentsDto>>> GetCommentsByArticleId(int id)
        {
            //Get a comment based on the article id
            var query = _context.Comments.Where(c => c.CommentID == id)
                .Include(c => c.Users)
                .Select(c => new CommentsDto
                {
                    id = c.CommentID,
                    info = c.Note,
                    username = c.Users.Username
                });

            var commentList = await query.ToListAsync();

            return commentList;
        }

        // PUT: api/comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments(int id, Comments comments)
        {   //Update a comment based on its id
            if (id != comments.CommentID)
            {
                return BadRequest();
            }

            _context.Entry(comments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
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

        // POST: api/comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {   //Create a new Comment
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comments.CommentID }, comments);
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comments>> DeleteComments(int id)
        {   //Delete a comment based on its ID
            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();

            return comments;
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.CommentID == id);
        }
    }
}
