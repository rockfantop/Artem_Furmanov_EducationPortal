using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class UserMaterial
    {
        public Guid MaterialId { get; set; }

        public Guid UserId { get; set; }

        public bool IsPassed { get; set; }

        public User User { get; set; }

        public Material Material { get; set; }
    }
}
