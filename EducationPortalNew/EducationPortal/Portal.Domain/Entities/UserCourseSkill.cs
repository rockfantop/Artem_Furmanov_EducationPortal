using Portal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class UserCourseSkill
    {
        public Guid CourseSkillId { get; set; }

        public Guid UserId { get; set; }

        public int SkillLevel { get; set; }

        public User User { get; set; }

        public CourseSkill CourseSkill { get; set; }
    }
}
