using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup.Localizer;

namespace RecipeApp
{
    public class UIHandler
    {
        public List<UI.GridElement> GridList;
        public List<UI.WindowElement> WindowList;

        public UIHandler()
        {
            GridList = new List<UI.GridElement>();
            WindowList = new List<UI.WindowElement>();
        }

        public void AddWindow(Window window, string name)
        {
            WindowList.Add(new UI.WindowElement(window, name));
        }

        public void WindowOpen(string name)
        {
            foreach(UI.WindowElement window in WindowList)
            {
                if(window.Name == name)
                {
                    Window w = window.Window;

                    w.Show();
                }
            }
        }

        public Window GetWindow(string name)
        {
            Window? w = null;

            foreach (UI.WindowElement window in WindowList)
            {
                if (window.Name == name)
                {
                    w = window.Window;
                    break;
                }
            }

            return w;
        }

        public void CloseAllWindows()
        {
            for(int i = 1; i < WindowList.Count + 1; i++)
            {
                WindowList[i - 1].Window = null;
            }
        }

        public void AddGrid(Grid g, string name)
        {
            GridList.Add(new UI.GridElement(g, name));
        }

        // GRID MANAGEMENT

        public void HideAllGrids()
        {
            foreach (UI.GridElement ge in GridList)
            {
                HideGrid(ge.Name);
            }
        }

        public void HideGrid(string name)
        {
            foreach (UI.GridElement ge in GridList)
            {
                if (ge.Name == name)
                {
                    ge.Grid.Visibility = Visibility.Hidden;
                }
            }
        }

        public void ShowGrid(string name)
        {
            HideAllGrids();

            foreach (UI.GridElement ge in GridList)
            {
                if (ge.Name == name)
                {
                    ge.Grid.Visibility = Visibility.Visible;
                }
            }
        }

        // END OF GRID MANAGEMENT

        public void RecipeListFill(ListBox listBox, List<Recipe.Recipe> recipes)
        {
            if (listBox.Items.Count > 0)
            {
                listBox.Items.Clear();
            }

            foreach (Recipe.Recipe r in recipes)
            {
                listBox.Items.Add(r);
            }

            listBox.SelectedIndex = 0;
        }
    }
}
