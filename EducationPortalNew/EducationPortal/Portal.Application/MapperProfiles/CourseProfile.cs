using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Application.MapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDTO, Course>()
                .ForMember(dest => dest.Materials, opt =>
                    opt.MapFrom(scr => scr.Materials))
                .ForMember(dest => dest.CourseSkills, opt =>
                    opt.MapFrom(scr => scr.CourseSkills)).ReverseMap();

            CreateMap<EmptyCourseDTO, Course>().ReverseMap();

            CreateMap<MaterialDTO, Material>()
                .Include<InternetMaterialDTO, InternetMaterial>()
                .Include<TextMaterialDTO, TextMaterial>()
                .Include<VideoMaterialDTO, VideoMaterial>()
                .ReverseMap();

            CreateMap<CourseSkillDTO, CourseSkill>().ReverseMap();

            CreateMap<InternetMaterialDTO, InternetMaterial>().ReverseMap();
            CreateMap<TextMaterialDTO, TextMaterial>().ReverseMap();
            CreateMap<VideoMaterialDTO, VideoMaterial>().ReverseMap();
        }
    }
}
