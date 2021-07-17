using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class TextMaterial : Material
    {
        public int NumberOfPages { get; set; }

        public string Format { get; set; }
    }
}
