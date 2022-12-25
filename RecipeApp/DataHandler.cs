using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RecipeApp
{
    public class DataHandler
    {
        public List<Recipe.Recipe> Recipes;

        public DataHandler()
        {
            Recipes = new List<Recipe.Recipe>();
        }

        public void LoadRecipes()
        {
            string recipesZip = "recipes.zip";

            List<string> recipeNames = new List<string>();

            using(ZipArchive archive = ZipFile.Open(recipesZip, ZipArchiveMode.Read))
            {
                ZipArchiveEntry? entry = archive.GetEntry("recipes.txt");

                using(StreamReader reader = new StreamReader(entry.Open()))
                {
                    if(reader.ReadLine() != null)
                    {
                        recipeNames.Add(reader.ReadLine());
                    }
                }

                foreach(string rnf in recipeNames)
                {
                    entry = archive.GetEntry(rnf + ".txt");

                    using(StreamReader reader = new StreamReader(entry.Open()))
                    {

                    }

                    entry = archive.GetEntry(rnf + "_ingredients.txt");

                    entry = archive.GetEntry(rnf + "_sidedishes.txt");
                }
            }
        }

        public void LoadRecipes(string file)
        {
            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                // name-note-servings\[ingredient]name-value-units;[ingredient];..\[sidedish]name;[sidedish];..

                string[] parts = line.Split('\\');

                string recipeData = parts[0];
                string ingredientData = parts[1];
                string sidedishData = parts[2];

                string recipeName = recipeData.Split('-')[0];
                string recipeNote = recipeData.Split('-')[1];
                int recipeServings = Convert.ToInt32(recipeData.Split('-')[2]);

                string[] sidedishDataSplit = sidedishData.Split(';');

                List<SideDish> sd = new List<SideDish>();

                foreach (string sdd in sidedishDataSplit)
                {
                    sd.Add(new SideDish(sdd));
                }

                string[] ingredientDataSplit = ingredientData.Split(';');

                List<Ingredient> id = new List<Ingredient>();

                foreach (string idd in ingredientDataSplit)
                {
                    string iname = idd.Split('-')[0];
                    int ivalue = Convert.ToInt32(idd.Split('-')[1]);
                    string iunits = idd.Split('-')[2];

                    id.Add(new Ingredient(iname, ivalue, iunits));
                }

                Recipe.Recipe recipe = new Recipe.Recipe(recipeName, recipeNote, recipeServings, id, sd);
            }
        }
    }
}
