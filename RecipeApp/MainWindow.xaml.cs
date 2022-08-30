using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecipeAppBackend backend;

        private bool newRecipe = false;
        private int editRecipeIndex = -1;

        public MainWindow()
        {
            InitializeComponent();

            backend = new RecipeAppBackend();

            backend.AddGrid(Main);
            backend.AddGrid(NewRecipe);

            backend.LoadRecipes();
            backend.DrawGrid(Main);

            LoadUI();
        }

        private void LoadUI()
        {
            if (backend.currentGrid == Main)
            {
                LoadMain();
            }
            else if (backend.currentGrid == NewRecipe)
            {
                LoadNewRecipe();
            }

        }

        private void LoadNewRecipe()
        {
            NewRecipeIngredientsList.Items.Clear();
            NewRecipeIngredientDescription.Text = "";
            NewRecipeIngredientMeasurement.Text = "";
            NewRecipeIngredientName.Text = "";
            NewRecipeIngredientUnit.Text = "";

            NewRecipeSaveIngredient.IsEnabled = false;
            NewRecipeDeleteIngredient.IsEnabled = false;
            NewRecipeEditIngredient.IsEnabled = false;

            NewRecipeIngredientName.IsEnabled = false;
            NewRecipeIngredientMeasurement.IsEnabled = false;
            NewRecipeIngredientDescription.IsEnabled = false;
            NewRecipeIngredientUnit.IsEnabled = false;

            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Pinch);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Grams);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Kilograms);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Ounces);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Pounds);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Milliliters);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Liters);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Teaspoons);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Tablespoons);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Cups);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Fluid_Ounces);
            NewRecipeIngredientUnit.Items.Add(MeasurementUnits.Units.Gallons);

            NewRecipeIngredientUnit.SelectedIndex = 0;
        }

        private void LoadMain()
        {
            RecipeList.Items.Clear();
            RecipeIngredientsList.Items.Clear();
            RecipeName.Content = "";

            foreach (Recipe r in backend.Recipes)
            {
                RecipeList.Items.Add(r);
            }

            if (RecipeList.Items.Count > 0)
            {
                RecipeList.SelectedIndex = 0;
            }
        }

        private void UpdateNewRecipe()
        {
            if (NewRecipeIngredientsList.Items.Count > 0 && NewRecipeIngredientsList.SelectedIndex < 0)
            {
                NewRecipeIngredientsList.SelectedIndex = 0;
            }

            if (NewRecipeIngredientsList.SelectedIndex >= 0)
            {
                RecipeIngredient ri = (RecipeIngredient)NewRecipeIngredientsList.SelectedItem;

                string name = ri.Name;
                string description = ri.Description;
                double measurement = ri.Measurement;
                MeasurementUnits.Units unit = ri.MeasurementUnit;

                NewRecipeIngredientName.Text = name;
                NewRecipeIngredientDescription.Text = description;
                NewRecipeIngredientMeasurement.Text = measurement.ToString();
                NewRecipeIngredientUnit.SelectedItem = unit;

                NewRecipeDeleteIngredient.IsEnabled = true;
                NewRecipeEditIngredient.IsEnabled = true;
            }
        }

        private void RecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = RecipeList.SelectedIndex;

            if (index >= 0)
            {
                Recipe? r = backend.GetRecipeByIndex(index);

                RecipeIngredientsList.Items.Clear();

                RecipeName.Content = r.Name;

                foreach (RecipeIngredient ri in r.Ingredients)
                {
                    RecipeIngredientsList.Items.Add(ri.ToString());
                }
            }
        }

        private void NewRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            backend.DrawGrid(NewRecipe);

            LoadUI();
        }

        private void NewRecipeAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            NewRecipeAddIngredient.IsEnabled = false;
            NewRecipeEditIngredient.IsEnabled = false;
            NewRecipeDeleteIngredient.IsEnabled = false;
            NewRecipeSaveIngredient.IsEnabled = true;

            NewRecipeIngredientDescription.IsEnabled = true;
            NewRecipeIngredientName.IsEnabled = true;
            NewRecipeIngredientMeasurement.IsEnabled = true;
            NewRecipeIngredientUnit.IsEnabled = true;
            NewRecipeIngredientsList.IsEnabled = false;

            NewRecipeIngredientDescription.Text = "";
            NewRecipeIngredientMeasurement.Text = "";
            NewRecipeIngredientName.Text = "";

            newRecipe = true;
        }

        private void NewRecipeSaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (NewRecipeIngredientName.Text != "" &&
               NewRecipeIngredientMeasurement.Text != "")
            {
                NewRecipeAddIngredient.IsEnabled = true;
                NewRecipeEditIngredient.IsEnabled = true;
                NewRecipeDeleteIngredient.IsEnabled = true;
                NewRecipeSaveIngredient.IsEnabled = false;

                NewRecipeIngredientDescription.IsEnabled = false;
                NewRecipeIngredientName.IsEnabled = false;
                NewRecipeIngredientMeasurement.IsEnabled = false;
                NewRecipeIngredientUnit.IsEnabled = false;
                NewRecipeIngredientsList.IsEnabled = true;

                string name = NewRecipeIngredientName.Text;
                string description;

                if (NewRecipeIngredientDescription.Text == null)
                {
                    description = "null";
                }
                else
                {
                    description = NewRecipeIngredientDescription.Text;
                }

                double measurement = Convert.ToDouble(NewRecipeIngredientMeasurement.Text);
                MeasurementUnits.Units unit = (MeasurementUnits.Units)NewRecipeIngredientUnit.SelectedItem;

                RecipeIngredient ri = new RecipeIngredient(name, description, measurement, unit);

                if (newRecipe)
                {
                    NewRecipeIngredientsList.Items.Add(ri);
                    newRecipe = false;
                }
                else
                {
                    NewRecipeIngredientsList.Items[editRecipeIndex] = ri;
                    NewRecipeIngredientsList.SelectedIndex = editRecipeIndex;
                }
            }
        }

        private void NewRecipeIngredientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNewRecipe();
        }

        private void NewRecipeDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            int index = NewRecipeIngredientsList.SelectedIndex;
            int n = 0;

            if (index >= 0)
            {
                NewRecipeIngredientsList.Items.RemoveAt(index);
            }

            if (NewRecipeIngredientsList.Items.Count > 0)
            {
                if (index == 0)
                {
                    n = 0;
                }
                else if (index == (NewRecipeIngredientsList.Items.Count - 1))
                {
                    n = index - 1;
                }
                else
                {
                    n = NewRecipeIngredientsList.Items.Count - 1;
                }

                NewRecipeIngredientsList.SelectedIndex = n;
            }
        }

        private void NewRecipeEditIngredient_Click(object sender, RoutedEventArgs e)
        {
            int index = NewRecipeIngredientsList.SelectedIndex;
            editRecipeIndex = index;

            NewRecipeAddIngredient.IsEnabled = false;
            NewRecipeEditIngredient.IsEnabled = false;
            NewRecipeDeleteIngredient.IsEnabled = false;
            NewRecipeSaveIngredient.IsEnabled = true;

            NewRecipeIngredientDescription.IsEnabled = true;
            NewRecipeIngredientName.IsEnabled = true;
            NewRecipeIngredientMeasurement.IsEnabled = true;
            NewRecipeIngredientUnit.IsEnabled = true;
            NewRecipeIngredientsList.IsEnabled = false;

            RecipeIngredient? ri = (RecipeIngredient)NewRecipeIngredientsList.SelectedItem;

            string name = ri.Name;
            string description = ri.Description;
            double measurement = ri.Measurement;
            MeasurementUnits.Units unit = ri.MeasurementUnit;

            NewRecipeIngredientName.Text = name;
            NewRecipeIngredientDescription.Text = description;
            NewRecipeIngredientMeasurement.Text = measurement.ToString();
            NewRecipeIngredientUnit.SelectedItem = unit;
        }

        private void NewRecipeSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe? r;

            string name = NewRecipeName.Text;
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();

            foreach (RecipeIngredient f in NewRecipeIngredientsList.Items)
            {
                ingredients.Add(f);
            }

            r = new Recipe(name, ingredients);

            backend.SaveRecipe(r);

            backend.DrawGrid(Main);

            LoadUI();
        }

        private void NewRecipeBack_Click(object sender, RoutedEventArgs e)
        {
            backend.DrawGrid(Main);

            LoadUI();
        }

        private void DeleteRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeList.SelectedIndex >= 0)
            {
                int index = RecipeList.SelectedIndex;
                int nextIndex = -1;

                Recipe r = (Recipe)RecipeList.SelectedItem;

                RecipeList.Items.RemoveAt(index);

                if(RecipeList.Items.Count > 0)
                {
                    if (index == 0)
                    {
                        nextIndex = 0;
                    }
                    else if (index == (RecipeList.Items.Count - 1))
                    {
                        nextIndex = index - 1;
                    }
                    else
                    {
                        nextIndex = RecipeList.Items.Count - 1;
                    }

                    RecipeList.SelectedIndex = nextIndex;
                }

                backend.DeleteRecipe(r);
            }
        }
    }
}
