using Portal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    [Serializable]
    public class User : DbEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
