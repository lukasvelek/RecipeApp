using System.Windows.Controls;

namespace RecipeApp.UI
{
    public class GridElement
    {
        public Grid Grid { get; set; }
        public string Name { get; set; }

        public GridElement(Grid grid, string name)
        {
            Grid = grid;
            Name = name;
        }
    }
}
