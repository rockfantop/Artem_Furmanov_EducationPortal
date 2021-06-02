using AutoMapper;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IAsyncRepository<CourseSkill> courseSkillRepository;
        private readonly IMapper mapper;

        public CourseSkillService(IAsyncRepository<CourseSkill> courseSkillRepository, IMapper mapper)
        {
            this.courseSkillRepository = courseSkillRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddSkillAsync(CourseSkillDTO courseSkillDTO)
        {
            try
            {
                var courseSkill = this.mapper.Map<CourseSkill>(courseSkillDTO);

                await this.courseSkillRepository.CreateAsync(courseSkill);

                return ServiceResult.FromResult(true, "Skill was added");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Skill wasn`t added");
            }
        }

        public async Task<IServiceResult<List<CourseSkillDTO>>> ShowAllAsync()
        {
            var enumeration = await this.courseSkillRepository.GetAllEntitiesAsync(x => x == x);

            var courseSkills = (List<CourseSkill>)enumeration;

            var courseSkillsDTO = this.mapper.Map<List<CourseSkillDTO>>(courseSkills);

            if (courseSkills != null)
            {
                return ServiceResult<List<CourseSkillDTO>>.FromResult(true, courseSkillsDTO, "All skills");
            }

            return ServiceResult<List<CourseSkillDTO>>.FromResult(false, null, "0 skills");
        }
    }
}
