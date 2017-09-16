using System;

namespace iKino.API.Models
{
    public sealed class AuthToken
    {
        public string Token { get; private set; }
        public DateTime Expiry { get; private set; }

        private AuthToken()
        {

        }

        public static AuthToken Create(string token, DateTime expiry)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token), "Token cannot be empty.");

            return new AuthToken
            {
                Token = token,
                Expiry = expiry,
            };
        }
    }
}