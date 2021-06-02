using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.MaterialCreatingSubWindows
{
    public class TextMaterialCreating
    {
        private readonly IMaterialsService materialsService;

        public TextMaterialCreating(IMaterialsService materialsService)
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

                    var textMaterial = new TextMaterialDTO
                    {
                        Id = Guid.NewGuid(),
                        Format = "pdf",
                        DateOfPublication = DateTime.Now
                    };

                    Console.WriteLine("Text material Title:\n");

                    textMaterial.Title = Console.ReadLine();

                    Console.WriteLine("\nText material Author:\n");

                    textMaterial.Author = Console.ReadLine();

                    Console.WriteLine("\nText material NumberOfPages:\n");

                    textMaterial.NumberOfPages = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nText material Content:\n");

                    textMaterial.Content = Console.ReadLine();

                    var result = await this.materialsService.TextMaterialService.AddTextMaterialAsync(textMaterial);

                    if (result.IsSuccesful)
                    {
                        var list = (List<MaterialDTO>)courseDTO.Materials;

                        if (list == null)
                        {
                            list = new List<MaterialDTO>();
                        }

                        list.Add(textMaterial);

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
