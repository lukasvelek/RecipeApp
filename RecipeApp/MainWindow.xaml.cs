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

        private bool newIngredient = false;
        private int editIngredientIndex = -1;

        private bool newInstruction = false;
        private int editInstructionIndex = -1;

        public MainWindow()
        {
            InitializeComponent();

            backend = new RecipeAppBackend();

            backend.LoadLanguages();
            backend.LoadRecipes();
            
            backend.AddGrid(Main);
            backend.AddGrid(NewRecipe);

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

            btn_newrecipe_save_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;

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

            NewRecipeInstructionText.Text = "";
            NewRecipeInstructionsList.Items.Clear();

            btn_newrecipe_save_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;

            NewRecipeInstructionText.IsEnabled = false;

            btn_newrecipe_back.Content = backend.GetText("btn_newrecipe_back");
            btn_newrecipe_save_recipe.Content = backend.GetText("btn_newrecipe_save_recipe");
            btn_newrecipe_add_instruction.Content = backend.GetText("btn_newrecipe_add_instruction");
            btn_newrecipe_delete_instruction.Content = backend.GetText("btn_newrecipe_delete_instruction");
            btn_newrecipe_edit_instruction.Content = backend.GetText("btn_newrecipe_edit_instruction");
            btn_newrecipe_save_instruction.Content = backend.GetText("btn_newrecipe_save_instruction");
            btn_newrecipe_add_ingredient.Content = backend.GetText("btn_newrecipe_add_ingredient");
            btn_newrecipe_delete_ingredient.Content = backend.GetText("btn_newrecipe_delete_ingredient");
            btn_newrecipe_save_ingredient.Content = backend.GetText("btn_newrecipe_save_ingredient");
            btn_newrecipe_edit_ingredient.Content = backend.GetText("btn_newrecipe_edit_ingredient");
            lbl_newrecipe_name.Content = backend.GetText("lbl_newrecipe_name");
            lbl_newrecipe_portions.Content = backend.GetText("lbl_newrecipe_portions");
            lbl_newrecipe_instructions.Content = backend.GetText("lbl_newrecipe_isntructions");
            lbl_newrecipe_ingredient_name.Content = backend.GetText("lbl_newrecipe_ingredient_name");
            lbl_newrecipe_ingredient_description.Content = backend.GetText("lbl_newrecipe_ingredient_description");
            lbl_newrecipe_ingredient_measurement.Content = backend.GetText("lbl_newrecipe_ingredient_measurement");
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

            backend.RefreshRecipes();

            lbl_main_ingredients.Content = backend.GetText("lbl_main_ingredients") + ":";
            lbl_main_recipe_name.Content = backend.GetText("lbl_main_recipe_name") + ":";
            lbl_main_portion_count.Content = backend.GetText("lbl_main_portion_count") + ":";
            lbl_main_instructions.Content = backend.GetText("lbl_main_instructions") + ":";
            btn_main_new_recipe.Content = backend.GetText("btn_main_new_recipe") + ":";
            btn_main_delete_recipe.Content = backend.GetText("btn_main_delete_recipe") + ":";
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

                btn_newrecipe_delete_ingredient.IsEnabled = true;
                btn_newrecipe_edit_ingredient.IsEnabled = true;
            }

            if(NewRecipeInstructionsList.Items.Count > 0 && NewRecipeInstructionsList.SelectedIndex < 0)
            {
                NewRecipeInstructionsList.SelectedIndex = 0;
            }

            if(NewRecipeInstructionsList.SelectedIndex >= 0)
            {
                string? instruction = NewRecipeInstructionsList.SelectedItem.ToString();

                NewRecipeInstructionText.Text = instruction;

                btn_newrecipe_edit_instruction.IsEnabled = true;
                btn_newrecipe_delete_instruction.IsEnabled = true;
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
                RecipePortionCount.Content = r.PortionCount;

                foreach (RecipeIngredient ri in r.Ingredients)
                {
                    RecipeIngredientsList.Items.Add(ri.ToString());
                }

                RecipeInstructionsList.Items.Clear();

                foreach(string ri in r.Instructions)
                {
                    RecipeInstructionsList.Items.Add(ri);
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
            btn_newrecipe_add_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
            btn_newrecipe_save_ingredient.IsEnabled = true;

            NewRecipeIngredientDescription.IsEnabled = true;
            NewRecipeIngredientName.IsEnabled = true;
            NewRecipeIngredientMeasurement.IsEnabled = true;
            NewRecipeIngredientUnit.IsEnabled = true;
            NewRecipeIngredientsList.IsEnabled = false;

            NewRecipeIngredientDescription.Text = "";
            NewRecipeIngredientMeasurement.Text = "";
            NewRecipeIngredientName.Text = "";

            newIngredient = true;
        }

        private void NewRecipeSaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (NewRecipeIngredientName.Text != "" &&
               NewRecipeIngredientMeasurement.Text != "")
            {
                btn_newrecipe_add_ingredient.IsEnabled = true;
                btn_newrecipe_edit_ingredient.IsEnabled = true;
                btn_newrecipe_delete_ingredient.IsEnabled = true;
                btn_newrecipe_save_ingredient.IsEnabled = false;

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

                if (newIngredient)
                {
                    NewRecipeIngredientsList.Items.Add(ri);
                    newIngredient = false;
                }
                else
                {
                    NewRecipeIngredientsList.Items[editIngredientIndex] = ri;
                    NewRecipeIngredientsList.SelectedIndex = editIngredientIndex;
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
            editIngredientIndex = index;

            btn_newrecipe_add_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
            btn_newrecipe_save_ingredient.IsEnabled = true;

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
            int portions = Convert.ToInt32(NewRecipePortionCount.Text);
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
            List<string> instructions = new List<string>();

            foreach (RecipeIngredient f in NewRecipeIngredientsList.Items)
            {
                ingredients.Add(f);
            }

            foreach(string s in NewRecipeInstructionsList.Items)
            {
                instructions.Add(s);
            }

            r = new Recipe(name, portions, ingredients, instructions);

            backend.SaveRecipe(r);

            System.Threading.Thread.Sleep(1000);

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

        private void NewRecipeAddInstruction_Click(object sender, RoutedEventArgs e)
        {
            btn_newrecipe_add_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;
            btn_newrecipe_save_instruction.IsEnabled = true;

            NewRecipeInstructionText.IsEnabled = true;
            NewRecipeInstructionsList.IsEnabled = false;

            NewRecipeInstructionText.Text = "";

            newInstruction = true;
        }

        private void NewRecipeEditInstruction_Click(object sender, RoutedEventArgs e)
        {
            int index = NewRecipeInstructionsList.SelectedIndex;
            editInstructionIndex = index;

            btn_newrecipe_add_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;
            btn_newrecipe_save_instruction.IsEnabled = true;

            NewRecipeInstructionText.IsEnabled = true;
            NewRecipeInstructionsList.IsEnabled = false;

            string? s = NewRecipeInstructionsList.SelectedItem.ToString();

            NewRecipeInstructionText.Text = s;
        }

        private void NewRecipeSaveInstruction_Click(object sender, RoutedEventArgs e)
        {
            if(NewRecipeInstructionText.Text != "")
            {
                btn_newrecipe_add_instruction.IsEnabled = true;
                btn_newrecipe_edit_instruction.IsEnabled = true;
                btn_newrecipe_delete_instruction.IsEnabled = true;
                btn_newrecipe_save_instruction.IsEnabled = false;

                NewRecipeInstructionText.IsEnabled = false;
                NewRecipeInstructionsList.IsEnabled = true;

                string text = NewRecipeInstructionText.Text;

                if (newInstruction)
                {
                    NewRecipeInstructionsList.Items.Add(text);
                }
                else
                {
                    NewRecipeInstructionsList.Items[editInstructionIndex] = text;
                    NewRecipeIngredientsList.SelectedIndex = editInstructionIndex;
                }
            }
        }

        private void NewRecipeDeleteInstruction_Click(object sender, RoutedEventArgs e)
        {
            int index = NewRecipeInstructionsList.SelectedIndex;
            int n = 0;

            if (index >= 0)
            {
                NewRecipeInstructionsList.Items.RemoveAt(index);
            }

            if (NewRecipeInstructionsList.Items.Count > 0)
            {
                if (index == 0)
                {
                    n = 0;
                }
                else if (index == (NewRecipeInstructionsList.Items.Count - 1))
                {
                    n = index - 1;
                }
                else
                {
                    n = NewRecipeInstructionsList.Items.Count - 1;
                }

                NewRecipeInstructionsList.SelectedIndex = n;
            }
        }

        private void NewRecipeInstructionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNewRecipe();
        }
    }
}
