using FluentValidation;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI.Windows
{
    public class RegisterWindow : IWindow
    {
        private readonly IUserService userService;
        private AbstractValidator<InputUserDTO> userValidator;

        public RegisterWindow(IUserService service, AbstractValidator<InputUserDTO> validator)
        {
            this.userService = service;
            this.userValidator = validator;
        }

        public string Title => "Registration";

        public void Show()
        {
            Console.Clear();

            var user = new InputUserDTO();

            FluentValidation.Results.ValidationResult result = new FluentValidation.Results.ValidationResult();

            Console.WriteLine("Welcome to the registration!\n");

            Console.WriteLine("Already exist? Press 1\n");
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

                var serviceResult = this.userService.Registation(user);

                Console.WriteLine(serviceResult.Message);
            }
        }
    }
}
