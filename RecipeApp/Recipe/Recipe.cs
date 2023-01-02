using System.Collections.Generic;

namespace RecipeApp.Recipe
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public int Servings { get; set; }
        public int TimeNeededMinutes { get; set; }

        public List<Ingredient> Ingredients { get; private set; }
        public List<SideDish> AvailableSideDish { get; private set; }

        public Recipe(string name, string note, int servings, int timeNeededMinutes, List<Ingredient>? ingredients = null, List<SideDish>? sideDish = null)
        {
            if (ingredients == null || ingredients.Count == 0)
            {
                Ingredients = new List<Ingredient>();
            }
            else
            {
                Ingredients = ingredients;
            }

            if (sideDish == null || sideDish.Count == 0)
            {
                AvailableSideDish = new List<SideDish>();
            }
            else
            {
                AvailableSideDish = sideDish;
            }

            Name = name;
            Note = note;
            Servings = servings;
            TimeNeededMinutes = timeNeededMinutes;
        }

        public RawRecipe GetRawRecipe()
        {
            List<string> ingredients = new List<string>();
            List<string> sideDishes = new List<string>();

            foreach (Ingredient i in Ingredients)
            {
                ingredients.Add(i.Name + "-" + i.Value.ToString() + "-" + i.Units);
            }

            foreach (SideDish sd in AvailableSideDish)
            {
                sideDishes.Add(sd.Name);
            }

            RawRecipe rr = new RawRecipe(Name, Note, Servings.ToString(), TimeNeededMinutes.ToString(), ingredients, sideDishes);

            return rr;
        }

        public string GetString()
        {
            string toReturn = Name + "-" + Note + ";";

            int x = 0;

            foreach (Ingredient i in Ingredients)
            {
                if ((x + 1) == Ingredients.Count)
                {
                    toReturn += i.GetString();
                }
                else
                {
                    toReturn += i.GetString() + "\\";
                }

                x++;
            }

            int y = 0;

            foreach (SideDish sd in AvailableSideDish)
            {
                if ((y + 1) == AvailableSideDish.Count)
                {
                    toReturn += sd.GetString();
                }
                else
                {
                    toReturn += sd.GetString() + "\\";
                }

                y++;
            }

            return toReturn;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
