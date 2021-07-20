using Portal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class Course : DbEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; } = false;

        public Guid? OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<CourseCourseSkill> CourseCourseSkills { get; set; }

        public ICollection<ProgressBar> ProgressBars { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
