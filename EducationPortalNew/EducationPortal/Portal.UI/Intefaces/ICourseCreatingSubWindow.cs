using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Intefaces
{
    public interface ICourseCreatingSubWindow
    {
        Task<CourseDTO> ShowCreatingSubWindow(CourseDTO courseDTO);

        Task<CourseDTO> Finish(CourseDTO courseDTO);
    }
}
