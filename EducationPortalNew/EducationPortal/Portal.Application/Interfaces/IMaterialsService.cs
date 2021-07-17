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

        Task<IServiceResult> CreateStatus(Guid materialId, Guid userId);
    }
}
