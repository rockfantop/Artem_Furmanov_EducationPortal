using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class User : DbEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<UserCourseSkill> UserCourseSkills { get; set; }

        public ICollection<Course> OwnedCourses { get; set; }

        public ICollection<ProgressBar> SubscribedCourses { get; set; }

        public ICollection<UserMaterial> UserMaterials { get; set; }
    }
}
