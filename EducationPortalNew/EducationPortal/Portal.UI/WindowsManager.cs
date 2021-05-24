using Portal.UI.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI
{
    public class WindowsManager
    {
        private Dictionary<string, IWindow> windowDictionary = new Dictionary<string, IWindow>();

        public WindowsManager(IEnumerable<IWindow> window)
        {
            foreach (var item in window)
            {
                this.windowDictionary.Add(item.Title, item);
            }
        }

        public void ShowWindowList()
        {
            int i = 1;

            foreach (var item in this.windowDictionary.Keys)
            {
                Console.WriteLine($"{i++} {item}");
            }
            Console.WriteLine("\nChoose window\n");
        }

        public void SwitchWindow(string input)
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
                this.windowDictionary[window].Show();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input!");
            }
        }

        public void Start()
        {
            this.windowDictionary["Registration"].Show();
            this.windowDictionary["Authentication"].Show();

            while (true)
            {
                Console.Clear();

                ShowWindowList();

                string input = Console.ReadLine();

                SwitchWindow(input);
            }
        }
    }
}
