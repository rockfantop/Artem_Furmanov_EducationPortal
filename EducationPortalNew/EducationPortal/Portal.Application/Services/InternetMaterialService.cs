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
    public class InternetMaterialService : IInternetMaterialService
    {
        private readonly IAsyncRepository<InternetMaterial> internetMaterialRepository;
        private readonly IMapper mapper;

        public InternetMaterialService(IAsyncRepository<InternetMaterial> internetMaterialRepository, IMapper mapper)
        {
            this.internetMaterialRepository = internetMaterialRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddInternetMaterialAsync(InternetMaterialDTO internetMaterialDTO)
        {
            try
            {
                var internetMaterial = this.mapper.Map<InternetMaterial>(internetMaterialDTO);

                await this.internetMaterialRepository.CreateAsync(internetMaterial);

                return ServiceResult.FromResult(true, "Material was added");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t added");
            }
        }

        public async Task<IServiceResult> UpdateInternetMaterialAsync(InternetMaterialDTO internetMaterialDTO)
        {
            try
            {
                var internetMaterial = this.mapper.Map<InternetMaterial>(internetMaterialDTO);

                await this.internetMaterialRepository.UpdateAsync(internetMaterial);

                return ServiceResult.FromResult(true, "Material was updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t updated");
            }
        }
    }
}
