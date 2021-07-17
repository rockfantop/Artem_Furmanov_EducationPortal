using AutoMapper;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IEfRepository<CourseSkill> courseSkillRepository;
        private readonly IMapper mapper;

        public CourseSkillService(IEfRepository<CourseSkill> courseSkillRepository, IMapper mapper)
        {
            this.courseSkillRepository = courseSkillRepository;
            this.mapper = mapper;
        }

        public async Task<IServiceResult> AddSkillAsync(CourseSkillDTO courseSkillDTO)
        {
            try
            {
                if ((await this.courseSkillRepository.FindAsync(CourseSkillSpecification.Title(courseSkillDTO.Title)) == null))
                {
                    var courseSkill = this.mapper.Map<CourseSkill>(courseSkillDTO);

                    await this.courseSkillRepository.AddAsync(courseSkill);

                    await this.courseSkillRepository.SaveChanges();

                    return ServiceResult.FromResult(true, "Skill was added");
                }
                else
                {
                    return ServiceResult.FromResult(false, "Skill wasn`t added");
                }
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Skill wasn`t added");
            }
        }

        public async Task<IServiceResult<PagedListDTO<CourseSkillDTO>>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var list = await this.courseSkillRepository.GetAsync(pageNumber, pageSize);

                var listDTO = this.mapper.Map<PagedListDTO<CourseSkillDTO>>(list);

                return ServiceResult<PagedListDTO<CourseSkillDTO>>.FromResult(true, listDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<PagedListDTO<CourseSkillDTO>>.FromResult(true, null, "Error");
            }
        }

        public async Task<IServiceResult<CourseSkillDTO>> GetSkill(Guid id)
        {
            try
            {
                var item = await this.courseSkillRepository.FindAsync(CourseSkillSpecification.Id(id));

                return ServiceResult<CourseSkillDTO>.FromResult(true, this.mapper.Map<CourseSkillDTO>(item), "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<CourseSkillDTO>.FromResult(true, null, "Error");
            }
        }
    }
}
