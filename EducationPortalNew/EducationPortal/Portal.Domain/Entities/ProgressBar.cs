using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class ProgressBar
    {
        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }

        public int Progress { get; set; }

        public User User { get; set; }

        public Course Course { get; set; }
    }
}
