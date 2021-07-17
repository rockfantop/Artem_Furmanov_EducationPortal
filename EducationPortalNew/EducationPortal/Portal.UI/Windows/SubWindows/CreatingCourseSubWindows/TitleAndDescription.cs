using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.CreatingCourseSubWindows
{
    public class TitleAndDescription : ITitleAndDescCreatingSubWindow
    {
        private readonly ICourseService courseService;
        private readonly IMaterialsCreatingSubWindow handler;

        public TitleAndDescription(ICourseService courseService, IMaterialsCreatingSubWindow handler)
        {
            this.courseService = courseService;
            this.handler = handler;
        }

        private enum Commands
        {
            NextStep = 1,
            Finish = 2
        }

        public async Task<CourseDTO> Finish(CourseDTO courseDTO)
        {
            var emptyCourse = new EmptyCourseDTO
            {
                OwnerId = courseDTO.OwnerId,
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                Description = courseDTO.Description
            };

            var result = await this.courseService.CreateCourseAsync(emptyCourse);

            return await Task.Factory.StartNew(() =>
            {
                if (result.IsSuccesful)
                {
                    return courseDTO;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<CourseDTO> ShowCreatingSubWindow(CourseDTO courseDTO)
        {
            while (true)
            {
                Console.Clear();

                var inputTitle = "";
                var inputDescription = "";

                //TODO: Data Validation
                while (true)
                {
                    Console.WriteLine("Write Title of the course\n");

                    inputTitle = Console.ReadLine();

                    break;
                }

                while (true)
                {
                    Console.WriteLine("\nWrite Description of the course\n");

                    inputDescription = Console.ReadLine();

                    break;
                }

                courseDTO.Title = inputTitle;
                courseDTO.Description = inputDescription;

                while (true)
                {
                    Console.Clear();

                    try
                    {
                        ShowCommands();

                        var userInput = int.Parse(Console.ReadLine());

                        if (userInput == 1)
                        {
                            return await this.handler.ShowCreatingSubWindow(courseDTO); 
                        }
                        else if (userInput == 2)
                        {
                            var result = await this.Finish(courseDTO);

                            if (result != null)
                            {
                                return result;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine("\nWrong input! Try again\n");
                    }
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
    }
}
