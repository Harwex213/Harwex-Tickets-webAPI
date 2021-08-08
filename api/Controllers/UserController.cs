using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models;
using api.Models.Requests;
using api.Models.Responses;
using api.Services;
using api.Services.PasswordHashers;
using api.Services.TokenGenerators;
using api.Services.TokenValidators;
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
        private readonly HarwexTicketsApiContext _harwexTicketsApiContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;


        public UserController(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            RefreshTokenValidator refreshTokenValidator,
            HarwexTicketsApiContext harwexTicketsApiContext, IPasswordHasher passwordHasher)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _harwexTicketsApiContext = harwexTicketsApiContext;
            _passwordHasher = passwordHasher;
            _refreshTokenValidator = refreshTokenValidator;
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
                await _harwexTicketsApiContext.Users.FirstOrDefaultAsync(u => u.Username == registerRequest.Username);
            if (existingUserByUsername != null) return Conflict(new ErrorResponse("Username already exists."));

            var existingUserByPhoneNumber =
                await _harwexTicketsApiContext.Users.FirstOrDefaultAsync(u =>
                    u.PhoneNumber == registerRequest.PhoneNumber);
            if (existingUserByPhoneNumber != null) return Conflict(new ErrorResponse("Phone Number already taken."));


            _harwexTicketsApiContext.Users.Add(new User
            {
                Id = 0,
                Username = registerRequest.Username,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = _passwordHasher.HashPassword(registerRequest.Password),
                Role = "user"
            });
            await _harwexTicketsApiContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            var user = await _harwexTicketsApiContext.Users.FirstOrDefaultAsync(
                u => u.Username == loginRequest.Username);
            if (user == null) return Unauthorized();

            var isCorrectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isCorrectPassword) return Unauthorized();

            return Ok(await Authenticate(user));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var userId = HttpContext.User.FindFirstValue("id");

            if (userId == null) return NotFound();

            var refreshToken = await _harwexTicketsApiContext.RefreshTokens.FirstOrDefaultAsync(
                t => t.UserId == int.Parse(userId));

            if (refreshToken == null) return Unauthorized();

            _harwexTicketsApiContext.RefreshTokens.Remove(refreshToken);
            await _harwexTicketsApiContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            var isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken) return BadRequest(new ErrorResponse("Invalid refresh token."));

            var refreshToken =
                await _harwexTicketsApiContext.RefreshTokens.FirstOrDefaultAsync(
                    t => t.Token == refreshRequest.RefreshToken);
            if (refreshToken == null) return BadRequest(new ErrorResponse("Invalid refresh token."));

            var user = await _harwexTicketsApiContext.Users.FindAsync(refreshToken.UserId);
            if (user == null) return BadRequest(new ErrorResponse("User doesn't exist"));

            return Ok(await Authenticate(user, refreshToken));
        }

        private async Task<AuthenticatedResponse> Authenticate(User user, RefreshToken refreshToken = null)
        {
            var accessTokenString = _accessTokenGenerator.Generate(user);
            var refreshTokenString = _refreshTokenGenerator.Generate();
            refreshToken ??= await _harwexTicketsApiContext.RefreshTokens.FirstOrDefaultAsync(
                t => t.UserId == user.Id);

            if (refreshToken != null)
            {
                _harwexTicketsApiContext.RefreshTokens.Remove(refreshToken);
                await _harwexTicketsApiContext.SaveChangesAsync();
            }

            _harwexTicketsApiContext.RefreshTokens.Add(new RefreshToken
            {
                Id = 0,
                Token = refreshTokenString,
                UserId = user.Id
            });
            await _harwexTicketsApiContext.SaveChangesAsync();

            return new AuthenticatedResponse
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshTokenString
            };
        }

        private IActionResult BadRequestModelState()
        {
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}