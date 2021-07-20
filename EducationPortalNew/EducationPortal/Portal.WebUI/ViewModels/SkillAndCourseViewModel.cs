using Portal.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.ViewModels
{
    public class SkillAndCourseViewModel
    {
        public CourseViewModel Course { get; set; }

        public PagedListModel<SkillViewModel> Skills { get; set; }
    }
}
