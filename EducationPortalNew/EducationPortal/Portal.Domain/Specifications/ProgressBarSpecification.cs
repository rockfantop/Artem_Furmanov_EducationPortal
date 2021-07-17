using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class ProgressBarSecification
    {
        public static Specification<ProgressBar> CourseId(Guid id)
        {
            return new Specification<ProgressBar>(x => x.CourseId == id);
        }

        public static Specification<ProgressBar> UserId(Guid id)
        {
            return new Specification<ProgressBar>(x => x.UserId == id);
        }
    }
}
