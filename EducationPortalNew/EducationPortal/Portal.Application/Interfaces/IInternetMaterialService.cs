using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface IInternetMaterialService
    {
        Task<IServiceResult> AddInternetMaterialAsync(InternetMaterialDTO internetMaterialDTO);

        Task<IServiceResult> UpdateInternetMaterialAsync(InternetMaterialDTO internetMaterialDTO);
    }
}
