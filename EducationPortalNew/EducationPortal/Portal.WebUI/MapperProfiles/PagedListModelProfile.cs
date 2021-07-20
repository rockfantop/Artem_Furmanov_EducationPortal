using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.WebUI.Models;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.MapperProfiles
{
    public class PagedListModelProfile : Profile
    {
        public PagedListModelProfile()
        {
            CreateMap<PagedListModel<SkillViewModel>, PagedListDTO<CourseSkillDTO>>()
                .ForMember(dest => dest.Items, opt =>
                    opt.MapFrom(scr => scr.Items)).ReverseMap();

            CreateMap<PagedListModel<CourseViewModel>, PagedListDTO<CourseDTO>>()
                .ForMember(dest => dest.Items, opt =>
                    opt.MapFrom(scr => scr.Items)).ReverseMap();

            CreateMap<CourseSkillDTO, SkillViewModel>().ReverseMap();
        }
    }
}
