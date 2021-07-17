using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public abstract class DbEntity
    {
        public Guid Id { get; set; }
    }
}
