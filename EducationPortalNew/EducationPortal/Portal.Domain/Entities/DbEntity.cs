using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public interface DbEntity
    {
        public Guid Id { get; set; }
    }
}
