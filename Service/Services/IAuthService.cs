﻿using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Services
{
    public interface IAuthService
    {
        Task Register(User user);
        Task<(string accessToken, string refreshToken)> LogIn(string username, string password);
        Task LogOut(long userId);
        Task<(string accessToken, string refreshToken)> Refresh(string oldRefreshTokenString);
    }
}