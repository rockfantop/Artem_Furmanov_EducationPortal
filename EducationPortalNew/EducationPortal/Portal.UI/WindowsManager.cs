using Microsoft.Extensions.Hosting;
using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.UI
{
    public class WindowsManager : BackgroundService
    {
        private Dictionary<string, IWindow> windowDictionary = new Dictionary<string, IWindow>();

        public WindowsManager(IEnumerable<IWindow> window)
        {
            foreach (var item in window)
            {
                this.windowDictionary.Add(item.Title, item);
            }
        }

        public async Task ShowWindowList()
        {
            await Task.Factory.StartNew(() =>
            {
                int i = 1;

                foreach (var item in this.windowDictionary.Keys)
                {
                    Console.WriteLine($"{i++} {item}");
                }
                Console.WriteLine("\nChoose window\n");
            });
        }

        public async Task SwitchWindow(string input)
        {
            int i = 1;

            string window = "";

            foreach (var item in this.windowDictionary.Keys)
            {
                if (i.ToString() == input)
                {
                    window = item;
                    break;
                }
                else
                {
                    i++;
                }
            }
            try
            {
                await this.windowDictionary[window].ShowAsync();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input!");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.windowDictionary["Registration"].ShowAsync();
            await this.windowDictionary["Authentication"].ShowAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.Clear();

                Console.WriteLine("Welcome to Main Menu\n");

                await ShowWindowList();

                string input = Console.ReadLine();

                await SwitchWindow(input);
            }
        }
    }
}
