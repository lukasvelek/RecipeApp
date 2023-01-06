using System.Windows;

namespace RecipeApp.UI
{
    public class WindowElement
    {
        public Window Window { get; private set; }
        public string Name { get; private set; }

        public WindowElement(Window window, string name)
        {
            Window = window;
            Name = name;
        }
    }
}
