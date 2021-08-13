using Domain.Entities;
using Domain.Interfaces.Services;

namespace Service
{
    public class AuthService : IAuthService
    {
        public void Register(User user)
        {
            throw new System.NotImplementedException();
        }

        public void LogIn(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public void LogOut(long userId)
        {
            throw new System.NotImplementedException();
        }

        public (string accessToken, string refreshToken) Refresh(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}