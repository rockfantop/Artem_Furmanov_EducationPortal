using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class CourseCourseSkillDTO
    {
        public Guid CourseId { get; set; }

        public Guid CourseSkillId { get; set; }

        public CourseDTO Course { get; set; }

        public CourseSkillDTO CourseSkill { get; set; }
    }
}
