using System;
using System.Security.Claims;
using System.Threading.Tasks;
using api.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

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
        public async Task<IActionResult> Register(AuthRegisterRequest registerRequest)
        {
            try
            {
                if (registerRequest.Password != registerRequest.ConfirmPassword)
                    return BadRequest("Passwords must match");
            
                await _authService.Register(_authMapper.Map<User>(registerRequest));
                return Ok();
            }
            catch (Exception e)
            {
                if (e is ConflictException) return Conflict(e.Message);
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(AuthLoginRequest loginRequest)
        {
            try
            {
                var (accessToken, refreshToken) =
                    await _authService.LogIn(loginRequest.Username, loginRequest.Password);
                return Ok(new AuthAuthenticatedResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception e)
            {
                if (e is UnauthorizedException) return Unauthorized();
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue("id");
                if (userId == null) return NotFound();
                await _authService.LogOut(long.Parse(userId));
                return Ok();
            }
            catch (Exception e)
            {
                if (e is UnauthorizedException) return Unauthorized();
                return BadRequest();
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(AuthRefreshRequest refreshRequest)
        {
            try
            {
                var (accessToken, refreshToken) = await _authService.Refresh(refreshRequest.RefreshToken);
                return Ok(new AuthAuthenticatedResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception e)
            {
                if (e is NotFoundException) return Unauthorized();
                return BadRequest();
            }
        }
    }
}