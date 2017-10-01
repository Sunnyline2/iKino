namespace eKino.Infrastructure.Services
{
    public interface ITokenService
    {
        TokenService.Token GenerateToken(string userId, string username, string role);
    }

}