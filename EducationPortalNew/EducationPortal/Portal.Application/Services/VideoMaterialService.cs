using AutoMapper;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class VideoMaterialService : IVideoMaterialService
    {
        private readonly IEfRepository<VideoMaterial> videoMaterialRepository;
        private readonly IMapper mapper;

        public VideoMaterialService(IEfRepository<VideoMaterial> videoMaterialRepository, IMapper mapper)
        {
            this.videoMaterialRepository = videoMaterialRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddVideoMaterialAsync(VideoMaterialDTO videoMaterialDTO)
        {
            try
            {
                var videoMaterial = this.mapper.Map<VideoMaterial>(videoMaterialDTO);

                await this.videoMaterialRepository.AddAsync(videoMaterial);

                await this.videoMaterialRepository.SaveChanges();

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

                await this.videoMaterialRepository.UpdateAsync(videoMaterial);

                await this.videoMaterialRepository.SaveChanges();

                return ServiceResult.FromResult(true, "Material was updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Material wasn`t updated");
            }
        }
    }
}
