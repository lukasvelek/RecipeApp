using System.Collections.Generic;

namespace RecipeApp
{
    public class Recipe
    {
        private string name;
        
        private int portionCount;
        
        private List<RecipeIngredient>? ingredients;
        
        private List<string>? instructions;
        
        public Recipe(string name, int portions, List<RecipeIngredient>? ingredients = null, List<string>? instructions = null)
        {
            this.name = name;
            this.portionCount = portions;

            if (ingredients != null)
            {
                this.ingredients = ingredients;
            }

            if(instructions != null)
            {
                this.instructions = instructions;
            }
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public int PortionCount
        {
            get { return this.portionCount; }
            private set { this.portionCount = value; }
        }

        public List<RecipeIngredient> Ingredients
        {
            get { return this.ingredients; }
            private set { this.Ingredients = value; }
        }

        public List<string> Instructions
        {
            get { return this.instructions; }
            private set { this.instructions = value; }
        }

        public void AddIngredient(RecipeIngredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public void AddInstruction(string instruction)
        {
            instructions.Add(instruction);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
