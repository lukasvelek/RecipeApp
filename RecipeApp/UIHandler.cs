using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecipeApp
{
    public class UIHandler
    {
        public List<Grid> GridList;

        public UIHandler()
        {
            GridList = new List<Grid>();
        }

        // GRID MANAGEMENT

        public void HideAllGrids()
        {
            foreach(Grid g in GridList)
            {
                HideGrid(g);
            }
        }

        public void HideGrid(Grid grid)
        {
            grid.Visibility = System.Windows.Visibility.Hidden;
        }

        public void ShowGrid(Grid grid)
        {
            HideAllGrids();

            grid.Visibility = System.Windows.Visibility.Visible;
        }

        // END OF GRID MANAGEMENT
    }
}
