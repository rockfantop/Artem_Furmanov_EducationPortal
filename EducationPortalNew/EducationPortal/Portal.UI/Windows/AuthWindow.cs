using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI.Windows
{
    public class AuthWindow : IWindow
    {
        private readonly IUserService userService;

        public AuthWindow(IUserService service)
        {
            this.userService = service;
        }

        public string Title => "Authentication";

        public void Show()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Authentication\n");

            InputUserDTO user;

            while (true)
            {
                Console.Write("Enter your email: ");

                string email = Console.ReadLine();

                Console.Write("Enter your password: ");

                string password = Console.ReadLine();

                user = new InputUserDTO
                {
                    Email = email,
                    Password = password
                };

                var serviceResult = this.userService.Athentication(user);

                if (serviceResult.IsSuccesful)
                {

                    Console.WriteLine("\nIn Application\n");

                    var logginedUser = InSystemUser.GetInstance();

                    logginedUser.Id = serviceResult.Result.Id;
                    logginedUser.Email = serviceResult.Result.Email;
                    logginedUser.Name = serviceResult.Result.Name;
                    logginedUser.OwnedCourses = serviceResult.Result.OwnedCourses;

                    return;
                }
                else
                {
                    Console.WriteLine(serviceResult.Message);
                }
            }
        }
    }
}
