using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> coureRepository;

        public CourseService(IRepository<Course> repository)
        {
            this.coureRepository = repository;
        }

        public IServiceResult AddMaterial(Course course, Material material)
        {
            throw new NotImplementedException();
        }

        public IServiceResult CreateCourse(EmptyCourseDTO emptyCourse)
        {
            try
            {
                Course course = new Course
                {
                    Id = Guid.NewGuid(),
                    Title = emptyCourse.Title,
                    Owner = emptyCourse.Owner,
                    Description = emptyCourse.Description
                };

                this.coureRepository.Create(course);

                return ServiceResult.FromResult(true, "Successful Created");
            }
            catch (Exception)
            {
                return ServiceResult.FromResult(false, "Failed Created");
            }
        }

        public IServiceResult SaveChanges()
        {
            throw new NotImplementedException();
        }

        public IServiceResult UpdateCourse()
        {
            throw new NotImplementedException();
        }
    }
}
