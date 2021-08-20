using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;

namespace Service.Services.Impl
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
            if (isUserExist != null)
            {
                throw new ConflictException("Username already exists.");
            }

            var isPhoneNumberExist =
                _unitOfWork.Repository<User>().List(u => u.PhoneNumber == user.PhoneNumber).FirstOrDefault();
            if (isPhoneNumberExist != null)
            {
                throw new ConflictException("Phone Number already taken.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.RoleName = "user";
            _unitOfWork.Repository<User>().Add(user);

            await _unitOfWork.CommitAsync();
        }

        public async Task<(string accessToken, string refreshToken)> LogIn(string username, string password)
        {
            var user = _unitOfWork.Repository<User>().List(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                throw new UnauthorizedException("Username or password is wrong.");
            }

            var isCorrectPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            if (!isCorrectPassword)
            {
                throw new UnauthorizedException("Username or password is wrong.");
            }

            var (accessTokenString, refreshTokenString) = GenerateTokens(user);
            await UpdateRefreshToken(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenString
            });

            return (accessTokenString, refreshTokenString);
        }

        public async Task LogOut(long userId)
        {
            var refreshToken = _unitOfWork.Repository<RefreshToken>().List(u => u.UserId == userId).FirstOrDefault();
            if (refreshToken == null)
            {
                throw new UnauthorizedException("User doesn't exist.");
            }

            _unitOfWork.Repository<RefreshToken>().Delete(refreshToken);
            await _unitOfWork.CommitAsync();
        }

        public async Task<(string accessToken, string refreshToken)> Refresh(string oldRefreshTokenString)
        {
            var isValidRefreshToken = _refreshTokenValidator.Validate(oldRefreshTokenString);
            if (!isValidRefreshToken)
            {
                throw new UnauthorizedException("Unauthorized.");
            }

            var refreshToken = _unitOfWork.Repository<RefreshToken>().List(u => u.Token == oldRefreshTokenString)
                .FirstOrDefault();
            if (refreshToken == null)
            {
                throw new UnauthorizedException("Unauthorized.");
            }

            var user = _unitOfWork.Repository<User>().Find(refreshToken.UserId);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            var (newAccessTokenString, newRefreshTokenString) = GenerateTokens(user);
            await UpdateRefreshToken(new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshTokenString
            }, refreshToken);

            return (newAccessTokenString, newRefreshTokenString);
        }

        private (string accessToken, string refreshToken) GenerateTokens(User user)
        {
            var claims = new List<Claim>
            {
                new("id", user.Id.ToString()),
                new("Name", user.Username),
                new("Role", user.RoleName)
            };

            var accessTokenString = _tokensGenerator.GenerateAccessToken(claims);
            var refreshTokenString = _tokensGenerator.GenerateRefreshToken();

            return (accessTokenString, refreshTokenString);
        }

        private async Task UpdateRefreshToken(RefreshToken newRefreshToken, RefreshToken oldRefreshToken = null)
        {
            oldRefreshToken ??= _unitOfWork.Repository<RefreshToken>().List(r => r.UserId == newRefreshToken.UserId)
                .FirstOrDefault();
            if (oldRefreshToken != null)
            {
                _unitOfWork.Repository<RefreshToken>().Delete(oldRefreshToken);
            }

            _unitOfWork.Repository<RefreshToken>().Add(new RefreshToken
            {
                Id = default,
                Token = newRefreshToken.Token,
                UserId = newRefreshToken.UserId,
                IsDeleted = false
            });

            await _unitOfWork.CommitAsync();
        }
    }
}