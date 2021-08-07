using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;

        public UserController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public ActionResult SomeThingVau()
        {
            return Ok();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            _context.Users.Add(new User
            {
                Id = 0,
                Password = registerDto.Password,
                PhoneNumber = registerDto.PhoneNumber,
                Username = registerDto.Username
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginDto loginDto)
        {
            var identity = await GetClaimsIdentity(loginDto.Username, loginDto.Password);
            if (identity == null) return BadRequest(new {errorText = "Invalid username or password."});

            var dateTimeNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                notBefore: dateTimeNow,
                claims: identity.Claims,
                expires: dateTimeNow.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            
            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "JWT", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}