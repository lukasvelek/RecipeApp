using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
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


            // Window initialization
            uiHandler.AddWindow(new RecipeForm(), "window_recipeform");
            uiHandler.AddWindow(new SingleRecipeWindow(), "window_singlerecipe");


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

                RecipeList_RecipeName.Content = "";
                RecipeList_RecipeNote.Content = "";
                RecipeList_RecipeServings.Content = "";
                RecipeList_RecipeTimeNeeded.Content = "";
                RecipeList_RecipeIngredients.Items.Clear();
                RecipeList_RecipeSideDishes.Items.Clear();
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

            if (r != null)
            {
                RecipeList_RecipeName.Content = r.Name;
                RecipeList_RecipeNote.Content = r.Note;
                RecipeList_RecipeServings.Content = r.Servings.ToString();
                RecipeList_RecipeTimeNeeded.Content = r.TimeNeededMinutes.ToString();

                RecipeList_RecipeSideDishes.Items.Clear();

                foreach (SideDish sd in r.AvailableSideDish)
                {
                    RecipeList_RecipeSideDishes.Items.Add(sd);
                }

                RecipeList_RecipeIngredients.Items.Clear();

                foreach (Ingredient i in r.Ingredients)
                {
                    RecipeList_RecipeIngredients.Items.Add(i);
                }
            }
        }

        private void RecipeList_NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm rf = (RecipeForm)uiHandler.GetWindow("window_recipeform");

            uiHandler.WindowOpen("window_recipeform");

            if(rf._Recipe != null)
            {
                dataHandler.Recipes.Add(rf._Recipe);

                _RecipeList();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
            SingleRecipeWindow srw = (SingleRecipeWindow)uiHandler.GetWindow("window_singlerecipe");

            randomizer.RandomRecipe();

            srw.LoadRecipe(randomizer.LastRecipe);

            uiHandler.WindowOpen("window_singlerecipe");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            uiHandler.CloseAllWindows();

            e.Cancel = false;
        }
    }
}
