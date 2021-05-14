using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IUserService
    {
        User FindUser(string email);

        void CreateUser(User newUser);

        bool VerifyUser(string email, string password);
    }
}
