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
            uiHandler.AddGrid(RecipeMain, "ui_main_menu");
            uiHandler.AddGrid(RecipeList, "ui_recipe_list");
            uiHandler.AddGrid(RecipeNew, "ui_recipe_new");

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
            li.Add(new Ingredient("Ing Test 2", 2, "ml"));

            List<SideDish> lsd = new List<SideDish>();

            lsd.Add(new SideDish("Side Test"));
            lsd.Add(new SideDish("Side Test 2"));

            Recipe.Recipe r = new Recipe.Recipe("Test", "Test", 3, li, lsd);

            dataHandler.Recipes.Add(r);


            // Initial configuration
            Window.Title = "Recepty " + VERSION;
            Window.ResizeMode = ResizeMode.CanMinimize;

            uiHandler.ShowGrid("ui_main_menu");
        }

        private void RecipeList_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipe_list");

            if(RecipeList_Recipes.Items.Count > 0)
            {
                RecipeList_Recipes.Items.Clear();
            }

            uiHandler.RecipeListFill(RecipeList_Recipes, dataHandler.Recipes);
        }

        private void RecipeList_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_main_menu");
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

                RecipeList_RecipeIngredients.Items.Clear();

                foreach(Ingredient i in r.Ingredients)
                {
                    RecipeList_RecipeIngredients.Items.Add(i);
                }
            }
        }

        private void RecipeList_NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipe_new");
        }
    }
}
