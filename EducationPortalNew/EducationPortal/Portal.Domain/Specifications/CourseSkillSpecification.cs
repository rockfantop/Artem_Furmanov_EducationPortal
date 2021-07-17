using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class CourseSkillSpecification
    {
        public static Specification<CourseSkill> Title(string title)
        {
            return new Specification<CourseSkill>(x => x.Title == title);
        }

        public static Specification<CourseSkill> Id(Guid id)
        {
            return new Specification<CourseSkill>(x => x.Id == id);
        }
    }
}
