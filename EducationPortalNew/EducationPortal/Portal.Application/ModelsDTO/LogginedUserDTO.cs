using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class LogginedUserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<CourseSkillDTO> Skills { get; set; }

        public IEnumerable<CourseDTO> OwnedCourses { get; set; }

        public IEnumerable<CourseDTO> SubscribedCourses { get; set; }
    }
}
