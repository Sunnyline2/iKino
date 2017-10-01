using eKino.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;

namespace eKino.Infrastructure.Services
{
    public class HashService : IHashService
    {
        private readonly HashSettings _hashSettings;

        public HashService(IOptions<HashSettings> hashSettings)
        {
            _hashSettings = hashSettings.Value;
        }

        public string Hash(string value)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.Default.GetBytes(string.Concat(value, _hashSettings.Salt));
                return BitConverter.ToString(md5.ComputeHash(bytes));
            }
        }
    }
}