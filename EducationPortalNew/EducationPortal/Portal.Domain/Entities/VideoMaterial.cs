using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Entities
{
    public class VideoMaterial : Material
    {
        public TimeSpan Duration { get; set; }

        public int Quality { get; set; }
    }
}
