using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class CourseCourseSkillSecification
    {
        public static Specification<CourseCourseSkill> CourseId(Guid id)
        {
            return new Specification<CourseCourseSkill>(x => x.CourseId == id);
        }

        public static Specification<CourseCourseSkill> CourseSkillId(Guid id)
        {
            return new Specification<CourseCourseSkill>(x => x.CourseSkillId == id);
        }
    }
}
