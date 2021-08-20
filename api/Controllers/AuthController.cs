using System;
using System.Security.Claims;
using System.Threading.Tasks;
using api.ViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Services;

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
                {
                    return BadRequest(new ErrorResponse("Passwords must match"));
                }

                await _authService.Register(_authMapper.Map<User>(registerRequest));
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                if (e is ConflictException)
                {
                    return Conflict(new ErrorResponse(e.Message));
                }

                return BadRequest(new ErrorResponse());
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
                if (e is UnauthorizedException)
                {
                    return Unauthorized(new ErrorResponse(e.Message));
                }

                return BadRequest(new ErrorResponse());
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue("id");
                if (userId == null)
                {
                    return NotFound(new ErrorResponse("User with that id doesn't found"));
                }

                await _authService.LogOut(long.Parse(userId));
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                if (e is UnauthorizedException)
                {
                    return Unauthorized(new ErrorResponse(e.Message));
                }

                return BadRequest(new ErrorResponse());
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
                if (e is NotFoundException)
                {
                    return Unauthorized(new ErrorResponse(e.Message));
                }

                return BadRequest(new ErrorResponse());
            }
        }
    }
}