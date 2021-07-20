using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.ViewModels
{
    public class VideoMaterialViewModel : MaterialViewModel
    {
        public TimeSpan Duration { get; set; }

        public int Quality { get; set; }
    }
}
