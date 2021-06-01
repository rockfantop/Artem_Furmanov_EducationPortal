using FluentValidation;
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
    public class RegisterWindow : IWindow
    {
        private readonly IUserService userService;
        private AbstractValidator<InputUserDTO> userValidator;
        private readonly ILogger<RegisterWindow> logger;

        public RegisterWindow(IUserService service,
            AbstractValidator<InputUserDTO> validator,
            ILogger<RegisterWindow> logger)
        {
            this.userService = service;
            this.userValidator = validator;
            this.logger = logger;
        }

        public string Title => "Registration";

        public async Task ShowAsync()
        {
            Console.Clear();

            var user = new InputUserDTO();

            FluentValidation.Results.ValidationResult result = new FluentValidation.Results.ValidationResult();

            Console.WriteLine("Welcome to the registration!\n");

            Console.WriteLine("Already exist? Press 1\n");

            while (true)
            {

                if (Console.ReadLine() == "1")
                {
                    return;
                }

                while (user.Email == null || user.Email == "" || result?.IsValid == false)
                {
                    Console.Write("Enter your email: ");

                    user.Email = Console.ReadLine();

                    result = this.userValidator.Validate(user, options => options.IncludeRuleSets("Email"));

                    if (result.IsValid == true)
                    {
                        break;
                    }

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                while (user.Password == null || user.Password == "" || result?.IsValid == false)
                {
                    Console.Write("Enter your password: ");

                    user.Password = Console.ReadLine();

                    result = userValidator.Validate(user, options => options.IncludeRuleSets("Password"));

                    if (result.IsValid == true)
                    {
                        break;
                    }

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                var serviceResult = await this.userService.RegistationAsync(user);

                this.logger.LogInformation($"{serviceResult.Message} with {user.Email}", user.Email);

                if (serviceResult.IsSuccesful)
                {
                    break;
                }

                user.Email = null;
                user.Password = null;

                Console.WriteLine(serviceResult.Message);
            }
        }
    }
}
