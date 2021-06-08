using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface IVideoMaterialService
    {
        Task<IServiceResult> AddVideoMaterialAsync(VideoMaterialDTO videoMaterialDTO);

        Task<IServiceResult> UpdateVideoMaterialAsync(VideoMaterialDTO videoMaterialDTO);
    }
}
