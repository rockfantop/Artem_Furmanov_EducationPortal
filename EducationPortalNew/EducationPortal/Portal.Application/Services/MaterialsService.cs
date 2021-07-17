using Portal.Application.Interfaces;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IEfRepository<UserMaterial> userMaterialRepository;

        public MaterialsService(IInternetMaterialService internetMaterialService,
            IVideoMaterialService videoMaterialService,
            ITextMaterialService textMaterialService,
            IEfRepository<UserMaterial> userMaterialRepository)
        {
            this.InternetMaterialService = internetMaterialService;
            this.VideoMaterialService = videoMaterialService;
            this.TextMaterialService = textMaterialService;
            this.userMaterialRepository = userMaterialRepository;
        }

        public IInternetMaterialService InternetMaterialService { get; }

        public IVideoMaterialService VideoMaterialService { get; }

        public ITextMaterialService TextMaterialService { get; }

        public async Task<IServiceResult> CreateStatus(Guid materialId, Guid userId)
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
    }
}
