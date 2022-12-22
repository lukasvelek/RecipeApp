﻿using RecipeApp.Recipe;
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
            grid.Visibility = Visibility.Hidden;
        }

        public void ShowGrid(Grid grid)
        {
            HideAllGrids();

            grid.Visibility = Visibility.Visible;
        }

        // END OF GRID MANAGEMENT

        public void RecipeListFill(ListBox listBox, List<Ingredient> ingredients)
        {
            if(listBox.Items.Count > 0)
            {
                listBox.Items.Clear();
            }

            foreach(Ingredient i in ingredients)
            {
                listBox.Items.Add(i);
            }

            listBox.SelectedIndex = 0;
        }
    }
}
