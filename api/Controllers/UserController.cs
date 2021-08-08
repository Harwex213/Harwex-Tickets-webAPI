using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Requests;
using api.Models.Responses;
using api.Services;
using api.Services.PasswordHashers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly UserContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(UserContext context, IPasswordHasher passwordHasher,
            AccessTokenGenerator accessTokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
        }

        [Authorize]
        [HttpGet]
        public IActionResult SomeThingVau()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            if (registerRequest.Password != registerRequest.ConfirmPassword)
                return BadRequest(new ErrorResponse("Password does not match confirm password."));

            var existingUserByUsername =
                await _context.Users.FirstOrDefaultAsync(u => u.Username == registerRequest.Username);
            if (existingUserByUsername != null) return Conflict(new ErrorResponse("Username already exists."));

            var existingUserByPhoneNumber =
                await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerRequest.PhoneNumber);
            if (existingUserByPhoneNumber != null) return Conflict(new ErrorResponse("Phone Number already taken."));


            _context.Users.Add(new User
            {
                Id = 0,
                Username = registerRequest.Username,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = _passwordHasher.HashPassword(registerRequest.Password)
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
            if (user == null) return Unauthorized();

            var isCorrectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isCorrectPassword) return Unauthorized();

            var accessToken = _accessTokenGenerator.GenerateToken(user);

            return Ok(new LoginResponse {AccessToken = accessToken});
        }

        private IActionResult BadRequestModelState()
        {
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}