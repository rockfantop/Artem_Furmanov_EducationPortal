using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class VideoMaterial : Material
    {
        public TimeSpan Duration { get; set; }

        public int Quality { get; set; }
    }
}
