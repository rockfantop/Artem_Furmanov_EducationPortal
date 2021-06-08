using AutoMapper;
using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> userRepository;
        private readonly IHasher hasher;
        private readonly IMapper mapper;

        public UserService(IAsyncRepository<User> repository, IHasher hasher, IMapper mapper) 
        {
            this.userRepository = repository;
            this.hasher = hasher;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> RegistationAsync(InputUserDTO inputUserDTO)
        {
            try
            {
                if (await this.userRepository.GetEntityAsync(x => x.Email == inputUserDTO.Email) == null)
                {
                    User user = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = inputUserDTO.Email,
                        Password = hasher.GetHash(inputUserDTO.Password)
                    };

                    await this.userRepository.CreateAsync(user);

                    return ServiceResult.FromResult(true, "Successful registrated.");
                }
                else
                {
                    return ServiceResult.FromResult(false, "User is already exist!");
                }
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "User is already exist!");
            }
        }

        public async Task<IServiceResult<LogginedUserDTO>> AuthenticationAsync(InputUserDTO inputUserDTO)
        {
            try
            {
                var password = this.hasher.GetHash(inputUserDTO.Password);

                var user = await this.userRepository
                    .GetEntityAsync(x => x.Email == inputUserDTO.Email && x.Password == this.hasher.GetHash(inputUserDTO.Password));

                if (user == null)
                {
                    return ServiceResult<LogginedUserDTO>.FromResult(false, null, "Invalid email or password!");
                }

                var logginedUserDTO = this.mapper.Map<LogginedUserDTO>(user);

                return ServiceResult<LogginedUserDTO>.FromResult(true, logginedUserDTO, "Welcome.");
            }
            catch (Exception)
            {
                return ServiceResult<LogginedUserDTO>.FromResult(false, null, "User is already exist!");
            }
        }

        public async Task<IServiceResult> UpdateUserCoursesAsync(LogginedUserDTO logginedUserDTO)
        {
            try
            {
                var user = this.mapper.Map<User>(logginedUserDTO);

                user.Password = (await this.userRepository.GetEntityAsync(x => x.Id == user.Id)).Password;
                
                await this.userRepository.UpdateAsync(user);

                return ServiceResult.FromResult(true, "Successful updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Oops, something go wrong");
            }
        }
    }
}
