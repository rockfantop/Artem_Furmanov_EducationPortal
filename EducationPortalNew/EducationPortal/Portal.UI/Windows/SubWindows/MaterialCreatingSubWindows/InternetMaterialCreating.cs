using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.MaterialCreatingSubWindows
{
    public class InternetMaterialCreating
    {
        private readonly IMaterialsService materialsService;

        public InternetMaterialCreating(IMaterialsService materialsService)
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

                    var internetMaterial = new InternetMaterialDTO
                    {
                        Id = Guid.NewGuid(),
                        CourseId = courseDTO.Id,
                        DateOfPublication = DateTime.Now
                    };

                    Console.WriteLine("Internet material Title:\n");

                    internetMaterial.Title = Console.ReadLine();

                    Console.WriteLine("\nInternet material Author:\n");

                    internetMaterial.Author = Console.ReadLine();

                    Console.WriteLine("\nInternet material Source:\n");

                    internetMaterial.Source = Console.ReadLine();

                    Console.WriteLine("\nInternet material Content:\n");

                    internetMaterial.Content = Console.ReadLine();

                    var result = await this.materialsService.InternetMaterialService.AddInternetMaterialAsync(internetMaterial);

                    if (result.IsSuccesful)
                    {
                        var list = (List<MaterialDTO>)courseDTO.Materials;

                        if (list == null)
                        {
                            list = new List<MaterialDTO>();
                        }

                        list.Add(internetMaterial);

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
