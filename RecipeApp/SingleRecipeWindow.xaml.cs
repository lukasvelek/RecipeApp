using RecipeApp.Recipe;
using System.Collections.Generic;
using System.Windows;

namespace RecipeApp
{
    public partial class SingleRecipeWindow : Window
    {
        private static UIHandler uiHandler = new UIHandler();

        public SingleRecipeWindow()
        {
            InitializeComponent();

            // Grid initialization
            uiHandler.AddGrid(RecipeSingle, "ui_recipe_single");
        }

        public void Translate(LanguageHandler lh)
        {
            Lbl_RecipeSingle_Ingredients.Content = lh.RECIPE_INGREDIENTS;
            Lbl_RecipeSingle_RecipeNote.Content = lh.RECIPE_NOTE;
            Lbl_RecipeSingle_Servings.Content = lh.RECIPE_SERVINGS;
            Lbl_RecipeSingle_SideDishes.Content = lh.RECIPE_SIDE_DISHES;
            Lbl_RecipeSingle_TimeNeeded.Content = lh.RECIPE_TIME_NEEDED;
        }

        public void LoadRecipe(Recipe.Recipe recipe)
        {
            string name = recipe.Name;
            string note = recipe.Note;
            string servings = recipe.Servings;
            string timeNeeded = recipe.TimeNeededMinutes;

            List<Ingredient> ingredients = recipe.Ingredients;
            List<SideDish> sideDishes = recipe.AvailableSideDish;

            RecipeSingle_RecipeName.Content = name;
            RecipeSingle_RecipeNote.Content = note;
            RecipeSingle_Servings.Content = servings.ToString();
            RecipeSingle_TimeNeeded.Content = timeNeeded.ToString();

            foreach (Ingredient i in ingredients)
            {
                RecipeSingle_Ingredients.Items.Add(i);
            }

            foreach (SideDish sd in sideDishes)
            {
                RecipeSingle_SideDishes.Items.Add(sd);
            }

            Title = name;
        }
    }
}
