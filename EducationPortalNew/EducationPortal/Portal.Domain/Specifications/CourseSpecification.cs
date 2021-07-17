using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class CourseSpecification
    {
        public static Specification<Course> Title(string title)
        {
            return new Specification<Course>(x => x.Title == title);
        }

        public static Specification<Course> Owner(Guid id)
        {
            return new Specification<Course>(x => x.OwnerId == id);
        }

        public static Specification<Course> IsPublic(bool flag)
        {
            return new Specification<Course>(x => x.IsPublic == flag);
        }
    }
}
