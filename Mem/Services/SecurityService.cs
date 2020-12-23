using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Mem.Services
{
    public class SecurityService
    {

        public String GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }


        public String Hash(string input, string salt)
        {
            byte[] _salt = Convert.FromBase64String(salt);
            byte[] hash = KeyDerivation.Pbkdf2(input, _salt, KeyDerivationPrf.HMACSHA256, 10000, 256);
            return Convert.ToBase64String(hash);
        }
    }
}
