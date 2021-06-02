using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using Portal.UI.Windows.SubWindows.MaterialCreatingSubWindows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.CreatingCourseSubWindows
{
    public class Materials : IMaterialsCreatingSubWindow
    {
        private readonly ICourseService courseService;
        private readonly InternetMaterialCreating internetMaterialCreating;
        private readonly VideoMaterialCreating videoMaterialCreating;
        private readonly TextMaterialCreating textMaterialCreating;

        public Materials(ICourseService courseService,
            InternetMaterialCreating internetMaterialCreating,
            VideoMaterialCreating videoMaterialCreating,
            TextMaterialCreating textMaterialCreating)
        {
            this.courseService = courseService;
            this.internetMaterialCreating = internetMaterialCreating;
            this.videoMaterialCreating = videoMaterialCreating;
            this.textMaterialCreating = textMaterialCreating;
        }

        private enum Commands
        {
            AddAnotherMaterial = 1,
            Finish = 2
        }

        private enum MaterialTypes
        {
            Video = 1,
            Text = 2,
            Internet = 3
        }

        public async Task<CourseDTO> Finish(CourseDTO courseDTO)
        {
            courseDTO.IsPublic = true;

            await this.courseService.UpdateCourseAsync(courseDTO);

            return await Task.Factory.StartNew(() =>
            {
                return courseDTO;
            });
        }

        public async Task<CourseDTO> ShowCreatingSubWindow(CourseDTO courseDTO)
        {
            while (true)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("Choose type of material:\n");

                    ShowMaterials();

                    var userInput = Console.ReadLine();

                    if (int.Parse(userInput) == 1)
                    {
                        courseDTO = await this.videoMaterialCreating.CreateAsync(courseDTO);
                    }
                    else if (int.Parse(userInput) == 2)
                    {
                        courseDTO = await this.textMaterialCreating.CreateAsync(courseDTO);
                    }
                    else
                    {
                        courseDTO = await this.internetMaterialCreating.CreateAsync(courseDTO);
                    }

                    while (true)
                    {
                        Console.Clear();

                        ShowCommands();

                        userInput = Console.ReadLine();

                        if (int.Parse(userInput) == 1)
                        {
                            break;
                        }
                        else
                        {
                            return await Finish(courseDTO);
                        }
                    }
                }
                catch (InvalidCastException)
                {
                    continue;
                }
            }
        }

        public void ShowCommands()
        {
            int i = 1;

            foreach (var item in Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine($"{i++} - {item}");
            }

            Console.WriteLine("\nChoose Command\n");
        }

        public void ShowMaterials()
        {
            int i = 1;

            foreach (var item in Enum.GetValues(typeof(MaterialTypes)))
            {
                Console.WriteLine($"{i++} - {item}");
            }

            Console.WriteLine("\nChoose Command\n");
        }
    }
}
