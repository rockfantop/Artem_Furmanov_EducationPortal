using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Portal.Application.Interfaces;
using Portal.Application.Services;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Interfaces;
using Portal.Infrastructure.Repositories;
using Portal.Infrastructure.XML.XMLHandlers;
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
            services.AddTransient<IUserHandler, XMLUserHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<Registration>();
            services.AddTransient<Authentication>();

            return services.BuildServiceProvider();
        }
    }
}
