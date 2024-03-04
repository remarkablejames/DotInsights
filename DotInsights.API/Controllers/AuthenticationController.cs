using DotInsights.API.Contracts;
using DotInsights.API.Models.DTOs.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthenticationController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] ApiUserDto userDto)
        {
            var errors = await _authManager.Register(userDto);
            
           

            if (errors.Any())
            {
                foreach (var error  in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
