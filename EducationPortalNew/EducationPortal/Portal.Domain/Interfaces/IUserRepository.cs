using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUser(string email);
    }
}
