using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; } = false;

        public Guid OwnerId { get; set; }

        public ICollection<MaterialViewModel> Materials { get; set; }
    }
}
