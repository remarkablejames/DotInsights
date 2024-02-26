using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotInsights.API.Data;
using DotInsights.API.Models;
using DotInsights.API.Models.DTOs.Post;

namespace DotInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Posts : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public Posts(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(_mapper.Map<List<GetPostDto>>(posts));
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostDetailsDto>> GetPost(int id)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            
            

            return Ok(_mapper.Map<GetPostDetailsDto>(post));
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreatePostDto newPostData)
        {
            // var post = new Post
            // {
            //     Title = newPostData.Title,
            //     Content = newPostData.Content,
            //     Created = DateTime.UtcNow
            // };
            
            var post = _mapper.Map<Post>(newPostData);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
