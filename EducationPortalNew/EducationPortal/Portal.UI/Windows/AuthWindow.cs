using Microsoft.Extensions.Logging;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Windows
{
    public class AuthWindow : IWindow
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthWindow> logger;

        public AuthWindow(IUserService service, ILogger<AuthWindow> logger)
        {
            this.userService = service;
            this.logger = logger;
        }

        public string Title => "Authentication";

        public async Task ShowAsync()
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

                var serviceResult = await this.userService.AuthenticationAsync(user);

                if (serviceResult.IsSuccesful)
                {
                    this.logger.LogInformation($"User {serviceResult.Result.Email} authorized", serviceResult.Result.Email);

                    Console.WriteLine("\nIn Application\n");

                    var logginedUser = InSystemUser.GetInstance();

                    logginedUser.Id = serviceResult.Result.Id;
                    logginedUser.Email = serviceResult.Result.Email;
                    logginedUser.Name = serviceResult.Result.Name;
                    logginedUser.Skills = serviceResult.Result.Skills;
                    logginedUser.OwnedCourses = serviceResult.Result.OwnedCourses;
                    logginedUser.SubscribedCourses = serviceResult.Result.SubscribedCourses;

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
