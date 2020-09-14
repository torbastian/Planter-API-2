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
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly PlantsContext _context;

        public ArticlesController(PlantsContext context)
        {
            _context = context;
        }

        // GET: api/articles/full
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        // GET: api/articles/full/5
        [HttpGet("full/{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // GET: api/articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticleDto()
        {
            //Get Article and the string value of the approvedtype
            var aatJoin = await _context.Articles
                .Join(_context.ApprovedTypes,
                A => A.ApprovedTypeID,
                AT => AT.ApprovedTypeID,
                (A, AT) => new ArticleDto
                {
                    id = A.ArticleID,
                    plantId = A.PlantsID,
                    approved = AT.AType,
                    text = A.Text,
                    tips = A.Tips
                }).ToListAsync();

            return aatJoin;
        }

        // GET: api/articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticleDtoId(int id)
        {
            //Get Article and the string value of the approvedtype where article id matches provided id
            var article = await _context.Articles.Where(a => a.ArticleID == id)
                .Join(_context.ApprovedTypes,
                A => A.ApprovedTypeID,
                AT => AT.ApprovedTypeID,
                (A, AT) => new ArticleDto
                {
                    id = A.ArticleID,
                    plantId = A.PlantsID,
                    approved = AT.AType,
                    text = A.Text,
                    tips = A.Tips
                }).FirstOrDefaultAsync();

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/articles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleID)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/articles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.ArticleID }, article);
        }

        // DELETE: api/articles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return article;
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleID == id);
        }
    }
}
