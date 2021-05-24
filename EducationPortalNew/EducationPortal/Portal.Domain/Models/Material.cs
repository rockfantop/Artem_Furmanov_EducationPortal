using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public abstract class Material : DbEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
