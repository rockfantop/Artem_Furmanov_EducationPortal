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
    public class CourseService : ICourseService
    {
        private readonly IAsyncRepository<Course> coureRepository;
        private readonly IMapper mapper;

        public CourseService(IAsyncRepository<Course> repository, IMapper mapper)
        {
            this.coureRepository = repository;
            this.mapper = mapper;
        }

        public Task<IServiceResult> AddMaterialAsync(CourseDTO courseDTO, MaterialDTO materialDTO)
        {
            var material = this.mapper.Map<Material>(materialDTO);

            var course = this.mapper.Map<Course>(courseDTO);

            throw new NotImplementedException();
        }

        public async Task<IServiceResult> CreateCourseAsync(EmptyCourseDTO emptyCourseDTO)
        {
            try
            {
                var course = this.mapper.Map<Course>(emptyCourseDTO);

                var checkCourseName = await this.coureRepository.GetEntityAsync(x => x.Title == course.Title);

                if (checkCourseName == null)
                {
                    await this.coureRepository.CreateAsync(course);

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

                var checkCourseName = this.coureRepository.GetEntityAsync(x => x.Title == course.Title);

                if (checkCourseName == null)
                {
                    await this.coureRepository.CreateAsync(course);

                    return ServiceResult.FromResult(true, "Successful Created");
                }

                return ServiceResult.FromResult(false, "Course with this name is already exist");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Failed Created");
            }
        }

        public async Task<IServiceResult<List<CourseDTO>>> GetAllPublicAsync()
        {
            try
            {
                var enumeration = await this.coureRepository.GetAllEntitiesAsync(x => x.IsPublic == true);

                var list = (List<Course>)enumeration;

                var listDTO = this.mapper.Map<List<CourseDTO>>(list);

                return ServiceResult<List<CourseDTO>>.FromResult(true, listDTO, "List of Public Courses");
            }
            catch (Exception)
            {
                return ServiceResult<List<CourseDTO>>.FromResult(false, null, "List of Public Courses");
            }
        }

        public async Task<IServiceResult> UpdateCourseAsync(CourseDTO courseDTO)
        {
            try
            {
                var course = this.mapper.Map<Course>(courseDTO);

                await this.coureRepository.UpdateAsync(course);

                return ServiceResult.FromResult(true, "Course updated");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Course updated failed");
            }
        }
    }
}
