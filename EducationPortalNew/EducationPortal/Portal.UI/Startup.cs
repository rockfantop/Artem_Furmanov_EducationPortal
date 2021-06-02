using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Application.Services;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using Portal.Infrastructure.Json.JsonHandlers;
using Portal.Infrastructure.Repositories;
using Portal.UI.Intefaces;
using Portal.UI.Validators;
using Portal.UI.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI
{
    public static class Startup
    {
        public static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Dictionary windows
            services.AddScoped<IWindow, AuthWindow>();
            services.AddScoped<IWindow, RegisterWindow>();
            services.AddScoped<IWindow, CourseWindow>();

            services.AddScoped(typeof(IJsonHandler<>), typeof(JsonHandler<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));

            services.AddTransient<IHasher, SHA256Hasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();

            services.AddTransient<AbstractValidator<InputUserDTO>, UserValidator>();

            services.AddScoped<WindowsManager>();

            services.AddHostedService<WindowsManager>();

            return services.BuildServiceProvider();
        }
    }
}
