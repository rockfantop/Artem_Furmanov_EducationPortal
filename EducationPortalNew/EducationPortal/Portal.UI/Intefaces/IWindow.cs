using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.UI.Intefaces
{
    public interface IWindow
    {
        string Title { get; }

        Task ShowAsync();
    }
}
