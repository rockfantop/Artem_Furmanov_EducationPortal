using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface IMaterialsService
    {
        IInternetMaterialService InternetMaterialService { get; }

        IVideoMaterialService VideoMaterialService { get; }

        ITextMaterialService TextMaterialService { get; }

        Task<IServiceResult> CreateStatusAsync(Guid materialId, Guid userId);

        Task<IServiceResult> PassMaterialAsync(Guid materialId, Guid userId);

        Task<IServiceResult<MaterialDTO>> GetMaterialAsync(Guid materialId, Guid userId);
    }
}
