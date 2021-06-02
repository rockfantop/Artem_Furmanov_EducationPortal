using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class TextMaterialDTO : MaterialDTO
    {
        public int NumberOfPages { get; set; }

        public string Format { get; set; }
    }
}
