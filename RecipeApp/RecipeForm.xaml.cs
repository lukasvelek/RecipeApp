using RecipeApp.Recipe;
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
using System.Windows.Shapes;

namespace RecipeApp
{
    public partial class RecipeForm : Window
    {
        private static UIHandler uiHandler = new UIHandler();

        public Recipe.Recipe? _Recipe = null;

        private List<Ingredient> Ingredients = new List<Ingredient>();
        private List<SideDish> SideDishes = new List<SideDish>();

        private bool newIngredient = false;
        private bool newSideDish = false;

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

        private void _SideDishes()
        {
            RecipeForm_SideDishes_List.Items.Clear();

            RecipeForm_SideDishes_Name.Text = "";

            RecipeForm_SideDishes_Edit.IsEnabled = false;
            RecipeForm_SideDishes_Save.IsEnabled = false;
            RecipeForm_SideDishes_Delete.IsEnabled = false;

            RecipeForm_SideDishes_Name.IsEnabled = false;

            if(SideDishes.Count > 0)
            {
                foreach(SideDish sd in SideDishes)
                {
                    RecipeForm_SideDishes_List.Items.Add(sd);
                }
            }

            if(RecipeForm_SideDishes_List.Items.Count > 0)
            {
                RecipeForm_SideDishes_List.SelectedIndex = 0;

                string text = (string)RecipeForm_SideDishes_List.SelectedItem;

                RecipeForm_SideDishes_Name.Text = text;

                RecipeForm_SideDishes_Delete.IsEnabled = true;
                RecipeForm_SideDishes_Edit.IsEnabled = true;
            }
        }

        private void RecipeForm_Main_ServingsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = RecipeForm_Main_ServingsSlider.Value;
            string text = "1";

            if(value == 11)
            {
                text = "10+";
            }
            else
            {
                text = value.ToString();
            }

            if(RecipeForm_Main_ServingsText != null)
            {
                RecipeForm_Main_ServingsText.Content = text;
            }
        }

        private void RecipeForm_Main_Save_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeForm_Main_Name.Text != "" &&
               RecipeForm_Main_IngredientsList.Items.Count > 0)
            {
                Recipe.Recipe r;

                string name = RecipeForm_Main_Name.Text;
                string note = RecipeForm_Main_Note.Text;
                int timeNeeded = Convert.ToInt32(RecipeForm_Main_TimeNeededText.Content);
                int servings = Convert.ToInt32(RecipeForm_Main_ServingsText.Content);

                List<Ingredient> ingredients = new List<Ingredient>();
                List<SideDish> sideDishes = new List<SideDish>();

                foreach(var item in RecipeForm_Main_IngredientsList.Items)
                {
                    Ingredient i = (Ingredient)item;

                    ingredients.Add(i);
                }

                foreach(var item in RecipeForm_Main_SideDishesList.Items)
                {
                    SideDish sd = (SideDish)item;

                    sideDishes.Add(sd);
                }

                r = new Recipe.Recipe(name, note, servings, timeNeeded, ingredients, sideDishes);

                _Recipe = r;
            }
        }

        private void RecipeForm_Main_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

            }
            else
            {

            }

            RecipeForm_SideDishes_Delete.IsEnabled = false;
            RecipeForm_SideDishes_New.IsEnabled = false;
            RecipeForm_SideDishes_Edit.IsEnabled = false;
            RecipeForm_SideDishes_List.IsEnabled = false;
            RecipeForm_SideDishes_Back.IsEnabled = false;
            RecipeForm_SideDishes_Save.IsEnabled = true;
            RecipeForm_SideDishes_Name.IsEnabled = true;

            RecipeForm_SideDishes_Name.Text = "";

            newSideDish = true;

            newSideDish = true;
        }

        private void RecipeForm_SideDishes_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecipeForm_SideDishes_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
