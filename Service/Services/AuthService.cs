using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Service.Exceptions;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly ITokensGenerator _tokensGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(ITokensGenerator tokensGenerator, IRefreshTokenValidator refreshTokenValidator,
            IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _tokensGenerator = tokensGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task Register(User user)
        {
            var isUserExist =
                _unitOfWork.Repository<User>().List(u => u.Username == user.Username).FirstOrDefault();
            if (isUserExist != null) throw new ConflictException("Username already exists.");

            var isPhoneNumberExist =
                _unitOfWork.Repository<User>().List(u => u.PhoneNumber == user.PhoneNumber).FirstOrDefault();
            if (isPhoneNumberExist != null) throw new ConflictException("Phone Number already taken.");

            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.RoleName = "user";
            _unitOfWork.Repository<User>().Add(user);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task<(string accessToken, string refreshToken)> LogIn(string username, string password)
        {
            var user = _unitOfWork.Repository<User>().List(u => u.Username == username).FirstOrDefault();
            if (user == null) throw new UnauthorizedException();

            var isCorrectPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            if (!isCorrectPassword) throw new UnauthorizedException();

            return await GenerateTokens(user);
        }

        public void LogOut(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<(string accessToken, string refreshToken)> Refresh(string refreshToken)
        {
            throw new NotImplementedException();
        }

        private async Task<(string accessToken, string refreshToken)> GenerateTokens(User user,
            RefreshToken refreshToken = null)
        {
            var claims = new List<Claim>
            {
                new("id", user.Id.ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };

            var accessTokenString = _tokensGenerator.GenerateAccessToken(claims);
            var refreshTokenString = _tokensGenerator.GenerateRefreshToken();
            refreshToken ??= _unitOfWork.Repository<RefreshToken>().List(t => t.UserId == user.Id).FirstOrDefault();

            if (refreshToken != null) _unitOfWork.Repository<RefreshToken>().Delete(refreshToken);

            _unitOfWork.Repository<RefreshToken>().Add(new RefreshToken
            {
                Id = default,
                Token = refreshTokenString,
                UserId = user.Id,
                IsDeleted = false
            });

            await _unitOfWork.CommitAsync();

            return (accessTokenString, refreshTokenString);
        }
    }
}