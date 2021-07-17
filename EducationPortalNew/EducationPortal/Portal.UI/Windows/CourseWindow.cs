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
        private readonly ICourseService courseService;
        private readonly ITitleAndDescCreatingSubWindow courseCreatingSubWindows;

        public CourseWindow(IUserService userService, ICourseService courseService, ITitleAndDescCreatingSubWindow courseCreatingSubWindows)
        {
            this.userService = userService;
            this.courseService = courseService;
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
                        await ShowYourCourses();
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
                OwnerId = InSystemUser.GetInstance().Id
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

        public async Task ShowYourCourses()
        {
            var result = await this.courseService.GetUserOwnedCourseListAsync(InSystemUser.GetInstance().Id, 1, 10);

            Console.WriteLine("Result:\n");

            if (result.Result.Items == null || ((List<CourseDTO>)result.Result.Items).Count == 0)
            {
                Console.WriteLine("No courses\n");

                Console.ReadKey();

                return;
            }

            foreach (var item in result.Result.Items)
            {
                Console.Write($"{item.Title}\t");

                if (item.IsPublic)
                {
                    Console.Write($"{item.Title}\t Status: public\n\n");
                }
                else
                {
                    Console.Write($"{item.Title}\t Status: private\n\n");
                }
            }

            Console.ReadKey();
        }
    }
}
