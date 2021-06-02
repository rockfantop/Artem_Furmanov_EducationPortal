using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class Material : DbEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime DateOfPublication { get; set; }

        public string Content { get; set; }

        public bool IsReaded { get; set; } = false;
    }
}
