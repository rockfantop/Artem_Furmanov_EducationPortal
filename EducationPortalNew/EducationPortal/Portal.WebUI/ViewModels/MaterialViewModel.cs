using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.ViewModels
{
    public class MaterialViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public Guid CourseId { get; set; }

        public DateTime DateOfPublication { get; set; }

        public string Content { get; set; }

        public bool IsReaded { get; set; } = false;
    }
}
