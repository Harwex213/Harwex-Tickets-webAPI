namespace Domain.Interfaces
{
    public interface IRefreshTokenValidator
    {
        bool Validate(string refreshToken);
    }
}