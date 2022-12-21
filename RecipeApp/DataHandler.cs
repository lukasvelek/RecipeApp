using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecipeApp
{
    public class DataHandler
    {
        public List<Recipe.Recipe> Recipes;
        public List<SideDish> SideDishes;

        public DataHandler()
        {
            Recipes = new List<Recipe.Recipe>();
            SideDishes = new List<SideDish>();
        }

        public void LoadRecipes(string file)
        {
            string[] lines = File.ReadAllLines(file);

            foreach(string line in lines)
            {
                // name-note\[ingredient]name-value-units;[ingredient];..\[sidedish]name;[sidedish];..

                string[] parts = line.Split('\\');

                string recipeData = parts[0];
                string ingredientData = parts[1];
                string sidedishData = parts[2];

                string recipeName = recipeData.Split('-')[0];
                string recipeNote = recipeData.Split('-')[1];

                string[] sidedishDataSplit = sidedishData.Split(';');
                
                List<SideDish> sd = new List<SideDish>();

                foreach(string sdd in sidedishDataSplit)
                {
                    sd.Add(new SideDish(sdd));
                }

                string[] ingredientDataSplit = ingredientData.Split(';');

                List<Ingredient> id = new List<Ingredient>();

                foreach(string idd in ingredientDataSplit)
                {
                    string iname = idd.Split('-')[0];
                    int ivalue = Convert.ToInt32(idd.Split('-')[1]);
                    string iunits = idd.Split('-')[2];

                    id.Add(new Ingredient(iname, ivalue, iunits));
                }
            }
        }
    }
}
