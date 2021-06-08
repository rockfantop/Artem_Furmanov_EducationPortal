using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Portal.Application.Hashers
{
    public class SHA256Hasher : IHasher
    {
        private string SHA256Hash(string value)
        {
            StringBuilder builder = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(encoding.GetBytes(value));

                foreach (Byte b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }

            return builder.ToString();
        }

        public string GetHash(string input)
        {
            return SHA256Hash(input);
        }

        public bool VerifyHash(string input, string hash)
        {
            return hash.GetHashCode() == SHA256Hash(input).GetHashCode();
        }
    }
}
