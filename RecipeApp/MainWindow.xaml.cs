using RecipeApp.Recipe;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private const string VERSION = "2.2";

        private static UIHandler uiHandler = new UIHandler();
        private static DataHandler dataHandler = new DataHandler();
        private static Randomizer randomizer = new Randomizer();

        public MainWindow()
        {
            InitializeComponent();


            // Object initialization


            // Grid initialization
            uiHandler.AddGrid(RecipeMain, "ui_main_menu");
            uiHandler.AddGrid(RecipeList, "ui_recipe_list");

            uiHandler.HideAllGrids();


            // Data initialization
            dataHandler.LoadRecipes();
            randomizer.Shuffle(dataHandler.Recipes);


            // Initial configuration
            Window.Title = "Recepty " + VERSION;
            Window.ResizeMode = ResizeMode.CanMinimize;

            uiHandler.ShowGrid("ui_main_menu");
        }

        private void _RecipeList()
        {
            if (RecipeList_Recipes.Items.Count > 0)
            {
                RecipeList_Recipes.Items.Clear();
            }

            uiHandler.RecipeListFill(RecipeList_Recipes, dataHandler.Recipes);

            if (RecipeList_Recipes.SelectedIndex >= 0)
            {
                RecipeList_DeleteRecipe.IsEnabled = true;
                RecipeList_EditRecipe.IsEnabled = true;
            }
            else
            {
                RecipeList_DeleteRecipe.IsEnabled = false;
                RecipeList_EditRecipe.IsEnabled = false;
            }
        }

        private void RecipeList_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipe_list");

            _RecipeList();
        }

        private void RecipeList_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_main_menu");
        }

        private void RecipeList_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe.Recipe r = (Recipe.Recipe)RecipeList_Recipes.SelectedItem;
        }

        private void RecipeList_NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm rf = new RecipeForm();

            rf.ShowDialog();

            if (rf._Recipe != null)
            {
                dataHandler.Recipes.Add(rf._Recipe);

                _RecipeList();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            dataHandler.SaveRecipes();
        }

        private void RecipeList_DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList_Recipes.SelectedIndex >= 0)
            {
                int index = RecipeList_Recipes.SelectedIndex;
                Recipe.Recipe r = (Recipe.Recipe)RecipeList_Recipes.SelectedItem;

                RecipeList_Recipes.Items.RemoveAt(index);
                dataHandler.Recipes.Remove(r);

                _RecipeList();
            }
        }

        private void RecipeRandom_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_main_menu");
        }

        private void RecipeRandom_Generate_Click(object sender, RoutedEventArgs e)
        {
            SingleRecipeWindow srw = new SingleRecipeWindow();

            randomizer.RandomRecipe();

            srw.LoadRecipe(randomizer.LastRecipe);
            srw.ShowDialog();
        }

        private void RecipeList_OpenRecipe_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeList_Recipes.SelectedItem != null)
            {
                SingleRecipeWindow srw = new SingleRecipeWindow();

                srw.LoadRecipe((Recipe.Recipe)RecipeList_Recipes.SelectedItem);
                srw.Show();
            }
        }

        private void RecipeList_EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeList_Recipes.SelectedItem != null)
            {
                RecipeForm rf = new RecipeForm();

                rf.LoadEditRecipe((Recipe.Recipe)RecipeList_Recipes.SelectedItem);
                rf.ShowDialog();

                if(rf._Recipe != null)
                {
                    if (rf._Edit)
                    {
                        int index = RecipeList_Recipes.SelectedIndex;

                        dataHandler.Recipes[index] = rf._Recipe;
                    }

                    rf._Edit = false;
                    _RecipeList();
                }
            }
        }
    }
}
