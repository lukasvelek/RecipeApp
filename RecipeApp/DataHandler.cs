using RecipeApp.Recipe;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RecipeApp
{
    public class DataHandler
    {
        private const string RECIPES_ZIP = "recipes.zip";
        private const string RECIPES_CONFIG = "recipes.ini";

        public string LANGUAGE = "";

        public List<Recipe.Recipe> Recipes;

        public DataHandler()
        {
            Recipes = new List<Recipe.Recipe>();
        }

        public void SaveConfig()
        {
            if (!File.Exists(RECIPES_CONFIG))
            {
                File.Create(RECIPES_CONFIG);
            }

            List<string> config = new List<string>();

            config.Add("lang=" + LANGUAGE);

            File.WriteAllLines(RECIPES_CONFIG, config);
        }

        public void SaveRecipes()
        {
            if (!File.Exists(RECIPES_ZIP))
            {
                File.Create(RECIPES_ZIP);
            }

            using (ZipArchive archive = ZipFile.Open(RECIPES_ZIP, ZipArchiveMode.Update))
            {
                while (archive.Entries.Count > 0)
                {
                    foreach (ZipArchiveEntry e in archive.Entries)
                    {
                        e.Delete();
                        break;
                    }
                }

                archive.CreateEntry("recipes.txt");

                foreach (ZipArchiveEntry e in archive.Entries)
                {
                    if (e.Name == "recipes.txt")
                    {
                        using (StreamWriter sw = new StreamWriter(e.Open()))
                        {
                            foreach (Recipe.Recipe r in Recipes)
                            {
                                sw.WriteLine(r.Name);
                            }
                        }
                    }
                }

                foreach (Recipe.Recipe r in Recipes)
                {
                    RawRecipe rr = r.GetRawRecipe();

                    archive.CreateEntry(rr.Name + ".txt");
                    archive.CreateEntry(rr.Name + "_ingredients.txt");
                    archive.CreateEntry(rr.Name + "_sidedishes.txt");

                    foreach (ZipArchiveEntry e in archive.Entries)
                    {
                        if (e.Name == (rr.Name + ".txt"))
                        {
                            using (StreamWriter sw = new StreamWriter(e.Open()))
                            {
                                sw.WriteLine("name=" + rr.Name);
                                sw.WriteLine("servings=" + rr.Servings);
                                sw.WriteLine("note=" + rr.Note);
                                sw.WriteLine("time=" + rr.TimeNeededMinutes);
                            }
                        }
                        else if (e.Name == (rr.Name + "_ingredients.txt"))
                        {
                            using (StreamWriter sw = new StreamWriter(e.Open()))
                            {
                                foreach (string il in rr.Ingredients)
                                {
                                    sw.WriteLine(il);
                                }
                            }
                        }
                        else if (e.Name == (rr.Name + "_sidedishes.txt"))
                        {
                            using (StreamWriter sw = new StreamWriter(e.Open()))
                            {
                                foreach (string sd in rr.SideDishes)
                                {
                                    sw.WriteLine(sd);
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

                    foreach (string rn in recipeNames)
                    {
                        string recipeName = "";
                        string recipeNote = "";
                        string recipeServings = "";
                        string recipeTimeNeeded = "";

                        List<string> ingredients = new List<string>();
                        List<string> sideDishes = new List<string>();

                        foreach (ZipArchiveEntry e in archive.Entries)
                        {
                            if (e.Name == (rn + ".txt"))
                            {
                                using (StreamReader sr = new StreamReader(e.Open()))
                                {
                                    string line = "";

                                    while ((line = sr.ReadLine()) != null)
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
                                            case "time":
                                                recipeTimeNeeded = paramValue;
                                                break;
                                        }
                                    }
                                }
                            }
                            else if (e.Name == (rn + "_ingredients.txt"))
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
                            else if (e.Name == (rn + "_sidedishes.txt"))
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

                        RawRecipe rr = new RawRecipe(recipeName, recipeNote, recipeServings, recipeTimeNeeded, ingredients, sideDishes);

                        Recipes.Add(rr.GetRecipe());
                    }
                }
            }
        }

        public void LoadConfig()
        {
            if (!File.Exists(RECIPES_CONFIG))
            {
                File.Create(RECIPES_CONFIG);
            }
            else
            {
                string[] lines = File.ReadAllLines(RECIPES_CONFIG);

                foreach(string line in lines)
                {
                    string varName = line.Split('=')[0];
                    string varValue = line.Split('=')[1];

                    if (varName == "lang")
                    {
                        LANGUAGE = varValue;
                    }
                }
            }
        }
    }
}
