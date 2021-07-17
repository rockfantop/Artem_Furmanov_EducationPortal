using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.MapperProfiles
{
    public class PagedListProfiles : Profile
    {
        public PagedListProfiles()
        {
            CreateMap<PagedList<CourseSkill>, PagedListDTO<CourseSkillDTO>>()
                .ForMember(dest => dest.Items, opt =>
                    opt.MapFrom(scr => scr.Items)).ReverseMap();

            CreateMap<PagedList<Course>, PagedListDTO<CourseDTO>>()
                .ForMember(dest => dest.Items, opt =>
                    opt.MapFrom(scr => scr.Items)).ReverseMap();

            CreateMap<CourseSkillDTO, CourseSkill>().ReverseMap();
        }
    }
}
