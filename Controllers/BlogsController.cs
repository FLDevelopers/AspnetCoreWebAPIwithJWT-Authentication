using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogsApi.Data;
using BlogsApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> GetBlogs(int id)
        {
            var blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null)
            {
                return NotFound();
            }

            return blogs;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogs(int id, Blogs blogs)
        {
            if (id != blogs.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogsExists(id))
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

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blogs>> PostBlogs(Blogs blogs)
        {
            _context.Blogs.Add(blogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogs", new { id = blogs.BlogId }, blogs);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogs(int id)
        {
            var blogs = await _context.Blogs.FindAsync(id);
            if (blogs == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blogs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogsExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
