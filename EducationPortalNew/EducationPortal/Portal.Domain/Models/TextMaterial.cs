using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class TextMaterial : Material
    {
        public int NumberOfPages { get; set; }

        public string Format { get; set; }
    }
}
