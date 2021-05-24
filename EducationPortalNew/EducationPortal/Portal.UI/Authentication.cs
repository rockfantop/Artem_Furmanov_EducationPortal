using Microsoft.Extensions.Logging;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
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

        public void Start()
        {
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
                    Console.WriteLine("In Application");
                    break;
                }
                else
                {
                    Console.WriteLine(serviceResult.Message);
                }
            }
        }
    }
}
