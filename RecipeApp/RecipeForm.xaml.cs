using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RecipeApp
{
    public partial class RecipeForm : Window
    {
        private static UIHandler uiHandler = new UIHandler();

        public Recipe.Recipe? _Recipe = null;

        private List<Ingredient> Ingredients = new List<Ingredient>();
        private List<SideDish> SideDishes = new List<SideDish>();

        private string name = "";
        private string note = "";

        private int servings = 0;
        private int timeNeeded = 0;

        private bool newIngredient = false;
        private bool newSideDish = false;

        private bool _enableClose = false;

        public RecipeForm()
        {
            InitializeComponent();

            // Grid initialization
            uiHandler.AddGrid(RecipeForm_Main, "ui_recipeform_main");
            uiHandler.AddGrid(RecipeForm_Ingredients, "ui_recipeform_ingredients");
            uiHandler.AddGrid(RecipeForm_SideDishes, "ui_recipeform_sidedishes");

            uiHandler.HideAllGrids();


            uiHandler.ShowGrid("ui_recipeform_main");

            RecipeForm_Main_ServingsText.Content = "1";
            RecipeForm_Main_TimeNeededText.Content = "10 minut";
        }

        private void _RecipeForm()
        {
            RecipeForm_Main_IngredientsList.Items.Clear();
            RecipeForm_Main_SideDishesList.Items.Clear();

            RecipeForm_Main_TimeNeededSlider.Value = timeNeeded;
            RecipeForm_Main_ServingsSlider.Value = servings;

            RecipeForm_Main_Name.Text = name;
            RecipeForm_Main_Note.Text = note;

            foreach (Ingredient i in Ingredients)
            {
                RecipeForm_Main_IngredientsList.Items.Add(i);
            }

            foreach (SideDish sd in SideDishes)
            {
                RecipeForm_Main_SideDishesList.Items.Add(sd);
            }
        }

        private void _SideDishes()
        {
            RecipeForm_SideDishes_List.Items.Clear();

            RecipeForm_SideDishes_Name.Text = "";

            RecipeForm_SideDishes_Edit.IsEnabled = false;
            RecipeForm_SideDishes_Save.IsEnabled = false;
            RecipeForm_SideDishes_Delete.IsEnabled = false;

            RecipeForm_SideDishes_Name.IsEnabled = false;

            if (SideDishes.Count > 0)
            {
                foreach (SideDish sd in SideDishes)
                {
                    RecipeForm_SideDishes_List.Items.Add(sd);
                }
            }

            if (RecipeForm_SideDishes_List.Items.Count > 0)
            {
                RecipeForm_SideDishes_List.SelectedIndex = 0;

                SideDish sd = (SideDish)RecipeForm_SideDishes_List.SelectedItem;

                string text = sd.Name;

                RecipeForm_SideDishes_Name.Text = text;

                RecipeForm_SideDishes_Delete.IsEnabled = true;
                RecipeForm_SideDishes_Edit.IsEnabled = true;
            }
        }

        private void _Ingredients()
        {
            RecipeForm_Ingredients_List.Items.Clear();
            RecipeForm_Ingredients_Units.Items.Clear();

            RecipeForm_Ingredients_Value.Text = "";
            RecipeForm_Ingredients_Name.Text = "";

            RecipeForm_Ingredients_Units.Items.Add("g");
            RecipeForm_Ingredients_Units.Items.Add("kg");
            RecipeForm_Ingredients_Units.Items.Add("ml");
            RecipeForm_Ingredients_Units.Items.Add("l");
            RecipeForm_Ingredients_Units.Items.Add("ks");
            RecipeForm_Ingredients_Units.Items.Add("špetka");

            RecipeForm_Ingredients_Units.SelectedIndex = 0;

            RecipeForm_Ingredients_Edit.IsEnabled = false;
            RecipeForm_Ingredients_Save.IsEnabled = false;
            RecipeForm_Ingredients_Delete.IsEnabled = false;

            RecipeForm_Ingredients_Name.IsEnabled = false;
            RecipeForm_Ingredients_Value.IsEnabled = false;
            RecipeForm_Ingredients_Units.IsEnabled = false;

            if (Ingredients.Count > 0)
            {
                foreach (Ingredient i in Ingredients)
                {
                    RecipeForm_Ingredients_List.Items.Add(i);
                }
            }

            if (RecipeForm_Ingredients_List.Items.Count > 0)
            {
                RecipeForm_Ingredients_List.SelectedIndex = 0;

                Ingredient i = (Ingredient)RecipeForm_Ingredients_List.SelectedItem;

                string name = i.Name;
                string unit = i.Units;
                int value = i.Value;

                RecipeForm_Ingredients_Name.Text = name;
                RecipeForm_Ingredients_Value.Text = value.ToString();

                for (int ix = 0; ix < RecipeForm_Ingredients_Units.Items.Count; ix++)
                {
                    if ((string)RecipeForm_Ingredients_Units.Items[ix] == unit)
                    {
                        RecipeForm_Ingredients_Units.SelectedIndex = ix;

                        break;
                    }
                }

                RecipeForm_Ingredients_Delete.IsEnabled = true;
                RecipeForm_Ingredients_Edit.IsEnabled = true;
            }
        }

        private void RecipeForm_Main_ServingsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = RecipeForm_Main_ServingsSlider.Value;
            string text = "1";

            if (value == 11)
            {
                text = "10+";
            }
            else
            {
                text = value.ToString();
            }

            if (RecipeForm_Main_ServingsText != null)
            {
                RecipeForm_Main_ServingsText.Content = text;
            }

            servings = Convert.ToInt32(value);
        }

        private void RecipeForm_Main_Save_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeForm_Main_Name.Text != "" &&
               RecipeForm_Main_IngredientsList.Items.Count > 0)
            {
                Recipe.Recipe r;

                string name = RecipeForm_Main_Name.Text;
                string note = RecipeForm_Main_Note.Text;
                string servings = (string)RecipeForm_Main_ServingsText.Content;
                string timeNeeded = RecipeForm_Main_TimeNeededText.Content.ToString().Split(' ')[0];

                List<Ingredient> ingredients = new List<Ingredient>();
                List<SideDish> sideDishes = new List<SideDish>();

                foreach (var item in RecipeForm_Main_IngredientsList.Items)
                {
                    Ingredient i = (Ingredient)item;

                    ingredients.Add(i);
                }

                foreach (var item in RecipeForm_Main_SideDishesList.Items)
                {
                    SideDish sd = (SideDish)item;

                    sideDishes.Add(sd);
                }

                r = new Recipe.Recipe(name, note, servings, timeNeeded, ingredients, sideDishes);

                _Recipe = r;
            }

            Hide();
        }

        private void RecipeForm_Main_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void RecipeForm_Main_TimeNeededSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = RecipeForm_Main_TimeNeededSlider.Value;
            string text = "10 minut";

            if (value == 250)
            {
                text = "240+ minut";
            }
            else
            {
                text = value.ToString() + " minut";
            }

            if (RecipeForm_Main_TimeNeededText != null)
            {
                RecipeForm_Main_TimeNeededText.Content = text;
            }

            timeNeeded = Convert.ToInt32(value);
        }

        private void RecipeForm_Main_SideDishesManage_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipeform_sidedishes");

            _SideDishes();
        }

        private void RecipeForm_SideDishes_New_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm_SideDishes_Delete.IsEnabled = false;
            RecipeForm_SideDishes_New.IsEnabled = false;
            RecipeForm_SideDishes_Edit.IsEnabled = false;
            RecipeForm_SideDishes_List.IsEnabled = false;
            RecipeForm_SideDishes_Back.IsEnabled = false;
            RecipeForm_SideDishes_Save.IsEnabled = true;
            RecipeForm_SideDishes_Name.IsEnabled = true;

            RecipeForm_SideDishes_Name.Text = "";

            newSideDish = true;
        }

        private void RecipeForm_SideDishes_Save_Click(object sender, RoutedEventArgs e)
        {
            if (newSideDish)
            {
                string name = RecipeForm_SideDishes_Name.Text;

                SideDishes.Add(new SideDish(name));
            }
            else
            {
                string name = RecipeForm_SideDishes_Name.Text;

                int index = RecipeForm_SideDishes_List.SelectedIndex;

                SideDishes.RemoveAt(index);
                SideDishes.Insert(index, new SideDish(name));
            }

            RecipeForm_SideDishes_Delete.IsEnabled = true;
            RecipeForm_SideDishes_New.IsEnabled = true;
            RecipeForm_SideDishes_Edit.IsEnabled = true;
            RecipeForm_SideDishes_List.IsEnabled = true;
            RecipeForm_SideDishes_Back.IsEnabled = true;
            RecipeForm_SideDishes_Save.IsEnabled = false;
            RecipeForm_SideDishes_Name.IsEnabled = false;

            RecipeForm_SideDishes_Name.Text = "";

            newSideDish = false;

            _SideDishes();
        }

        private void RecipeForm_SideDishes_Edit_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm_SideDishes_Delete.IsEnabled = false;
            RecipeForm_SideDishes_New.IsEnabled = false;
            RecipeForm_SideDishes_Edit.IsEnabled = false;
            RecipeForm_SideDishes_List.IsEnabled = false;
            RecipeForm_SideDishes_Back.IsEnabled = false;
            RecipeForm_SideDishes_Save.IsEnabled = true;
            RecipeForm_SideDishes_Name.IsEnabled = true;

            SideDish sd = (SideDish)RecipeForm_SideDishes_List.SelectedItem;

            RecipeForm_SideDishes_Name.Text = sd.Name;

            newSideDish = false;
        }

        private void RecipeForm_SideDishes_Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = RecipeForm_SideDishes_List.SelectedIndex;

            SideDishes.RemoveAt(index);

            _SideDishes();
        }

        private void RecipeForm_SideDishes_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipeform_main");

            _RecipeForm();
        }

        private void RecipeForm_Ingredients_New_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm_Ingredients_Delete.IsEnabled = false;
            RecipeForm_Ingredients_New.IsEnabled = false;
            RecipeForm_Ingredients_Edit.IsEnabled = false;
            RecipeForm_Ingredients_List.IsEnabled = false;
            RecipeForm_Ingredients_Back.IsEnabled = false;
            RecipeForm_Ingredients_Save.IsEnabled = true;
            RecipeForm_Ingredients_Name.IsEnabled = true;
            RecipeForm_Ingredients_Units.IsEnabled = true;
            RecipeForm_Ingredients_Value.IsEnabled = true;

            RecipeForm_Ingredients_Name.Text = "";

            newIngredient = true;
        }

        private void RecipeForm_Ingredients_Edit_Click(object sender, RoutedEventArgs e)
        {
            RecipeForm_Ingredients_Delete.IsEnabled = false;
            RecipeForm_Ingredients_New.IsEnabled = false;
            RecipeForm_Ingredients_Edit.IsEnabled = false;
            RecipeForm_Ingredients_List.IsEnabled = false;
            RecipeForm_Ingredients_Back.IsEnabled = false;
            RecipeForm_Ingredients_Save.IsEnabled = true;
            RecipeForm_Ingredients_Name.IsEnabled = true;
            RecipeForm_Ingredients_Units.IsEnabled = true;
            RecipeForm_Ingredients_Value.IsEnabled = true;

            Ingredient i = (Ingredient)RecipeForm_Ingredients_List.SelectedItem;

            RecipeForm_Ingredients_Name.Text = i.Name;
            RecipeForm_Ingredients_Value.Text = i.Value.ToString();

            for (int ix = 0; ix < RecipeForm_Ingredients_Units.Items.Count; ix++)
            {
                if ((string)RecipeForm_Ingredients_Units.Items[ix] == i.Units)
                {
                    RecipeForm_Ingredients_Units.SelectedIndex = ix;

                    break;
                }
            }

            newSideDish = false;
        }

        private void RecipeForm_Ingredients_Save_Click(object sender, RoutedEventArgs e)
        {
            if (newIngredient)
            {
                string name = RecipeForm_Ingredients_Name.Text;
                string unit = (string)RecipeForm_Ingredients_Units.SelectedItem;
                int value = Convert.ToInt32(RecipeForm_Ingredients_Value.Text);

                Ingredients.Add(new Ingredient(name, value, unit));
            }
            else
            {
                string name = RecipeForm_Ingredients_Name.Text;
                string unit = (string)RecipeForm_Ingredients_Units.SelectedItem;
                int value = Convert.ToInt32(RecipeForm_Ingredients_Value.Text);

                int index = RecipeForm_Ingredients_List.SelectedIndex;

                Ingredients.RemoveAt(index);
                Ingredients.Insert(index, new Ingredient(name, value, unit));
            }

            RecipeForm_Ingredients_Delete.IsEnabled = true;
            RecipeForm_Ingredients_New.IsEnabled = true;
            RecipeForm_Ingredients_Edit.IsEnabled = true;
            RecipeForm_Ingredients_List.IsEnabled = true;
            RecipeForm_Ingredients_Back.IsEnabled = true;
            RecipeForm_Ingredients_Save.IsEnabled = false;
            RecipeForm_Ingredients_Name.IsEnabled = false;
            RecipeForm_Ingredients_Value.IsEnabled = false;
            RecipeForm_Ingredients_Units.IsEnabled = false;

            RecipeForm_Ingredients_Name.Text = "";
            RecipeForm_Ingredients_Units.SelectedIndex = 0;
            RecipeForm_Ingredients_Value.Text = "";

            newIngredient = false;

            _Ingredients();
        }

        private void RecipeForm_Ingredients_Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = RecipeForm_Ingredients_List.SelectedIndex;

            Ingredients.RemoveAt(index);

            _Ingredients();
        }

        private void RecipeForm_Ingredients_Back_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipeform_main");

            _RecipeForm();
        }

        private void RecipeForm_Main_IngredientsManage_Click(object sender, RoutedEventArgs e)
        {
            uiHandler.ShowGrid("ui_recipeform_ingredients");

            _Ingredients();
        }
    }
}
