using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.MapperProfiles
{
    public class MaterialViewModelProfile : Profile
    {
        public MaterialViewModelProfile()
        {
            CreateMap<MaterialDTO, MaterialViewModel>()
                    .Include<InternetMaterialDTO, InternetMaterialViewModel>()
                    .Include<TextMaterialDTO, TextMaterialViewModel>()
                    .Include<VideoMaterialDTO, VideoMaterialViewModel>()
                    .ReverseMap();

            CreateMap<InternetMaterialDTO, InternetMaterialViewModel>().ReverseMap();
            CreateMap<TextMaterialDTO, TextMaterialViewModel>().ReverseMap();
            CreateMap<VideoMaterialDTO, VideoMaterialViewModel>().ReverseMap();
        }
    }
}
