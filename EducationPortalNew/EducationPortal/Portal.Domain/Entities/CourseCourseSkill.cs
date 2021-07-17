using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class CourseCourseSkill
    {
        public Guid CourseId { get; set; }

        public Guid CourseSkillId { get; set; }

        public Course Course { get; set; }

        public CourseSkill CourseSkill { get; set; }
    }
}
