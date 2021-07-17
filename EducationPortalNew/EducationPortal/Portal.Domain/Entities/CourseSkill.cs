using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class CourseSkill : DbEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<UserCourseSkill> UserCourseSkills { get; set; }

        public ICollection<CourseCourseSkill> CourseCourseSkills { get; set; }
    }
}
