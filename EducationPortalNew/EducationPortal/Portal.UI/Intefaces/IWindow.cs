using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.UI.Intefaces
{
    public interface IWindow
    {
        string Title { get; }

        void Show();
    }
}
