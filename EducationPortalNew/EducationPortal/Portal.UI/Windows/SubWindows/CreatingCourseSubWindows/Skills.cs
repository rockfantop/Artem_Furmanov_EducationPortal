using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows.SubWindows.CreatingCourseSubWindows
{
    public class Skills : ICourseSkillCreatingSubWindow
    {
        private readonly ICourseService courseService;
        private readonly ICourseSkillService courseSkillService;

        public Skills(ICourseService courseService, ICourseSkillService courseSkillService)
        {
            this.courseService = courseService;
            this.courseSkillService = courseSkillService;
        }

        private enum Commands
        {
            AddAnotherSkill = 1,
            Finish = 2
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

                    Console.WriteLine("Choose skill:\n");

                    var result = await ShowAllSkills();

                    var userInput = int.Parse(Console.ReadLine()) - 1;

                    var enumeration = courseDTO.CourseSkills;

                    var list = (List<CourseSkillDTO>)enumeration;
                    
                    if (enumeration == null) 
                    {
                        list = new List<CourseSkillDTO>();
                    }

                    list.Add(result[userInput]);

                    courseDTO.CourseSkills = list;

                    while (true)
                    {
                        Console.Clear();

                        ShowCommands();

                        var userInput2 = Console.ReadLine();

                        if (int.Parse(userInput2) == 1)
                        {
                            break;
                        }
                        else if (int.Parse(userInput2) == 2)
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

        public async Task<List<CourseSkillDTO>> ShowAllSkills()
        {
            var skills = (await this.courseSkillService.ShowAllAsync()).Result;

            var i = 1;

            Console.WriteLine("Result:\n");

            if (skills == null)
            {
                Console.WriteLine("No Skills\n");

                Console.ReadKey();

                return null;
            }

            foreach (var item in skills)
            {
                Console.Write($"{i++} {item.Title}\n\n");
            }

            return skills;
        }
    }
}
