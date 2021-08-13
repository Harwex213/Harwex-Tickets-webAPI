using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        void Register(User user);
        void LogIn(string username, string password);
        void LogOut(long userId);
        (string accessToken, string refreshToken) Refresh(string refreshToken);
    }
}