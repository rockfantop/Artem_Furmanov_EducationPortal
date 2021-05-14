using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using Portal.Infrastructure.XML;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserHandler userHandler;

        public UserRepository(IUserHandler handler)
        {
            this.userHandler = handler;
        }

        public void Create(User item)
        {
            userHandler.CreateUser(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUser(string email)
        {
            return userHandler.FindUser(email);
        }

        public IEnumerable<User> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public User GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
