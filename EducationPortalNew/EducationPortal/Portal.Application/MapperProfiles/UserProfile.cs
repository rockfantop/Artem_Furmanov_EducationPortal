﻿using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Identity;

namespace Portal.Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LogginedUserDTO, User>()
                .ForMember(dest => dest.OwnedCourses, opt =>
                    opt.MapFrom(scr => scr.OwnedCourses))
                .ForMember(dest => dest.SubscribedCourses, opt =>
                    opt.MapFrom(scr => scr.SubscribedCourses))
                .ForMember(dest => dest.UserCourseSkills, opt =>
                    opt.MapFrom(scr => scr.Skills)).ReverseMap();


            CreateMap<CourseDTO, Course>()
                .ForMember(dest => dest.Materials, opt =>
                    opt.MapFrom(scr => scr.Materials))
                .ForMember(dest => dest.CourseCourseSkills, opt =>
                    opt.MapFrom(scr => scr.CourseSkills)).ReverseMap();

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
