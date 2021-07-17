using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows
{
    public class CourseSkillWindow : IWindow
    {
        private readonly ICourseSkillService courseSkillService;

        public CourseSkillWindow(ICourseSkillService courseSkillService)
        {
            this.courseSkillService = courseSkillService;
        }

        public string Title => "Skills";

        private enum Commands
        {
            CreateSkill = 1,
            ShowAllSkills = 2,
            ClearConsole = 3
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Welcome to the Skills\n");

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
                    case (int)Commands.CreateSkill:
                        await CreateCourseSkill();
                        break;
                    case (int)Commands.ShowAllSkills:
                        await ShowAllSkills();
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

        public async Task CreateCourseSkill()
        {
            var curseSkillDTO = new CourseSkillDTO()
            {
                Id = Guid.NewGuid(),
                SkillLevel = 0
            };

            while (true)
            {
                Console.Clear();

                var inputTitle = "";
                var inputDescription = "";

                //TODO: Data Validation
                while (true)
                {
                    Console.WriteLine("Write Title of the skill\n");

                    inputTitle = Console.ReadLine();

                    break;
                }

                while (true)
                {
                    Console.WriteLine("\nWrite Description of the skill\n");

                    inputDescription = Console.ReadLine();

                    break;
                }

                curseSkillDTO.Title = inputTitle;
                curseSkillDTO.Description = inputDescription;

                var result = await this.courseSkillService.AddSkillAsync(curseSkillDTO);

                if (result.IsSuccesful)
                {
                    Console.WriteLine(result.Message);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        public async Task ShowAllSkills()
        {
            var skills = (await this.courseSkillService.GetListAsync(1, 10)).Result;

            Console.WriteLine("Result:\n");

            if (skills.Items == null || ((List<CourseSkillDTO>)skills.Items).Count == 0)
            {
                Console.WriteLine("No Skills\n");

                Console.ReadKey();

                return;
            }

            foreach (var item in skills.Items)
            {
                Console.Write($"{item.Title}\n\n");
            }

            Console.ReadKey();
        }
    }
}
