using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class UserService :  IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository repository) 
        {
            this.userRepository = repository;
        }

        public void CreateUser(User newUser)
        {
            newUser.Password = new SHA256Hasher().GetHash(newUser.Password);

            userRepository.Create(newUser);
        }

        public User FindUser(string email)
        {
            return userRepository.FindUser(email);
        }

        public bool VerifyUser(string email, string password)
        {
            if (FindUser(email) is User user)
            {
                return new SHA256Hasher().VerifyHash(password, user.Password);
            }
            else
            {
                return false;
            }
        }
    }
}
