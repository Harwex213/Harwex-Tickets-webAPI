using System.Security.Claims;
using api.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _authMapper;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IMapper authMapper)
        {
            _authService = authService;
            _authMapper = authMapper;
        }

        [HttpPost("register")]
        public IActionResult Register(AuthRegisterRequest registerRequest)
        {
            _authService.Register(_authMapper.Map<User>(registerRequest));
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult LogIn(AuthLoginRequest loginRequest)
        {
            _authService.LogIn(loginRequest.Username, loginRequest.Password);
            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            var userId = HttpContext.User.FindFirstValue("id");
            if (userId == null) return NotFound();
            _authService.LogOut(long.Parse(userId));
            return Ok();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(AuthRefreshRequest refreshRequest)
        {
            var (accessToken, refreshToken) = _authService.Refresh(refreshRequest.RefreshToken);
            return Ok(new AuthAuthenticatedResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}