using eKino.Infrastructure.DTO;
using eKino.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eKino.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;

        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value ?? throw new ArgumentNullException(nameof(tokenSettings));
        }
        public class Token
        {
            public string SecurityToken { get; set; }
            public DateTime Expiry { get; set; }
        }

        public Token GenerateToken(string userId, string username, string role)
        {
            var expires = DateTime.UtcNow.AddMinutes(_tokenSettings.Expiry);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim(ClaimTypes.Role, role)
            };


            var token = new JwtSecurityToken(_tokenSettings.Issuer, _tokenSettings.Audience, claims, DateTime.UtcNow,
               expires, new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key)), SecurityAlgorithms.HmacSha256));


            return new Token
            {
                SecurityToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiry = expires,
            };
            ;
        }
    }


    public static class TokenExtensions
    {
        public static TokenService.Token GenerateToken(this ITokenService service, UserDto user)
        {
            return service.GenerateToken(user.UserId.ToString(), user.Username, user.Role);
        }
    }
}