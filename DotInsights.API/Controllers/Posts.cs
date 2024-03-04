using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotInsights.API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotInsights.API.Data;
using DotInsights.API.Models;
using DotInsights.API.Models.DTOs.Post;
using Microsoft.AspNetCore.Authorization;

namespace DotInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Posts : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostsRepository _postsRepository;

        public Posts( IMapper mapper,IPostsRepository postsRepository)
        {
            _mapper = mapper;
            _postsRepository = postsRepository;
        }
        

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetPosts()
        {
            var posts = await _postsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<GetPostDto>>(posts));
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostDetailsDto>> GetPost(int id)
        {
            var post = await _postsRepository.GetAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            
            

            return Ok(_mapper.Map<GetPostDetailsDto>(post));
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }


            try
            {
                await _postsRepository.UpdateAsync(post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await PostExists(id))
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
        [Authorize]
        public async Task<ActionResult<Post>> PostPost(CreatePostDto newPostData)
        {
            // var post = new Post
            // {
            //     Title = newPostData.Title,
            //     Content = newPostData.Content,
            //     Created = DateTime.UtcNow
            // };
            
            var post = _mapper.Map<Post>(newPostData);
            await _postsRepository.AddAsync(post);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postsRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> PostExists(int id)
        {
            return await _postsRepository.Exists(id);
        }
    }
}
