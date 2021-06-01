using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.MapperProfiles;
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
using Portal.UI.Windows.SubWindows.CreatingCourseSubWindows;
using Portal.UI.Windows.SubWindows.MaterialCreatingSubWindows;
using System;
using System.Threading.Tasks;

namespace Portal.UI
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().StartAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddDebug();
                })
                .ConfigureServices((_, services) =>
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
                    services.AddScoped<IMaterialsService, MaterialsService>();
                    services.AddScoped<IInternetMaterialService, InternetMaterialService>();
                    services.AddScoped<IVideoMaterialService, VideoMaterialService>();
                    services.AddScoped<ITextMaterialService, TextMaterialService>();

                    services.AddScoped<ITitleAndDescCreatingSubWindow, TitleAndDescription>();
                    services.AddScoped<IMaterialsCreatingSubWindow, Materials>();

                    services.AddScoped<InternetMaterialCreating>();
                    services.AddScoped<VideoMaterialCreating>();
                    services.AddScoped<TextMaterialCreating>();

                    services.AddAutoMapper(typeof(UserProfile));

                    services.AddTransient<AbstractValidator<InputUserDTO>, UserValidator>();

                    services.AddScoped<WindowsManager>();

                    services.AddHostedService<WindowsManager>();
                });
    }
}
