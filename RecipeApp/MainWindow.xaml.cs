using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private const string VERSION = "3.0 beta";

        private static UIHandler uiHandler = new UIHandler();
        private static DataHandler dataHandler = new DataHandler();
        private static Randomizer randomizer = new Randomizer();
        private static LanguageHandler langHandler = new LanguageHandler();

        public MainWindow()
        {
            InitializeComponent();

            // Grid initialization
            uiHandler.AddGrid(RecipeMain, "ui_main_menu");
            uiHandler.AddGrid(RecipeList, "ui_recipe_list");

            uiHandler.HideAllGrids();


            // Data initialization
            dataHandler.LoadRecipes();
            dataHandler.LoadConfig();
            randomizer.Shuffle(dataHandler.Recipes);


            // Initial configuration
            Window.Title = "Recepty " + VERSION;
            Window.ResizeMode = ResizeMode.CanMinimize;

            uiHandler.ShowGrid("ui_main_menu");

            langHandler.Initialize(dataHandler.LANGUAGE);

            Translate(langHandler);
        }

        private void Translate(LanguageHandler lh)
        {
            OpenRecipeList.Content = lh.MAINWINDOW_MANAGE_RECIPES;
            RecipeRandom_Generate.Content = lh.MAINWINDOW_GENERATE_RANDOM_RECIPE;
            Settings.Content = lh.MAINWINDOW_SETTINGS;
            RecipeList_Back.Content = lh.GENERAL_BACK;
            RecipeList_DeleteRecipe.Content = lh.GENERAL_DELETE;
            RecipeList_EditRecipe.Content = lh.GENERAL_EDIT;
            RecipeList_NewRecipe.Content = lh.GENERAL_NEW;
            RecipeList_OpenRecipe.Content = lh.GENERAL_OPEN;

            Title = lh.MAIN_WINDOW_TITLE;
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

            rf.Translate(langHandler);
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
            dataHandler.SaveConfig();

            Application.Current.Shutdown();
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
            if (RecipeList_Recipes.SelectedItem != null)
            {
                SingleRecipeWindow srw = new SingleRecipeWindow();

                srw.Translate(langHandler);
                srw.LoadRecipe((Recipe.Recipe)RecipeList_Recipes.SelectedItem);
                srw.Show();
            }
        }

        private void RecipeList_EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList_Recipes.SelectedItem != null)
            {
                RecipeForm rf = new RecipeForm();

                rf.Translate(langHandler);
                rf.LoadEditRecipe((Recipe.Recipe)RecipeList_Recipes.SelectedItem);
                rf.ShowDialog();

                if (rf._Recipe != null)
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();

            sw.Translate(langHandler);
            sw.ShowDialog();
            dataHandler.LANGUAGE = sw.language;
        }
    }
}
