using Microsoft.Extensions.Logging;
using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using Portal.UI.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI
{
    class Authentication
    {
        private readonly IUserService userService;

        public Authentication(IUserService service)
        {
            this.userService = service;
        }

        private bool AuthenticateUser(string email, string password)
        {
            return this.userService.VerifyUser(email, password);
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Authentication\n");
            while (true)
            {
                Console.Write("Enter your email: ");

                string email = Console.ReadLine();

                Console.Write("Enter your password: ");

                string password = Console.ReadLine();

                if (AuthenticateUser(email, password) != false)
                {
                    Console.WriteLine("In Application");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email or password");
                }
            }
        }
    }
}
