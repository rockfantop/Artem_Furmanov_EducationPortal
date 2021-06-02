using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.ModelsDTO
{
    public class VideoMaterialDTO : MaterialDTO
    {
        public TimeSpan Duration { get; set; }

        public int Quality { get; set; }
    }
}
