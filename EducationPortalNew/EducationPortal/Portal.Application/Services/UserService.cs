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
                if (this.userRepository.GetEntity(x => x.Email == newUser.Email) == null)
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
                else
                {
                    return ServiceResult.FromResult(false, "User is already exist!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ServiceResult.FromResult(false, "User is already exist!");
            }
        }

        public IServiceResult<LogginedUserDTO> Athentication(InputUserDTO newLog)
        {
            try
            {
                var user = this.userRepository
                    .GetEntity(x => x.Email == newLog.Email && x.Password == this.hasher.GetHash(newLog.Password));

                if (user == null)
                {
                    return ServiceResult<LogginedUserDTO>.FromResult(false, null, "Invalid email or password!");
                }

                var logginedUserDTO = new LogginedUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    OwnedCourses = user.OwnedCourses
                };

                return ServiceResult<LogginedUserDTO>.FromResult(true, logginedUserDTO, "Welcome.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ServiceResult<LogginedUserDTO>.FromResult(false, null, "User is already exist!");
            }
        }

        public IServiceResult UpdateInfo(LogginedUserDTO logginedUser)
        {
            try
            {
                User user = new User
                {
                    Id = logginedUser.Id,
                    Email = logginedUser.Email,
                    Name = logginedUser.Name,
                    OwnedCourses = logginedUser.OwnedCourses
                };

                this.userRepository.Update(user);

                return ServiceResult.FromResult(true, "Successful updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Oops, something go wrong");
            }
        }
    }
}
