using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IUserService
    {
        IServiceResult Registation(InputUserDTO newUser);

        IServiceResult Athentication(InputUserDTO userLog);
    }
}
