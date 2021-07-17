using AutoMapper;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class TextMaterialService : ITextMaterialService
    {
        private readonly IEfRepository<TextMaterial> textMaterialRepository;
        private readonly IMapper mapper;

        public TextMaterialService(IEfRepository<TextMaterial> textMaterialRepository, IMapper mapper)
        {
            this.textMaterialRepository = textMaterialRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddTextMaterialAsync(TextMaterialDTO textMaterialDTO)
        {
            try
            {
                var textMaterial = this.mapper.Map<TextMaterial>(textMaterialDTO);

                await this.textMaterialRepository.AddAsync(textMaterial);

                await this.textMaterialRepository.SaveChanges();

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

                await this.textMaterialRepository.UpdateAsync(textMaterial);

                await this.textMaterialRepository.SaveChanges();

                return ServiceResult.FromResult(true, "Material was updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t updated");
            }
        }
    }
}
