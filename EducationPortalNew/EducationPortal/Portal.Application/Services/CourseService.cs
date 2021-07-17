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
    public class CourseService : ICourseService
    {
        private readonly IEfRepository<Course> coureRepository;
        private readonly IEfRepository<CourseCourseSkill> courseCourseSkillRepository;
        private readonly IEfRepository<ProgressBar> courseProgressBarRepository;
        private readonly IMapper mapper;
        private readonly ICourseSkillService courseSkillService;

        public CourseService(IEfRepository<Course> repository,
            IEfRepository<CourseCourseSkill> courseCourseSkillRepository,
            IEfRepository<ProgressBar> courseProgressBarRepository,
            IMapper mapper,
            ICourseSkillService courseSkillService)
        {
            this.coureRepository = repository;
            this.courseCourseSkillRepository = courseCourseSkillRepository;
            this.courseProgressBarRepository = courseProgressBarRepository;
            this.mapper = mapper;
            this.courseSkillService = courseSkillService;
        }

        public async Task<IServiceResult> AddCourseSkillToCourseAsync(Guid courseSkillId, Guid courseId)
        {
            try
            {
                var relation = new CourseCourseSkill
                {
                    CourseSkillId = courseSkillId,
                    CourseId = courseId
                };

                if ((await this.courseCourseSkillRepository.FindAsync(CourseCourseSkillSecification.CourseId(courseId)
                    .And(CourseCourseSkillSecification.CourseSkillId(courseSkillId)))) == null)
                {
                    await this.courseCourseSkillRepository.AddAsync(relation);

                    await this.courseCourseSkillRepository.SaveChanges();

                    return ServiceResult.FromResult(true, "Successful");
                }
                else
                {
                    return ServiceResult.FromResult(false, "Error");
                }
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Error");
            }
        }

        public Task<IServiceResult> AddMaterialAsync(CourseDTO courseDTO, MaterialDTO materialDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult> CreateCourseAsync(EmptyCourseDTO emptyCourseDTO)
        {
            try
            {
                var course = this.mapper.Map<Course>(emptyCourseDTO);

                var checkCourseName = await this.coureRepository.FindAsync(CourseSpecification.Title(emptyCourseDTO.Title));

                if (checkCourseName == null)
                {
                    await this.coureRepository.AddAsync(course);

                    await this.coureRepository.SaveChanges();

                    return ServiceResult.FromResult(true, "Successful Created");
                }

                return ServiceResult.FromResult(false, "Course with this name is already exist");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Failed Created");
            }
        }

        public async Task<IServiceResult> CreateCourseAsync(CourseDTO courseDTO)
        {
            try
            {
                var course = this.mapper.Map<Course>(courseDTO);

                var checkCourseName = await this.coureRepository.FindAsync(CourseSpecification.Title(courseDTO.Title));

                if (checkCourseName == null)
                {
                    await this.coureRepository.AddAsync(course);

                    await this.coureRepository.SaveChanges();

                    return ServiceResult.FromResult(true, "Successful Created");
                }

                return ServiceResult.FromResult(false, "Course with this name is already exist");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Failed Created");
            }
        }

        public async Task<IServiceResult<PagedListDTO<CourseDTO>>> GetPublicListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var list = await this.coureRepository.GetListWithInclude(CourseSpecification.IsPublic(true),
                    pageNumber, pageSize, default, x => x.Materials, x => x.CourseCourseSkills);

                var listDTO = this.mapper.Map<PagedListDTO<CourseDTO>>(list);

                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(true, listDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(true, null, "Error");
            }
        }

        public async Task<IServiceResult<PagedListDTO<CourseDTO>>> GetUserOwnedCourseListAsync(Guid owner, int pageNumber, int pageSize)
        {
            try
            {
                var list = await this.coureRepository.GetAsync(CourseSpecification.Owner(owner), pageNumber, pageSize);

                ((List<Course>)list.Items).ForEach(x => 
                { 
                    x.CourseCourseSkills = null;
                    x.ProgressBars = null;
                    x.Materials = null;
                } 
                );

                var listDTO = this.mapper.Map<PagedListDTO<CourseDTO>>(list);

                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(true, listDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(true, null, "Error");
            }
        }

        public async Task<IServiceResult> SubscribeOnCourse(Guid userId, Guid courseId)
        {
            try
            {
                var relation = new ProgressBar
                {
                    UserId = userId,
                    CourseId = courseId
                };

                if ((await this.courseProgressBarRepository.FindAsync(ProgressBarSecification.CourseId(courseId)
                    .And(ProgressBarSecification.UserId(userId)))) == null)
                {
                    await this.courseProgressBarRepository.AddAsync(relation);

                    await this.courseProgressBarRepository.SaveChanges();

                    return ServiceResult.FromResult(true, "Successful");
                }
                else
                {
                    return ServiceResult.FromResult(false, "Error");
                }
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Error");
            }
        }

        public async Task<IServiceResult> UpdateCourseAsync(CourseDTO courseDTO)
        {
            try
            {
                var course = this.mapper.Map<Course>(courseDTO);

                await this.coureRepository.UpdateAsync(course);

                await this.coureRepository.SaveChanges();

                return ServiceResult.FromResult(true, "Course updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Course updated failed");
            }
        }
    }
}
