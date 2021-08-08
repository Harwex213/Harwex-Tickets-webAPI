using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Contexts
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("anyone")]
        public IActionResult Anyone()
        {
            return Ok("Who are you?");
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult User()
        {
            return Ok("You are user!");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("You are admin!");
        }
    }
}