using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
   public interface ICourseSkillService
    {
        Task<IServiceResult> AddSkillAsync(CourseSkillDTO courseSkillDTO);

        Task<IServiceResult<PagedListDTO<CourseSkillDTO>>> GetListAsync(int pageNumber, int pageSize);

        Task<IServiceResult<CourseSkillDTO>> GetSkill(Guid id);
    }
}
