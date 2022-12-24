using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Recipe
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public int Servings { get; set; }
        
        public List<Ingredient> Ingredients { get; private set; }
        public List<SideDish> AvailableSideDish { get; private set; }

        public Recipe(string name, string note, int servings, List<Ingredient>? ingredients = null, List<SideDish>? sideDish = null)
        {
            if(ingredients == null || ingredients.Count == 0)
            {
                Ingredients = new List<Ingredient>();
            } 
            else
            {
                Ingredients = ingredients;
            }

            if(sideDish == null || sideDish.Count == 0)
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
