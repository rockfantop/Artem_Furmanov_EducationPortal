using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class Material : DbEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime DateOfPublication { get; set; }

        public string Content { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<UserMaterial> UserMaterials { get; set; }
    }
}
