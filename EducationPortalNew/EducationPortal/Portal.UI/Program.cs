using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Application.Services;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using Portal.Infrastructure.Repositories;
using Portal.Infrastructure.XML.XMLHandlers;
using Portal.UI.Validators;
using System;

namespace Portal.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = ConfigureServices(new ServiceCollection());
            controller.GetRequiredService<Registration>().Start();
            controller.GetRequiredService<Authentication>().Start();
        }

        static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IXmlHandler<User>, XmlHandler<User>>();
            services.AddTransient<IHasher, SHA256Hasher>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<AbstractValidator<InputUserDTO>, UserValidator>();
            services.AddTransient<Registration>();
            services.AddTransient<Authentication>();

            return services.BuildServiceProvider();
        }
    }
}
