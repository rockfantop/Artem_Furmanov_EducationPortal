using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        public MaterialsService(IInternetMaterialService internetMaterialService,
            IVideoMaterialService videoMaterialService,
            ITextMaterialService textMaterialService)
        {
            this.InternetMaterialService = internetMaterialService;
            this.VideoMaterialService = videoMaterialService;
            this.TextMaterialService = textMaterialService;
        }

        public IInternetMaterialService InternetMaterialService { get; }

        public IVideoMaterialService VideoMaterialService { get; }

        public ITextMaterialService TextMaterialService { get; }
    }
}
