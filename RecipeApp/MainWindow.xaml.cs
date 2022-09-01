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
        private SettingsWindow _settings;

        private RecipeAppBackend backend;

        private bool newIngredient = false;
        private int editIngredientIndex = -1;

        private bool newInstruction = false;
        private int editInstructionIndex = -1;

        private bool editing = false;
        private Recipe? recipeToBeEdited = null;

        private bool isSettingsOpened = false;

        public MainWindow()
        {
            InitializeComponent();

            _settings = new SettingsWindow();

            backend = new RecipeAppBackend();

            backend.LoadLanguages();
            backend.LoadConfig();
            backend.LoadUnits();
            backend.LoadRecipes();

            backend.AddGrid(Main);
            backend.AddGrid(NewRecipe);

            backend.DrawGrid(Main);

            _settings.backend = backend;
            _settings.LoadLanguages();
            _settings.LoadUI();

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
                if (editing)
                {
                    LoadEditRecipe();
                }
                else
                {
                    LoadNewRecipe();
                }
            }
        }

        private void LoadEditRecipe()
        {
            NewRecipeIngredientsList.Items.Clear();
            NewRecipeIngredientDescription.Text = "";
            NewRecipeIngredientMeasurement.Text = "";
            NewRecipeIngredientName.Text = "";
            NewRecipeIngredientUnit.Text = "";
            NewRecipeInstructionsList.Items.Clear();
            NewRecipeInstructionText.Text = "";

            btn_newrecipe_save_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;

            NewRecipeInstructionText.IsEnabled = false;

            foreach(RecipeIngredient ri in recipeToBeEdited.Ingredients)
            {
                NewRecipeIngredientsList.Items.Add(ri);
            }

            foreach(string ri in recipeToBeEdited.Instructions)
            {
                NewRecipeInstructionsList.Items.Add(ri);
            }

            if(NewRecipeIngredientsList.Items.Count > 0)
            {
                NewRecipeIngredientsList.SelectedIndex = 0;
            }

            if(NewRecipeInstructionsList.Items.Count > 0)
            {
                NewRecipeInstructionsList.SelectedIndex = 0;
            }

            NewRecipeName.Text = recipeToBeEdited.Name;
            NewRecipePortionCount.Text = recipeToBeEdited.PortionCount.ToString();

            btn_newrecipe_save_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;

            NewRecipeIngredientName.IsEnabled = false;
            NewRecipeIngredientMeasurement.IsEnabled = false;
            NewRecipeIngredientDescription.IsEnabled = false;
            NewRecipeIngredientUnit.IsEnabled = false;

            foreach (Unit u in backend.mUnits.Units)
            {
                NewRecipeIngredientUnit.Items.Add(u.Name);
            }

            NewRecipeIngredientUnit.SelectedIndex = 0;

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
            lbl_newrecipe_name.Content = backend.GetText("lbl_newrecipe_name") + ":";
            lbl_newrecipe_portions.Content = backend.GetText("lbl_newrecipe_portions") + ":";
            lbl_newrecipe_instructions.Content = backend.GetText("lbl_newrecipe_isntructions") + ":";
            lbl_newrecipe_ingredient_name.Content = backend.GetText("lbl_newrecipe_ingredient_name") + ":";
            lbl_newrecipe_ingredient_description.Content = backend.GetText("lbl_newrecipe_ingredient_description") + ":";
            lbl_newrecipe_ingredient_measurement.Content = backend.GetText("lbl_newrecipe_ingredient_measurement") + ":";
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

            foreach(Unit u in backend.mUnits.Units)
            {
                NewRecipeIngredientUnit.Items.Add(u.Name);
            }

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
            lbl_newrecipe_name.Content = backend.GetText("lbl_newrecipe_name") + ":";
            lbl_newrecipe_portions.Content = backend.GetText("lbl_newrecipe_portions") + ":";
            lbl_newrecipe_instructions.Content = backend.GetText("lbl_newrecipe_isntructions") + ":";
            lbl_newrecipe_ingredient_name.Content = backend.GetText("lbl_newrecipe_ingredient_name") + ":";
            lbl_newrecipe_ingredient_description.Content = backend.GetText("lbl_newrecipe_ingredient_description") + ":";
            lbl_newrecipe_ingredient_measurement.Content = backend.GetText("lbl_newrecipe_ingredient_measurement") + ":";
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
            btn_main_new_recipe.Content = backend.GetText("btn_main_new_recipe");
            btn_main_delete_recipe.Content = backend.GetText("btn_main_delete_recipe");
            btn_main_edit_recipe.Content = backend.GetText("btn_main_edit_recipe");
            btn_main_settings.Content = backend.GetText("btn_main_settings");
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
                string unit = ri.MeasurementUnit;

                NewRecipeIngredientName.Text = name;
                NewRecipeIngredientDescription.Text = description;
                NewRecipeIngredientMeasurement.Text = measurement.ToString();
                NewRecipeIngredientUnit.SelectedItem = unit;

                btn_newrecipe_delete_ingredient.IsEnabled = true;
                btn_newrecipe_edit_ingredient.IsEnabled = true;
            }

            if (NewRecipeInstructionsList.Items.Count > 0 && NewRecipeInstructionsList.SelectedIndex < 0)
            {
                NewRecipeInstructionsList.SelectedIndex = 0;
            }

            if (NewRecipeInstructionsList.SelectedIndex >= 0)
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

                foreach (string ri in r.Instructions)
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

            btn_newrecipe_back.IsEnabled = false;
            btn_newrecipe_save_recipe.IsEnabled = false;
            btn_newrecipe_add_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;
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
                string unit = (string)NewRecipeIngredientUnit.SelectedItem;

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
            
                btn_newrecipe_back.IsEnabled = true;
                btn_newrecipe_save_recipe.IsEnabled = true;
                btn_newrecipe_add_instruction.IsEnabled = true;
                
                UpdateNewRecipe();
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
            string unit = ri.MeasurementUnit;

            NewRecipeIngredientName.Text = name;
            NewRecipeIngredientDescription.Text = description;
            NewRecipeIngredientMeasurement.Text = measurement.ToString();
            NewRecipeIngredientUnit.SelectedItem = unit;

            btn_newrecipe_back.IsEnabled = false;
            btn_newrecipe_save_recipe.IsEnabled = false;
            btn_newrecipe_add_instruction.IsEnabled = false;
            btn_newrecipe_add_instruction.IsEnabled = false;
            btn_newrecipe_edit_instruction.IsEnabled = false;
            btn_newrecipe_delete_instruction.IsEnabled = false;
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

            foreach (string s in NewRecipeInstructionsList.Items)
            {
                instructions.Add(s);
            }

            r = new Recipe(name, portions, ingredients, instructions);

            if (editing)
            {
                recipeToBeEdited.UpdateRecipe(name, portions, ingredients, instructions);

                editing = false;

                backend.UpdateRecipe(recipeToBeEdited);
            }
            else
            {
                backend.SaveRecipe(r);
            }

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
            if (RecipeList.SelectedIndex >= 0)
            {
                int index = RecipeList.SelectedIndex;
                int nextIndex = -1;

                Recipe r = (Recipe)RecipeList.SelectedItem;

                RecipeList.Items.RemoveAt(index);

                if (RecipeList.Items.Count > 0)
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

            btn_newrecipe_back.IsEnabled = false;
            btn_newrecipe_save_recipe.IsEnabled = false;
            btn_newrecipe_add_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
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

            btn_newrecipe_back.IsEnabled = false;
            btn_newrecipe_save_recipe.IsEnabled = false;
            btn_newrecipe_add_ingredient.IsEnabled = false;
            btn_newrecipe_edit_ingredient.IsEnabled = false;
            btn_newrecipe_delete_ingredient.IsEnabled = false;
        }

        private void NewRecipeSaveInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (NewRecipeInstructionText.Text != "")
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

                btn_newrecipe_back.IsEnabled = true;
                btn_newrecipe_save_recipe.IsEnabled = true;
                btn_newrecipe_add_ingredient.IsEnabled = true;

                UpdateNewRecipe();
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

        private void btn_main_edit_recipe_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeList.SelectedIndex >= 0)
            {
                editing = true;
                recipeToBeEdited = (Recipe) RecipeList.SelectedItem;

                backend.DrawGrid(NewRecipe);

                LoadUI();
            }
        }

        private void btn_main_settings_Click(object sender, RoutedEventArgs e)
        {
            if (!isSettingsOpened)
            {
                _settings.Show();
                isSettingsOpened = true;
            }
            else
            {
                if (!_settings.isOpened)
                {
                    _settings = new SettingsWindow();
                    _settings.backend = backend;
                    _settings.LoadLanguages();
                    _settings.LoadUI();

                    isSettingsOpened = false;

                    btn_main_settings_Click(sender, e);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _settings.Close();
            _settings = null;
        }
    }
}
