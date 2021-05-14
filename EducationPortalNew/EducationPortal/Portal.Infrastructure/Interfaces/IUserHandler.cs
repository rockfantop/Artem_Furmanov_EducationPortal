using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Interfaces
{
    public interface IUserHandler
    {
        void CreateUser(User newUser);

        User FindUser(string email);

        void UpdateUser(User newUser);

        void DeleteUser(int id);

        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        void SaveUser();
    }
}
