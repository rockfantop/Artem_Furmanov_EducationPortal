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
    public class VideoMaterialService : IVideoMaterialService
    {
        private readonly IAsyncRepository<VideoMaterial> internetMaterialRepository;
        private readonly IMapper mapper;

        public VideoMaterialService(IAsyncRepository<VideoMaterial> internetMaterialRepository, IMapper mapper)
        {
            this.internetMaterialRepository = internetMaterialRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddVideoMaterialAsync(VideoMaterialDTO videoMaterialDTO)
        {
            try
            {
                var videoMaterial = this.mapper.Map<VideoMaterial>(videoMaterialDTO);

                await this.internetMaterialRepository.CreateAsync(videoMaterial);

                return ServiceResult.FromResult(true, "Material was added");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t added");
            }
        }

        public async Task<IServiceResult> UpdateVideoMaterialAsync(VideoMaterialDTO videoMaterialDTO)
        {
            try
            {
                var videoMaterial = this.mapper.Map<VideoMaterial>(videoMaterialDTO);

                await this.internetMaterialRepository.UpdateAsync(videoMaterial);

                return ServiceResult.FromResult(true, "Material was updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t updated");
            }
        }
    }
}
