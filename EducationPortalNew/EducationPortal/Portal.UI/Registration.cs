using FluentValidation;
using Microsoft.Extensions.Logging;
using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.UI.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI
{
    class Registration
    {
        private readonly IUserService userService;
        //private readonly ILogger<Registration> logger;
        private UserValidator userValidator = new UserValidator();

        public Registration(IUserService service)
        {
            this.userService = service;
        }

        private void RegistrateUser(User newUser)
        {
            userService.CreateUser(newUser);
            //logger.LogInformation($"Created {newUser.Email} user");
        }

        public void Start()
        {
            var user = new User();

            FluentValidation.Results.ValidationResult result = new FluentValidation.Results.ValidationResult();

            Console.WriteLine("Welcome to the registration!\n");

            Console.WriteLine("Already exist? Press 1\n");
            {

                if (Console.ReadLine() == "1")
                {
                    return;
                }

                while (user.Name == null || user.Name == "")
                {
                    Console.Write("Enter your name: ");

                    user.Name = Console.ReadLine();

                    result = userValidator.Validate(user, options => options.IncludeRuleSets("Name"));

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                while (user.Email == null || user.Email == "" || result?.IsValid == false)
                {
                    Console.Write("Enter your email: ");

                    user.Email = Console.ReadLine();

                    result = userValidator.Validate(user, options => options.IncludeRuleSets("Email"));

                    if (result.IsValid == true)
                    {
                        if (userService.FindUser(user.Email) != null)
                        {
                            Console.WriteLine("Email is exist!");
                            user.Email = "";
                            continue;
                        }
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

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                RegistrateUser(user);

                Console.WriteLine("You are registered!");
            }
        }
    }
}
