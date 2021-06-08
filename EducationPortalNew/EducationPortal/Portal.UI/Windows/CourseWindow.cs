using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using Portal.UI.Windows.SubWindows.CreatingCourseSubWindows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows
{
    class CourseWindow : IWindow
    {
        private readonly IUserService userService;
        private readonly ITitleAndDescCreatingSubWindow courseCreatingSubWindows;

        public CourseWindow(IUserService userService, ITitleAndDescCreatingSubWindow courseCreatingSubWindows)
        {
            this.userService = userService;
            this.courseCreatingSubWindows = courseCreatingSubWindows;
        }

        public string Title => "Course Creating";

        private enum Commands
        {
            CreateCourse = 1,
            ShowMyCourses = 2,
            ClearConsole = 3
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Welcome to the Course\n");

                ShowCommands();

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                await CommandSelector(input);
            }
        }
        
        public void ShowCommands()
        {
            int i = 1;

            Console.WriteLine("0 - To menu");

            foreach (var item in Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine($"{i++} - {item}");
            }

            Console.WriteLine("\nChoose Command\n");
        }
        
        public async Task CommandSelector(string input)
        {
            try
            {
                switch (int.Parse(input))
                {
                    case (int)Commands.CreateCourse:
                        await CreateCourse();
                        break;
                    case (int)Commands.ShowMyCourses:
                        ShowYourCourses();
                        break;
                    case (int)Commands.ClearConsole:
                        Console.Clear();
                        return;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task CreateCourse()
        {
            var emptyCourse = new CourseDTO
            {
                Id = Guid.NewGuid(),
                Owner = InSystemUser.GetInstance().Id
            };

            var course = await this.courseCreatingSubWindows.ShowCreatingSubWindow(emptyCourse);

            if (course != null)
            {
                var list = ((List<CourseDTO>)InSystemUser.GetInstance().OwnedCourses);

                list.Add(course);

                InSystemUser.GetInstance().OwnedCourses = list;

                var logginedUser = new LogginedUserDTO
                {
                    Id = InSystemUser.GetInstance().Id,
                    Email = InSystemUser.GetInstance().Email,
                    Name = InSystemUser.GetInstance().Name,
                    OwnedCourses = InSystemUser.GetInstance().OwnedCourses,
                    Skills = InSystemUser.GetInstance().Skills,
                    SubscribedCourses = InSystemUser.GetInstance().SubscribedCourses
                };

                await this.userService.UpdateUserCoursesAsync(logginedUser);
            }
        }

        public void ShowYourCourses()
        {
            var courseList = InSystemUser.GetInstance().OwnedCourses;

            Console.WriteLine("Result:\n");

            if (courseList == null)
            {
                Console.WriteLine("No courses\n");
                return;
            }

            foreach (var item in courseList)
            {
                Console.Write($"{item.Title}\t");

                if (item.IsPublic)
                {
                    Console.Write("Status: public\n\n");
                }
                else
                {
                    Console.Write("Status: private\n\n");
                }
            }

            Console.ReadKey();
        }
    }
}
