using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class EmptyCourseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid Owner { get; set; }
    }
}
