using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
   public interface ICourseSkillService
    {
        Task<IServiceResult<List<CourseSkillDTO>>> ShowAllAsync();

        Task<IServiceResult> AddSkillAsync(CourseSkillDTO courseSkillDTO);
    }
}
