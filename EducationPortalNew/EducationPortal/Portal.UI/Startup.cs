using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient<IWindow, AuthWindow>();
            services.AddTransient<IWindow, RegisterWindow>();
            services.AddTransient<IWindow, CourseWindow>();

            services.AddTransient(typeof(IJsonHandler<>), typeof(JsonHandler<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IHasher, SHA256Hasher>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();

            services.AddTransient<AbstractValidator<InputUserDTO>, UserValidator>();

            services.AddTransient<WindowsManager>();

            return services.BuildServiceProvider();
        }
    }
}
