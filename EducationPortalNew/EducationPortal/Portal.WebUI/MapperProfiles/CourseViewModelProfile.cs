using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.MapperProfiles
{
    public class CourseViewModelProfile : Profile
    {
        public CourseViewModelProfile()
        {
            CreateMap<CourseDTO, CourseViewModel>()
                .ForMember(dest => dest.Materials, opt =>
                    opt.MapFrom(scr => scr.Materials));
                //.ForMember(dest => dest.CourseCourseSkills, opt =>
                //    opt.MapFrom(scr => scr.CourseSkills)).ReverseMap();

            CreateMap<EmptyCourseDTO, CourseCreateViewModel>().ReverseMap();

            //CreateMap<CourseCourseSkill, CourseCourseSkillDTO>().ReverseMap();

            CreateMap<MaterialDTO, MaterialViewModel>()
                .Include<InternetMaterialDTO, InternetMaterialViewModel>()
                .Include<TextMaterialDTO, TextMaterialViewModel>()
                .Include<VideoMaterialDTO, VideoMaterialViewModel>()
                .ReverseMap();

            CreateMap<CourseSkillDTO, SkillViewModel>().ReverseMap();

            CreateMap<InternetMaterialDTO, InternetMaterialViewModel>().ReverseMap();
            CreateMap<TextMaterialDTO, TextMaterialViewModel>().ReverseMap();
            CreateMap<VideoMaterialDTO, VideoMaterialViewModel>().ReverseMap();
        }
    }
}
