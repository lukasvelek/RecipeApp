using System;
using System.Collections.Generic;

namespace RecipeApp
{
    public class Randomizer
    {
        private List<Recipe.Recipe> ShuffledRecipes;

        public Recipe.Recipe LastRecipe { get; set; }

        public Randomizer()
        {
            ShuffledRecipes = new List<Recipe.Recipe>();

            LastRecipe = null;
        }

        public void RandomRecipe()
        {
            Random r = new Random();

            int x = r.Next(0, ShuffledRecipes.Count);

            if (LastRecipe != ShuffledRecipes[x])
            {
                LastRecipe = ShuffledRecipes[x];
            }
            else
            {
                RandomRecipe();
            }
        }

        public void Shuffle(List<Recipe.Recipe> recipes)
        {
            if (ShuffledRecipes.Count > 0)
            {
                ShuffledRecipes.Clear();
            }

            for (int i = 0; i < recipes.Count; i++)
            {
                Random r = new Random();

                int x = r.Next(0, recipes.Count);

                Recipe.Recipe r1 = recipes[x];

                ShuffledRecipes.Add(r1);
            }

            if (ShuffledRecipes.Count == recipes.Count)
            {
                return;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
