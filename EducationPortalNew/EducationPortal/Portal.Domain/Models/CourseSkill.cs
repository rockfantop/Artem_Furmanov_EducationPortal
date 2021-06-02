using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class CourseSkill : DbEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SkillLevel { get; set; }
    }
}
