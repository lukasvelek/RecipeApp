using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Recipe
    {
        private string name;
        private List<RecipeIngredient> ingredients;

        public Recipe(string name, List<RecipeIngredient>? ingredients = null)
        {
            this.name = name;
            
            if(ingredients != null)
            {
                this.ingredients = ingredients;
            }
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public List<RecipeIngredient> Ingredients
        {
            get { return this.ingredients; }
            private set { this.Ingredients = value; }
        }

        public void AddIngredient(RecipeIngredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
