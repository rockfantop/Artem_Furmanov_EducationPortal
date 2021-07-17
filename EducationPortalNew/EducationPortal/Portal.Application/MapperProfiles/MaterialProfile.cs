﻿using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;

namespace Portal.Application.MapperProfiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialDTO, Material>()
                    .Include<InternetMaterialDTO, InternetMaterial>()
                    .Include<TextMaterialDTO, TextMaterial>()
                    .Include<VideoMaterialDTO, VideoMaterial>()
                    .ReverseMap();

            CreateMap<InternetMaterialDTO, InternetMaterial>().ReverseMap();
            CreateMap<TextMaterialDTO, TextMaterial>().ReverseMap();
            CreateMap<VideoMaterialDTO, VideoMaterial>().ReverseMap();
        }
    }
}
