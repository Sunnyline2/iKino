using iKino.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace iKino.API.Services
{
    public class HashService : IHashService
    {
        private readonly IConfiguration _configuration;

        public HashService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Hash(string value)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.Default.GetBytes($"{value}-{_configuration["Database:Salt"]}");
                return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
    }
}