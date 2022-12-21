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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        public const string VERSION = "2.0";

        UIHandler uiHandler;

        public MainWindow()
        {
            InitializeComponent();

            // Object initialization
            uiHandler = new UIHandler();


            // Grid initialization
            uiHandler.GridList.Add(RecipeMain);
            uiHandler.GridList.Add(RecipeList);

            uiHandler.HideAllGrids();

            
            // Data initialization



            // Initial configuration
            Window.Title = "Recepty " + VERSION;
            Window.ResizeMode = ResizeMode.CanMinimize;

            uiHandler.ShowGrid(RecipeMain);
        }

        private void RecipeList_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid(RecipeList);
        }
    }
}
