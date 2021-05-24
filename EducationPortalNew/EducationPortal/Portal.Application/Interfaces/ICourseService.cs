using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface ICourseService
    {
        IServiceResult CreateCourse(EmptyCourseDTO emptyCourse);

        IServiceResult AddMaterial(Course course, Material material);

        IServiceResult UpdateCourse();

        IServiceResult SaveChanges();
    }
}
