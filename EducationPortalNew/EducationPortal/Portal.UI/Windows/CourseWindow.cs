using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI.Windows
{
    class CourseWindow : IWindow
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public CourseWindow(ICourseService courseService, IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }

        public string Title => "Course";

        private enum Commands
        {
            CreateCourse = 1,
            ShowCourses = 2,
            ClearConsole = 3
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Course\n");

                ShowCommands();

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                CommandSelector(input);
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

        public void CommandSelector(string input)
        {
            try
            {
                switch (int.Parse(input))
                {
                    case (int)Commands.CreateCourse:
                        CreateCourse();
                        break;
                    case (int)Commands.ShowCourses:
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

        public void CreateCourse()
        {
            var emptyCourse = new EmptyCourseDTO();

            Console.WriteLine("Write Title of the course\n");

            string input = Console.ReadLine();

            emptyCourse.Title = input;
            emptyCourse.Owner = InSystemUser.GetInstance().Id;

            var result = this.courseService.CreateCourse(emptyCourse);

            Console.WriteLine(result.Message);
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
                Console.WriteLine(item.Title);
            }
        }
    }
}
