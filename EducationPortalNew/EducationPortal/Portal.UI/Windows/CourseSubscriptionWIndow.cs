using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.UI.Windows
{
    public class CourseSubscriptionWIndow : IWindow
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ICourseSkillService courseSkillService;
        private readonly IMaterialsService materialsService;

        public CourseSubscriptionWIndow(ICourseService courseService,
            IUserService userService,
            ICourseSkillService courseSkillService,
            IMaterialsService materialsService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.courseSkillService = courseSkillService;
            this.materialsService = materialsService;
        }

        public string Title => "Course Subscription";

        private enum Commands
        {
            SubscribeOnCourse = 1,
            ShowMySubs = 2,
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Welcome to the SubsCourses\n");

                await ShowAllCourses();

                ShowCommands();

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }
                else if (input == "1")
                {
                    await SubscribeCourse();
                }
                else if (input == "2")
                {
                    await ShowMySubsCourses();
                }
            }
        }

        public void ShowCommands()
        {
            Console.WriteLine("0 - To menu");

            int i = 1;

            foreach (var item in Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine($"{i++} - {item}");
            }

            Console.WriteLine("\nChoose Command\n");
        }

        public async Task SubscribeCourse()
        {
            var courseList = ((List<CourseDTO>)(await this.courseService.GetPublicListAsync(1, 10)).Result.Items);

            while (true)
            {
                try
                {
                    Console.Clear();

                    await ShowAllCourses();

                    Console.WriteLine("Choose Course:\n\n0 - Exit\n\n");

                    var userInput = int.Parse(Console.ReadLine());

                    if (userInput == 0)
                    {
                        return;
                    }

                    var course = courseList[userInput - 1];

                    Console.Clear();

                    Console.WriteLine($"Course: {course.Title}\t Description: {course.Description} \t Materials count: {((List<MaterialDTO>)course.Materials).Count}");
                    
                    Console.Write($"\nSkills: ");

                    ((List<CourseCourseSkillDTO>)course.CourseSkills).ForEach(x =>
                    {
                        var skill = this.courseSkillService.GetSkill(x.CourseSkillId).Result;

                        Console.Write($"\n{skill.Result.Title}\n");
                    });

                    Console.WriteLine("\n");

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Subscribe - 1\n\nExit - 0\n\n");

                            userInput = int.Parse(Console.ReadLine());

                            if (userInput == 0)
                            {
                                break;
                            }
                            else if (userInput == 1)
                            {
                                ((List<MaterialDTO>)course.Materials).ForEach(x =>
                                {
                                    this.materialsService.CreateStatus(x.Id, InSystemUser.GetInstance().Id);
                                }
                                );

                                await this.courseService.SubscribeOnCourse(InSystemUser.GetInstance().Id, course.Id);

                                break;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong input!");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong input!");
                }
            }
        }

        public async Task ShowAllCourses()
        {
            var courseList = (await this.courseService.GetPublicListAsync(1, 10)).Result;

            Console.WriteLine("All Courses:\n");

            var i = 1;

            if (courseList.Items == null || ((List<CourseDTO>)courseList.Items).Count == 0)
            {
                Console.WriteLine("No public courses\n");
                return;
            }

            foreach (var item in courseList.Items)
            {
                Console.Write($"{i++} Course:\t{item.Title}\n\n");
            }
        }

        public async Task ShowMySubsCourses()
        {
            Console.Clear();

            if (InSystemUser.GetInstance().SubscribedCourses == null)
            {
                Console.WriteLine("No subs courses\n");
                return;
            }

            var i = 1;

            foreach (var item in InSystemUser.GetInstance().SubscribedCourses)
            {
                Console.Write($"{i++} {item.Title}\n\n");
            }

            Console.WriteLine("Choose to open\n\n 0 - exit\n\n");

            var userInput = int.Parse(Console.ReadLine());

            if (userInput == 0)
            {
                return;
            }
            else
            {
                await ShowSubCourseInfo(InSystemUser.GetInstance().SubscribedCourses, userInput);
            }
        }

        public async Task ShowSubCourseInfo(IEnumerable<CourseDTO> courses, int input)
        {
            var list = (List<CourseDTO>)courses;

            while (true)
            {
                try
                {
                    var course = list[input - 1];

                    Console.Clear();

                    Console.WriteLine("Choose material:\n\n 0 - exit\n\n");

                    double progress = 1 * (double)(((List<MaterialDTO>)course.Materials).Count(x => x.IsReaded == true)) / (double)(((List<MaterialDTO>)course.Materials).Count);

                    progress *= 100;

                    Console.WriteLine($"Course: {course.Title}\n\nDescription: {course.Description}\n\nProgress: {progress}%\n\n");

                    var i = 1;

                    Console.WriteLine("Materials:\n");

                    foreach (var item in (List<MaterialDTO>)course.Materials)
                    {
                        if (item.IsReaded == true)
                        {
                            Console.WriteLine($"{i++} {item.Title}\tCompleted");
                        }
                        else
                        {
                            Console.WriteLine($"{i++} {item.Title}\tUncompleted");
                        }
                    }

                    Console.WriteLine();

                    var userInput = int.Parse(Console.ReadLine());

                    if (userInput == 0)
                    {
                        return;
                    }

                    while (true)
                    {
                        Console.Clear();

                        Console.WriteLine("0 - Exit\n\n1 - Complete\n\n");

                        var material = ((List<MaterialDTO>)course.Materials)[userInput - 1];

                        var index = userInput - 1;

                        Console.WriteLine($"Title: {material.Title}\tDataOfPublication:{material.DateOfPublication}\n\n{material.Content}\n\n");

                        userInput = int.Parse(Console.ReadLine());

                        if (userInput == 0)
                        {
                            break;
                        }
                        else if (userInput == 1)
                        {
                            ((List<MaterialDTO>)course.Materials)[index].IsReaded = true;

                            var logginedUser = new LogginedUserDTO
                            {
                                Id = InSystemUser.GetInstance().Id,
                                Email = InSystemUser.GetInstance().Email,
                                Name = InSystemUser.GetInstance().Name,
                                OwnedCourses = InSystemUser.GetInstance().OwnedCourses,
                                Skills = InSystemUser.GetInstance().Skills,
                                SubscribedCourses = InSystemUser.GetInstance().SubscribedCourses
                            };

                            if (((List<MaterialDTO>)course.Materials).Count(x => x.IsReaded == true) == ((List<MaterialDTO>)course.Materials).Count)
                            {
                                ((List<CourseSkillDTO>)InSystemUser.GetInstance().Skills).AddRange((List<CourseSkillDTO>)course.CourseSkills);

                                logginedUser.Skills = InSystemUser.GetInstance().Skills;
                            }

                            await this.userService.UpdateUserCoursesAsync(logginedUser);

                            continue;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong input!");
                }
            }
        }
    }
}
