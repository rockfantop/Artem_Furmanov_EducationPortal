using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.MapperProfiles
{
    public class CourseSkillProfile : Profile
    {
        public CourseSkillProfile()
        {
            CreateMap<CourseSkillDTO, CourseSkill>().ReverseMap();
        }
    }
}
