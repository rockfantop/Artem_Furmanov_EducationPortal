using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface ITextMaterialService
    {
        Task<IServiceResult> AddTextMaterialAsync(TextMaterialDTO textMaterialDTO);

        Task<IServiceResult> UpdateTextMaterialAsync(TextMaterialDTO textMaterialDTO);
    }
}
