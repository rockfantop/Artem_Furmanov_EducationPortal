using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LogginedUserDTO, User>().ForMember(dest => dest.OwnedCourses, opt =>
                    opt.MapFrom(scr => scr.OwnedCourses)).ReverseMap();


            CreateMap<CourseDTO, Course>()
                .ForMember(dest => dest.Materials, opt =>
                    opt.MapFrom(scr => scr.Materials)).ReverseMap();

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
