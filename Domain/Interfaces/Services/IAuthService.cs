using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        void Register(User user);
        Task<(string accessToken, string refreshToken)> LogIn(string username, string password);
        void LogOut(long userId);
        Task<(string accessToken, string refreshToken)> Refresh(string refreshToken);
    }
}