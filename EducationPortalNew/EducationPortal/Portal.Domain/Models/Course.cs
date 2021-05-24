using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class Course : DbEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid Owner { get; set; }

        public IEnumerable<Material> Materials { get; set; }
    }
}
