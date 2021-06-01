using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface IUserService
    {
        Task<IServiceResult> RegistationAsync(InputUserDTO inputUserDTO);

        Task<IServiceResult<LogginedUserDTO>> AuthenticationAsync(InputUserDTO inputUserDTO);

        Task<IServiceResult> UpdateUserCoursesAsync(LogginedUserDTO logginedUserDTO);
    }
}
