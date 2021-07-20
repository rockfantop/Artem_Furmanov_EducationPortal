using Microsoft.AspNetCore.Identity;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Identity
{
    public class Role : IdentityRole<Guid>
    {
    }

    public class User : IdentityUser<Guid>, DbEntity
    {
        public ICollection<UserCourseSkill> UserCourseSkills { get; set; }

        public ICollection<Course> OwnedCourses { get; set; }

        public ICollection<ProgressBar> SubscribedCourses { get; set; }

        public ICollection<UserMaterial> UserMaterials { get; set; }
    }
}
