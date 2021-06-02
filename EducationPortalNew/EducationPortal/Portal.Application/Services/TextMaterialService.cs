using AutoMapper;
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
    public class TextMaterialService : ITextMaterialService
    {
        private readonly IAsyncRepository<TextMaterial> internetMaterialRepository;
        private readonly IMapper mapper;

        public TextMaterialService(IAsyncRepository<TextMaterial> internetMaterialRepository, IMapper mapper)
        {
            this.internetMaterialRepository = internetMaterialRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddTextMaterialAsync(TextMaterialDTO textMaterialDTO)
        {
            try
            {
                var textMaterial = this.mapper.Map<TextMaterial>(textMaterialDTO);

                await this.internetMaterialRepository.CreateAsync(textMaterial);

                return ServiceResult.FromResult(true, "Material was added");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t added");
            }
        }

        public async Task<IServiceResult> UpdateTextMaterialAsync(TextMaterialDTO textMaterialDTO)
        {
            try
            {
                var textMaterial = this.mapper.Map<TextMaterial>(textMaterialDTO);

                await this.internetMaterialRepository.UpdateAsync(textMaterial);

                return ServiceResult.FromResult(true, "Material was updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t updated");
            }
        }
    }
}
