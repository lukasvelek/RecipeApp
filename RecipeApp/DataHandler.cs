using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace RecipeApp
{
    public class DataHandler
    {
        private const string RECIPES_ZIP = "recipes.zip";

        public List<Recipe.Recipe> Recipes;

        public DataHandler()
        {
            Recipes = new List<Recipe.Recipe>();
        }

        public void SaveRecipes()
        {
            List<string> recipeNamesZip = new List<string>();

            using(ZipArchive archive = ZipFile.OpenRead(RECIPES_ZIP))
            {
                foreach(ZipArchiveEntry e in archive.Entries)
                {
                    if(e.Name == "recipes.txt")
                    {
                        using(StreamReader sr = new StreamReader(e.Open()))
                        {
                            string line = "";

                            recipeNamesZip.Add(line);
                        }
                    }
                }
            }

            foreach (Recipe.Recipe r in Recipes)
            {
                foreach (string rnz in recipeNamesZip)
                {
                    if (r.Name == rnz)
                    {

                    }
                    else
                    {
                        using (ZipArchive archive = ZipFile.Open(RECIPES_ZIP, ZipArchiveMode.Update))
                        {
                            foreach(ZipArchiveEntry e in archive.Entries)
                            {
                                if(e.Name == "recipes.txt")
                                {
                                    using (StreamWriter writer = new StreamWriter(e.Name, false))
                                    {
                                        writer.WriteLine(r.Name);
                                    }

                                    break;
                                }
                            }

                            ZipArchiveEntry ingredients = archive.CreateEntry(r.Name + "_ingredients.txt");

                            using(StreamWriter writer = new StreamWriter(ingredients.Open()))
                            {
                                foreach(Ingredient i in r.Ingredients)
                                {
                                    string iName = i.Name;
                                    string iValue = i.Value.ToString();
                                    string iUnit = i.Units;

                                    writer.WriteLine(iName + "-" + iValue + "-" + iUnit);
                                }
                            }

                            ZipArchiveEntry sideDish = archive.CreateEntry(r.Name + "_sidedishes.txt");

                            using(StreamWriter writer = new StreamWriter(sideDish.Open()))
                            {
                                foreach(SideDish sd in r.AvailableSideDish)
                                {
                                    string sdName = sd.Name;

                                    writer.WriteLine(sdName);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LoadRecipes()
        {
            if (!File.Exists(RECIPES_ZIP))
            {
                File.Create(RECIPES_ZIP);
            }
            else
            {
                List<string> recipeNames = new List<string>();

                using (ZipArchive archive = ZipFile.OpenRead(RECIPES_ZIP))
                {
                    foreach (ZipArchiveEntry e in archive.Entries)
                    {
                        if (e.Name == "recipes.txt")
                        {
                            using (StreamReader sr = new StreamReader(e.Open()))
                            {
                                string line = "";

                                while ((line = sr.ReadLine()) != null)
                                {
                                    recipeNames.Add(line);
                                }
                            }
                        }
                    }

                    foreach(string rn in recipeNames)
                    {
                        string recipeName = "";
                        string recipeNote = "";
                        string recipeServings = 0;

                        List<string> ingredients = new List<string>();
                        List<string> sideDishes = new List<string>();

                        foreach(ZipArchiveEntry e in archive.Entries)
                        {
                            if(e.Name == (rn + ".txt"))
                            {
                                using(StreamReader sr = new StreamReader(e.Open()))
                                {
                                    string line = "";

                                    while((line = sr.ReadLine()) != null)
                                    {
                                        string paramName = line.Split('=')[0];
                                        string paramValue = line.Split('=')[1];

                                        switch (paramName)
                                        {
                                            case "name":
                                                recipeName = paramValue;
                                                break;

                                            case "servings":
                                                recipeServings = paramValue;
                                                break;

                                            case "note":
                                                recipeNote = paramValue;
                                                break;
                                        }
                                    }
                                }
                            }
                            else if(e.Name == (rn + "_ingredients.txt"))
                            {
                                using (StreamReader sr = new StreamReader(e.Open()))
                                {
                                    string line = "";

                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        string iName = line.Split('-')[0];
                                        int iServings = Convert.ToInt32(line.Split('-')[1]);
                                        string iUnit = line.Split('-')[2];

                                        ingredients.Add(iName + "-" + iServings + "-" + iUnit);
                                    }
                                }
                            }
                            else if(e.Name == (rn + "_sidedishes.txt"))
                            {
                                using (StreamReader sr = new StreamReader(e.Open()))
                                {
                                    string line = "";

                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        string sdName = line;

                                        sideDishes.Add(sdName);
                                    }
                                }
                            }
                        }

                        RawRecipe rr = new RawRecipe(recipeName, recipeNote, recipeServings, ingredients, sideDishes);

                        Recipes.Add(rr.GetRecipe());
                    }
                }
            }
        }
    }
}
