using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IServiceResult> CreateCourseAsync(EmptyCourseDTO emptyCourseDTO);

        Task<IServiceResult> CreateCourseAsync(CourseDTO courseDTO);

        Task<IServiceResult> AddMaterialAsync(CourseDTO courseDTO, MaterialDTO materialDTO);

        Task<IServiceResult> UpdateCourseAsync(CourseDTO courseDTO);

        Task<IServiceResult<List<CourseDTO>>> GetAllPublicAsync();
    }
}
