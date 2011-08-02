using System;
using System.Security.Cryptography;
using System.Text;

namespace Maplink.Webservices.Places.Client.Helpers
{
    public class HmacSha1HashGenerator : IHmacSha1HashGenerator
    {
        public string For(string content, string key)
        {
            var encoding = Encoding.UTF8;

            var computedHash = 
                new HMACSHA1(encoding.GetBytes(key))
                    .ComputeHash(encoding.GetBytes(content));

            return BitConverter
                .ToString(computedHash)
                .Replace("-", String.Empty)
                .ToLower();
        }
    }
}
