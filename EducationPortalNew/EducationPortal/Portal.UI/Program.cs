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
using Portal.UI.Validators;
using Portal.UI.Windows;
using System;

namespace Portal.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = Startup.ConfigureServices(new ServiceCollection());
            controller.GetRequiredService<WindowsManager>().Start();
        }
    }
}
