using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeApp.UI
{
    public class WindowElement
    {
        public Window Window { get; set; }
        public string Name { get; set; }

        public WindowElement(Window window, string name)
        {
            Window = window;
            Name = name;
        }
    }
}
