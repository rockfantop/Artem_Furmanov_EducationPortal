using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IHasher hasher;

        public UserService(IRepository<User> repository, IHasher hasher) 
        {
            this.userRepository = repository;
            this.hasher = hasher;
        }

        public IServiceResult Registation(InputUserDTO newUser)
        {
            try
            {
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = newUser.Email,
                    Password = hasher.GetHash(newUser.Password)
                };

                this.userRepository.Create(user);

                return ServiceResult.FromResult(true, "Successful registrated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ServiceResult.FromResult(false, "User is already exist!");
            }
        }

        public IServiceResult Athentication(InputUserDTO newLog)
        {
            try
            {
                var user = this.userRepository
                    .GetEntity(x => x.Email == newLog.Email && x.Password == this.hasher.GetHash(newLog.Password));

                if (user == null)
                {
                    return ServiceResult.FromResult(false, "Invalid email or password!");
                }

                return ServiceResult.FromResult(true, "Welcome.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ServiceResult.FromResult(false, "User is already exist!");
            }
        }
    }
}
