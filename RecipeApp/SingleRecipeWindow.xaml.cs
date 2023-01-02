using RecipeApp.Recipe;
using System.Collections.Generic;
using System.Windows;

namespace RecipeApp
{
    public partial class SingleRecipeWindow : Window
    {
        UIHandler uiHandler;

        public SingleRecipeWindow()
        {
            InitializeComponent();

            // Handler initialization
            uiHandler = new UIHandler();

            // Grid initialization
            uiHandler.AddGrid(RecipeSingle, "ui_recipe_single");
        }

        public void LoadRecipe(Recipe.Recipe recipe)
        {
            string name = recipe.Name;
            string note = recipe.Note;
            int servings = recipe.Servings;
            int timeNeeded = recipe.TimeNeededMinutes;

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
