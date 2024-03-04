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

namespace DotInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Comments : ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;


        public Comments( ICommentsRepository  commentsRepository, IMapper mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var comments = await _commentsRepository.GetAllAsync();
            return Ok(comments);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _commentsRepository.GetAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            

            try
            {
                await _commentsRepository.UpdateAsync(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            await _commentsRepository.AddAsync(comment);

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentsRepository.GetAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CommentExists(int id)
        {
            return await _commentsRepository.Exists(id);
        }
    }
}
