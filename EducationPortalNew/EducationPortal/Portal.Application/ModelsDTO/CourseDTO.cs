using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; } = false;

        public Guid Owner { get; set; }

        public IEnumerable<MaterialDTO> Materials { get; set; }
    }
}
