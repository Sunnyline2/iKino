using System;
using System.IdentityModel.Tokens.Jwt;

namespace iKino.API.Services.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GenerateToken(string userId, string username, string role, DateTime expires);
    }
}