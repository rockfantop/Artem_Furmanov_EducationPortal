using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.MaterialCreatingSubWindows
{
    public class VideoMaterialCreating
    {
        private readonly IMaterialsService materialsService;

        public VideoMaterialCreating(IMaterialsService materialsService)
        {
            this.materialsService = materialsService;
        }

        public async Task<CourseDTO> CreateAsync(CourseDTO courseDTO)
        {
            while (true)
            {
                try
                {
                    Console.Clear();

                    var videoMaterial = new VideoMaterialDTO
                    {
                        Id = Guid.NewGuid(),
                        Quality = 720,
                        Duration = DateTime.Today.TimeOfDay,
                        DateOfPublication = DateTime.Now
                    };

                    Console.WriteLine("Video material Title:\n");

                    videoMaterial.Title = Console.ReadLine();

                    Console.WriteLine("\nVideo material Author:\n");

                    videoMaterial.Author = Console.ReadLine();

                    Console.WriteLine("\nVideo material Content:\n");

                    videoMaterial.Content = Console.ReadLine();

                    var result = await this.materialsService.VideoMaterialService.AddVideoMaterialAsync(videoMaterial);

                    if (result.IsSuccesful)
                    {
                        var list = (List<MaterialDTO>)courseDTO.Materials;

                        if (list == null)
                        {
                            list = new List<MaterialDTO>();
                        }

                        list.Add(videoMaterial);

                        courseDTO.Materials = list;

                        return courseDTO;
                    }
                    else
                    {
                        return courseDTO;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
