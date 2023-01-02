using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeApp
{
    public partial class RecipeForm : Window
    {
        private static UIHandler uiHandler = new UIHandler();

        public RecipeForm()
        {
            InitializeComponent();

            // Grid initialization
            uiHandler.AddGrid(RecipeForm_Main, "ui_recipeform_main");

            uiHandler.HideAllGrids();


            uiHandler.ShowGrid("ui_recipeform_main");
        }
    }
}
