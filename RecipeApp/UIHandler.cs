using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace RecipeApp
{
    public class UIHandler
    {
        public List<UI.GridElement> GridList;

        public UIHandler()
        {
            GridList = new List<UI.GridElement>();
        }

        public void AddGrid(Grid g, string name)
        {
            GridList.Add(new UI.GridElement(g, name));
        }

        // GRID MANAGEMENT

        public void HideAllGrids()
        {
            foreach(UI.GridElement ge in GridList)
            {
                HideGrid(ge.Name);
            }
        }

        public void HideGrid(string name)
        {
            foreach(UI.GridElement ge in GridList)
            {
                if(ge.Name == name)
                {
                    ge.Grid.Visibility = Visibility.Hidden;
                }
            }
        }

        public void ShowGrid(string name)
        {
            HideAllGrids();

            foreach(UI.GridElement ge in GridList)
            {
                if(ge.Name == name)
                {
                    ge.Grid.Visibility = Visibility.Visible;
                }
            }
        }

        // END OF GRID MANAGEMENT

        public void RecipeListFill(ListBox listBox, List<Recipe.Recipe> recipes)
        {
            if(listBox.Items.Count > 0)
            {
                listBox.Items.Clear();
            }

            foreach(Recipe.Recipe r in recipes)
            {
                listBox.Items.Add(r);
            }

            listBox.SelectedIndex = 0;
        }
    }
}
