using AutoMapper;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;

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
