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
        private readonly IMaterialsService materialsService;

        public CourseService(IEfRepository<Course> repository,
            IEfRepository<CourseCourseSkill> courseCourseSkillRepository,
            IEfRepository<ProgressBar> courseProgressBarRepository,
            IMapper mapper,
            ICourseSkillService courseSkillService,
            IMaterialsService materialsService)
        {
            this.coureRepository = repository;
            this.courseCourseSkillRepository = courseCourseSkillRepository;
            this.courseProgressBarRepository = courseProgressBarRepository;
            this.mapper = mapper;
            this.courseSkillService = courseSkillService;
            this.materialsService = materialsService;
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

        public async Task<IServiceResult<CourseDTO>> GetCourseAsync(Guid id)
        {
            try
            {
                var course = await this.coureRepository.GetWithInclude(CourseSpecification.Id(id), x => x.Materials);

                var courseDTO = this.mapper.Map<CourseDTO>(course);

                return ServiceResult<CourseDTO>.FromResult(true, courseDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<CourseDTO>.FromResult(true, null, "Error");
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

        public async Task<IServiceResult> SubscribeOnCourseAsync(Guid userId, Guid courseId)
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
                    var course = await this.coureRepository.GetWithInclude(CourseSpecification.Id(courseId), x => x.Materials);

                    foreach (var item in course.Materials)
                    {
                        await this.materialsService.CreateStatusAsync(item.Id, userId);
                    }

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

        public async Task<IServiceResult<PagedListDTO<CourseDTO>>> GetSubscribedCoursesAsync(Guid userId, int page)
        {
            try
            {
                var course = await this.coureRepository.GetAsync(CourseSpecification.SubscribedCourseId(userId), page, 10);

                var courseDTO = this.mapper.Map<PagedListDTO<CourseDTO>>(course);

                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(true, courseDTO, "Successful");
            }
            catch (Exception)
            {
                return ServiceResult<PagedListDTO<CourseDTO>>.FromResult(false, null, "Error");
            }
        }

        public async Task<IServiceResult> FinishCourse(Guid id, Guid userId)
        {
            try
            {
                var result = await GetCourseAsync(id);

                var materialList = new List<MaterialDTO>();

                foreach (var item in result.Result.Materials)
                {
                    materialList.Add((await this.materialsService.GetMaterialAsync(item.Id, userId)).Result);
                }

                bool isAllPassed = true;

                foreach (var item in materialList)
                {
                    if (item.IsReaded == false)
                    {
                        isAllPassed = false;
                    }
                }

                if (isAllPassed)
                {
                    var skills = await this.courseSkillService.GetCourseSkillsListAsync(1, 10, id);

                    foreach (var item in skills.Result.Items)
                    {
                        await this.courseSkillService.CreateStatusAsync(item.Id, userId);
                    }
                }

                return ServiceResult.FromResult(true, "Completed");
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
