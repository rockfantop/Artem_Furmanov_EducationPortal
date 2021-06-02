using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI
{
    public class InSystemUser
    {
        private static InSystemUser instance;

        private InSystemUser() { }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<CourseSkillDTO> Skills { get; set; }

        public IEnumerable<CourseDTO> OwnedCourses { get; set; }

        public IEnumerable<CourseDTO> SubscribedCourses { get; set; }

        public static InSystemUser GetInstance()
        {
            if (instance == null)
            {
                instance = new InSystemUser();
            }

            return instance;
        }
    }
}
