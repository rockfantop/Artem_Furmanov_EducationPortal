using AutoMapper;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IEfRepository<UserMaterial> userMaterialRepository;
        private readonly IEfRepository<Material> materialRepository;
        private readonly IMapper mapper;

        public MaterialsService(IInternetMaterialService internetMaterialService,
            IVideoMaterialService videoMaterialService,
            ITextMaterialService textMaterialService,
            IEfRepository<UserMaterial> userMaterialRepository,
            IEfRepository<Material> materialRepository,
            IMapper mapper)
        {
            this.InternetMaterialService = internetMaterialService;
            this.VideoMaterialService = videoMaterialService;
            this.TextMaterialService = textMaterialService;
            this.userMaterialRepository = userMaterialRepository;
            this.materialRepository = materialRepository;
            this.mapper = mapper;
        }

        public IInternetMaterialService InternetMaterialService { get; }

        public IVideoMaterialService VideoMaterialService { get; }

        public ITextMaterialService TextMaterialService { get; }

        public async Task<IServiceResult> CreateStatusAsync(Guid materialId, Guid userId)
        {
            try
            {
                var relation = new UserMaterial
                {
                    UserId = userId,
                    MaterialId = materialId
                };

                await this.userMaterialRepository.AddAsync(relation);

                await this.userMaterialRepository.SaveChanges();

                return ServiceResult.FromResult(true, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Error");
            }
        }

        public async Task<IServiceResult<MaterialDTO>> GetMaterialAsync(Guid materialId, Guid userId)
        {
            try
            {
                var material = await this.materialRepository.GetWithInclude(MaterialSpecification.UserId(userId)
                    .And(MaterialSpecification.MaterialId(materialId)), x => x.UserMaterials);

                var materialDTO = this.mapper.Map<MaterialDTO>(material);
                materialDTO.IsReaded = ((HashSet<UserMaterial>)material.UserMaterials).First().IsPassed;

                return ServiceResult<MaterialDTO>.FromResult(true, materialDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<MaterialDTO>.FromResult(true, null, "Error");
            }
        }

        public async Task<IServiceResult> PassMaterialAsync(Guid materialId, Guid userId)
        {
            try
            {
                var userMaterial = await this.userMaterialRepository.FindAsync(UserMaterialSpecification.MaterialId(materialId)
                    .And(UserMaterialSpecification.UserId(userId)));

                userMaterial.IsPassed = true;

                await this.userMaterialRepository.UpdateAsync(userMaterial);

                await this.userMaterialRepository.SaveChanges();

                return ServiceResult.FromResult(true, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Error");
            }
        }
    }
}
