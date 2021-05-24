using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IHasher
    {
        string GetHash(string input);

        bool VerifyHash(string input, string hash);
    }
}
