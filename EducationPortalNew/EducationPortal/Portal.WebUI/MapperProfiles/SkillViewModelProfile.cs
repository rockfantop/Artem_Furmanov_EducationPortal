using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.MapperProfiles
{
    public class SkillViewModelProfile : Profile
    {
        public SkillViewModelProfile()
        {
            CreateMap<CourseSkillDTO, SkillViewModel>().ReverseMap();
        }
    }
}
