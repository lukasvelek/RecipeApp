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
using System.IO;
using RecipeApp.Recipe;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private const string VERSION = "2.0";
        private const string DATA_FILE = "recipes.dat";

        UIHandler uiHandler;
        DataHandler dataHandler;

        public MainWindow()
        {
            InitializeComponent();


            // Object initialization
            uiHandler = new UIHandler();
            dataHandler = new DataHandler();


            // Grid initialization
            uiHandler.GridList.Add(RecipeMain);
            uiHandler.GridList.Add(RecipeList);

            uiHandler.HideAllGrids();


            // Data initialization
            if (File.Exists(DATA_FILE))
            {
                dataHandler.LoadRecipes(DATA_FILE);
            }
            else
            {
                File.Create(DATA_FILE);
            }

            List<Ingredient> li = new List<Ingredient>();

            li.Add(new Ingredient("Ing Test", 3, "g"));

            List<SideDish> lsd = new List<SideDish>();

            lsd.Add(new SideDish("Side Test"));

            Recipe.Recipe r = new Recipe.Recipe("Test", "Test", 3, li, lsd);

            dataHandler.Recipes.Add(r);


            // Initial configuration
            Window.Title = "Recepty " + VERSION;
            Window.ResizeMode = ResizeMode.CanMinimize;

            uiHandler.ShowGrid(RecipeMain);
        }

        private void RecipeList_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid(RecipeList);

            if(RecipeList_Recipes.Items.Count > 0)
            {
                RecipeList_Recipes.Items.Clear();
            }

            uiHandler.RecipeListFill(RecipeList_Recipes, dataHandler.Recipes);
        }

        private void RecipeList_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid(RecipeMain);
        }

        private void RecipeList_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe.Recipe r = (Recipe.Recipe)RecipeList_Recipes.SelectedItem;

            if(r != null)
            {
                RecipeList_RecipeName.Content = r.Name;
                RecipeList_RecipeServings.Content = r.Servings.ToString();
                RecipeList_RecipeNote.Content = r.Note;

                RecipeList_RecipeSideDishes.Items.Clear();
                
                foreach(SideDish sd in r.AvailableSideDish)
                {
                    RecipeList_RecipeSideDishes.Items.Add(sd);
                }
            }
        }
    }
}
