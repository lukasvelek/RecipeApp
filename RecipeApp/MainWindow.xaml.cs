using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
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

        bool newIngredient = false;
        bool newSideDish = false;

        bool isIngredientFormActive = false;
        bool isSideDishFormActive = false;

        public MainWindow()
        {
            InitializeComponent();


            // Object initialization


            // Grid initialization
            uiHandler.AddGrid(RecipeMain, "ui_main_menu");
            uiHandler.AddGrid(RecipeList, "ui_recipe_list");
            uiHandler.AddGrid(RecipeNew, "ui_recipe_new");
            uiHandler.AddGrid(RecipeRandom, "ui_recipe_random");

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

                RecipeList_RecipeName.Content = "";
                RecipeList_RecipeNote.Content = "";
                RecipeList_RecipeServings.Content = "";
                RecipeList_RecipeTimeNeeded.Content = "";
                RecipeList_RecipeIngredients.Items.Clear();
                RecipeList_RecipeSideDishes.Items.Clear();
            }
        }

        private void _RecipeNew()
        {
            RecipeNew_RecipeServingsSlider.Value = 1;
            RecipeNew_RecipeServings.Content = "1";

            RecipeNew_IngredientsList_Units.Items.Clear();

            RecipeNew_IngredientsList_Units.Items.Add("g");
            RecipeNew_IngredientsList_Units.Items.Add("kg");
            RecipeNew_IngredientsList_Units.Items.Add("ml");
            RecipeNew_IngredientsList_Units.Items.Add("l");
            RecipeNew_IngredientsList_Units.Items.Add("ks");
            RecipeNew_IngredientsList_Units.Items.Add("špetka");

            RecipeNew_IngredientsList_Units.SelectedIndex = 0;

            RecipeNew_IngredientsList_Name.IsEnabled = false;
            RecipeNew_IngredientsList_Volume.IsEnabled = false;
            RecipeNew_IngredientsList_Units.IsEnabled = false;

            RecipeNew_IngredientsList_DeleteIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_EditIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_SaveIngredient.IsEnabled = false;

            RecipeNew_SideDishList_Name.IsEnabled = false;
            RecipeNew_SideDishList_DeleteSideDish.IsEnabled = false;
            RecipeNew_SideDishList_EditSideDish.IsEnabled = false;
            RecipeNew_SideDishList_SaveSideDish.IsEnabled = false;
            RecipeNew_SideDishList_NewSideDish.IsEnabled = true;
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
            uiHandler.ShowGrid("ui_recipe_new");

            _RecipeNew();
        }

        private void RecipeNew_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipe_list");

            _RecipeList();
        }

        private void RecipeNew_RecipeServingsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (RecipeNew_RecipeServingsSlider.Value >= 1 && RecipeNew_RecipeServingsSlider.Value <= 8)
            {
                if (RecipeNew_RecipeServings != null)
                {
                    RecipeNew_RecipeServings.Content = Convert.ToInt32(RecipeNew_RecipeServingsSlider.Value);
                }
            }
        }

        private void RecipeNew_IngredientsList_NewIngredient_Click(object sender, RoutedEventArgs e)
        {
            RecipeNew_IngredientsList_Name.Text = "";
            RecipeNew_IngredientsList_Volume.Text = "";
            RecipeNew_IngredientsList_Units.SelectedIndex = 0;

            RecipeNew_IngredientsList_Name.IsEnabled = true;
            RecipeNew_IngredientsList_Units.IsEnabled = true;
            RecipeNew_IngredientsList_Volume.IsEnabled = true;

            RecipeNew_IngredientsList_NewIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_DeleteIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_EditIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_SaveIngredient.IsEnabled = true;
            RecipeNew_IngredientsList.IsEnabled = false;

            newIngredient = true;

            isIngredientFormActive = true;
        }

        private void RecipeNew_IngredientsList_SaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (newIngredient)
            {
                string name = RecipeNew_IngredientsList_Name.Text;
                int count = Convert.ToInt32(RecipeNew_IngredientsList_Volume.Text);
                string unit = RecipeNew_IngredientsList_Units.Text;

                Ingredient i = new Ingredient(name, count, unit);

                RecipeNew_IngredientsList.Items.Add(i);

                RecipeNew_IngredientsList.SelectedIndex = RecipeNew_IngredientsList.Items.Count - 1; // selects last item added
            }
            else
            {
                int index = RecipeNew_IngredientsList.SelectedIndex;

                string name = RecipeNew_IngredientsList_Name.Text;
                int value = Convert.ToInt32(RecipeNew_IngredientsList_Volume.Text);
                string units = RecipeNew_IngredientsList_Units.Text;

                Ingredient ni = new Ingredient(name, value, units);

                RecipeNew_IngredientsList.Items[index] = ni;
            }

            RecipeNew_IngredientsList_Name.IsEnabled = false;
            RecipeNew_IngredientsList_Units.IsEnabled = false;
            RecipeNew_IngredientsList_Volume.IsEnabled = false;

            RecipeNew_IngredientsList_NewIngredient.IsEnabled = true;
            RecipeNew_IngredientsList_DeleteIngredient.IsEnabled = true;
            RecipeNew_IngredientsList_EditIngredient.IsEnabled = true;
            RecipeNew_IngredientsList_SaveIngredient.IsEnabled = false;
            RecipeNew_IngredientsList.IsEnabled = true;

            newIngredient = false;

            isIngredientFormActive = false;
        }

        private void RecipeNew_IngredientsList_DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeNew_IngredientsList.SelectedIndex >= 0)
            {
                Ingredient i = (Ingredient)RecipeNew_IngredientsList.SelectedItem;
                int index = RecipeNew_IngredientsList.SelectedIndex;

                if (RecipeNew_IngredientsList.Items.Count > 1)
                {
                    if (index - 1 >= 0)
                    {
                        RecipeNew_IngredientsList.SelectedIndex = index - 1;
                    }
                    else
                    {
                        RecipeNew_IngredientsList.SelectedIndex = 0;
                    }
                }
                else
                {
                    RecipeNew_IngredientsList.SelectedIndex = -1;
                }

                RecipeNew_IngredientsList.Items.Remove(i);
            }
        }

        private void RecipeNew_IngredientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeNew_IngredientsList.Items.Count == 0)
            {
                RecipeNew_IngredientsList_DeleteIngredient.IsEnabled = false;
                RecipeNew_IngredientsList_SaveIngredient.IsEnabled = false;
                RecipeNew_IngredientsList_EditIngredient.IsEnabled = false;

                RecipeNew_IngredientsList_Name.Text = "";
                RecipeNew_IngredientsList_Volume.Text = "";
                RecipeNew_IngredientsList_Units.SelectedIndex = 0;
            }
        }

        private void RecipeNew_IngredientsList_EditIngredient_Click(object sender, RoutedEventArgs e)
        {
            RecipeNew_IngredientsList_Name.IsEnabled = true;
            RecipeNew_IngredientsList_Units.IsEnabled = true;
            RecipeNew_IngredientsList_Volume.IsEnabled = true;

            RecipeNew_IngredientsList_NewIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_DeleteIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_EditIngredient.IsEnabled = false;
            RecipeNew_IngredientsList_SaveIngredient.IsEnabled = true;
            RecipeNew_IngredientsList.IsEnabled = false;

            newIngredient = false;

            isIngredientFormActive = true;
        }

        private void RecipeNew_SideDishList_NewSideDish_Click(object sender, RoutedEventArgs e)
        {
            RecipeNew_SideDishList_Name.Text = "";

            RecipeNew_SideDishList.IsEnabled = false;

            RecipeNew_SideDishList_Name.IsEnabled = true;
            RecipeNew_SideDishList_DeleteSideDish.IsEnabled = false;
            RecipeNew_SideDishList_EditSideDish.IsEnabled = false;
            RecipeNew_SideDishList_NewSideDish.IsEnabled = false;
            RecipeNew_SideDishList_SaveSideDish.IsEnabled = true;

            newSideDish = true;

            isSideDishFormActive = true;
        }

        private void RecipeNew_SideDishList_SaveSideDish_Click(object sender, RoutedEventArgs e)
        {
            if (newSideDish)
            {
                string name = RecipeNew_SideDishList_Name.Text;

                SideDish sd = new SideDish(name);

                RecipeNew_SideDishList.Items.Add(sd);

                RecipeNew_SideDishList.SelectedIndex = RecipeNew_SideDishList.Items.Count - 1;
            }
            else
            {
                int index = RecipeNew_SideDishList.SelectedIndex;

                SideDish sd = new SideDish(RecipeNew_SideDishList_Name.Text);

                RecipeNew_SideDishList.Items[index] = sd;
            }

            RecipeNew_SideDishList_Name.IsEnabled = false;

            RecipeNew_SideDishList_NewSideDish.IsEnabled = true;
            RecipeNew_SideDishList_DeleteSideDish.IsEnabled = true;
            RecipeNew_SideDishList_EditSideDish.IsEnabled = true;
            RecipeNew_SideDishList_SaveSideDish.IsEnabled = false;
            RecipeNew_SideDishList.IsEnabled = true;

            newSideDish = false;

            isSideDishFormActive = false;
        }

        private void RecipeNew_SideDishList_EditSideDish_Click(object sender, RoutedEventArgs e)
        {
            RecipeNew_SideDishList.IsEnabled = false;

            RecipeNew_SideDishList_Name.IsEnabled = true;
            RecipeNew_SideDishList_DeleteSideDish.IsEnabled = false;
            RecipeNew_SideDishList_EditSideDish.IsEnabled = false;
            RecipeNew_SideDishList_NewSideDish.IsEnabled = false;
            RecipeNew_SideDishList_SaveSideDish.IsEnabled = true;

            newSideDish = false;

            isSideDishFormActive = true;
        }

        private void RecipeNew_SideDishList_DeleteSideDish_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeNew_SideDishList.SelectedIndex >= 0)
            {
                SideDish sd = (SideDish)RecipeNew_SideDishList.SelectedItem;
                int index = RecipeNew_SideDishList.SelectedIndex;

                if (RecipeNew_SideDishList.Items.Count > 1)
                {
                    if (index - 1 >= 0)
                    {
                        RecipeNew_SideDishList.SelectedIndex = index - 1;
                    }
                    else
                    {
                        RecipeNew_SideDishList.SelectedIndex = 0;
                    }
                }
                else
                {
                    RecipeNew_SideDishList.SelectedIndex = -1;
                }

                RecipeNew_SideDishList.Items.Remove(sd);
            }
        }

        private void RecipeNew_SideDishList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeNew_SideDishList.Items.Count == 0)
            {
                RecipeNew_SideDishList_DeleteSideDish.IsEnabled = false;
                RecipeNew_SideDishList_SaveSideDish.IsEnabled = false;
                RecipeNew_SideDishList_EditSideDish.IsEnabled = false;

                RecipeNew_SideDishList_Name.Text = "";
            }
        }

        private void RecipeNew_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!isSideDishFormActive && !isIngredientFormActive)
            {
                string recipeName = "";
                string recipeNote = "";
                int recipeServings = 0;
                int recipeTimeNeededMinutes = 0;

                if (RecipeNew_RecipeName.Text != "")
                {
                    recipeName = RecipeNew_RecipeName.Text;
                }

                if (RecipeNew_RecipeNote.Text != "")
                {
                    recipeNote = RecipeNew_RecipeNote.Text;
                }

                if (RecipeNew_RecipeTimeNeeded.Text != "")
                {
                    recipeTimeNeededMinutes = Convert.ToInt32(RecipeNew_RecipeTimeNeeded.Text);
                }

                recipeServings = Convert.ToInt32(RecipeNew_RecipeServings.Content);

                List<Ingredient> ingredients = new List<Ingredient>();
                List<SideDish> sideDishes = new List<SideDish>();

                foreach (Ingredient i in RecipeNew_IngredientsList.Items)
                {
                    ingredients.Add(i);
                }

                foreach (SideDish sd in RecipeNew_SideDishList.Items)
                {
                    sideDishes.Add(sd);
                }

                Recipe.Recipe recipe = new Recipe.Recipe(recipeName, recipeNote, recipeServings, recipeTimeNeededMinutes, ingredients, sideDishes);

                dataHandler.Recipes.Add(recipe);

                RecipeNew_RecipeName.Text = "";
                RecipeNew_RecipeNote.Text = "";
                RecipeNew_RecipeTimeNeeded.Text = "";
                RecipeNew_IngredientsList.Items.Clear();
                RecipeNew_SideDishList.Items.Clear();
                RecipeNew_RecipeServings.Content = "";
                RecipeNew_RecipeServingsSlider.Value = 1;

                uiHandler.ShowGrid("ui_recipe_list");

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
            SingleRecipeWindow srw = new SingleRecipeWindow();

            randomizer.RandomRecipe();

            srw.LoadRecipe(randomizer.LastRecipe);
            srw.Show();
        }
    }
}
