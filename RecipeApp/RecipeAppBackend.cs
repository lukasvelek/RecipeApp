using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public class RecipeAppBackend
    {
        private const string RECIPES_LOCATION = "recipes";

        private List<Grid> grids;
        private List<Recipe> recipes;

        public Grid? currentGrid;

        public RecipeAppBackend()
        {
            recipes = new List<Recipe>();
            grids = new List<Grid>();
        }

        // GRID

        public void AddGrid(Grid g)
        {
            bool add = true;

            foreach (Grid g2 in grids)
            {
                if (g2 == g)
                {
                    add = false;
                }
            }

            if (add)
            {
                grids.Add(g);
            }
        }

        public void DrawGrid(Grid g)
        {
            HideGrid(null);

            foreach (Grid g2 in grids)
            {
                if (g == g2)
                {
                    g2.Visibility = Visibility.Visible;

                    currentGrid = g2;
                }
            }
        }

        public void HideGrid(Grid? g)
        {
            foreach (Grid g2 in grids)
            {
                if (g == null)
                {
                    g2.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (g == g2)
                    {
                        g2.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        // RECIPE IO

        public void LoadRecipes()
        {
            if (Directory.Exists(RECIPES_LOCATION))
            {
                var files = from file in Directory.EnumerateFiles(RECIPES_LOCATION) select file;

                foreach (var f in files)
                {
                    string[] lines = File.ReadAllLines(f);

                    string recipeName = "";
                    List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

                    foreach (string line in lines)
                    {
                        string lineName = line.Split('=')[0];
                        string lineData = line.Split('=')[1];

                        switch (lineName)
                        {
                            case "name":
                                recipeName = lineData;

                                break;
                            case "ingredients":
                                // name, description, measurement, unit
                                string? description = "";

                                if (lineData.Split(',')[1] == "null")
                                {
                                    description = null;
                                }
                                else
                                {
                                    description = lineData.Split(',')[1];
                                }

                                MeasurementUnits.Units unit = MeasurementUnits.Units.Grams;

                                switch (lineData.Split(',')[3])
                                {
                                    case "g":
                                        unit = MeasurementUnits.Units.Grams;
                                        break;

                                    case "kg":
                                        unit = MeasurementUnits.Units.Kilograms;
                                        break;

                                    case "pinch":
                                        unit = MeasurementUnits.Units.Pinch;
                                        break;

                                    case "ml":
                                        unit = MeasurementUnits.Units.Milliliters;
                                        break;

                                    case "lb":
                                        unit = MeasurementUnits.Units.Pounds;
                                        break;

                                    case "oz":
                                        unit = MeasurementUnits.Units.Ounces;
                                        break;

                                    case "fl_oz":
                                        unit = MeasurementUnits.Units.Fluid_Ounces;
                                        break;

                                    case "gal":
                                        unit = MeasurementUnits.Units.Gallons;
                                        break;

                                    case "tbsp":
                                        unit = MeasurementUnits.Units.Tablespoons;
                                        break;

                                    case "tsp":
                                        unit = MeasurementUnits.Units.Teaspoons;
                                        break;

                                    case "l":
                                        unit = MeasurementUnits.Units.Liters;
                                        break;

                                    case "cup":
                                        unit = MeasurementUnits.Units.Cups;
                                        break;
                                }

                                recipeIngredients.Add(new RecipeIngredient(lineData.Split(',')[0],
                                                                           description,
                                                                           Convert.ToDouble(lineData.Split(',')[2]),
                                                                           unit));

                                break;
                        }

                    }

                    recipes.Add(new Recipe(recipeName, recipeIngredients));
                }
            }
            else
            {
                Directory.CreateDirectory(RECIPES_LOCATION);

                LoadRecipes();
            }
        }

        public void SaveRecipe(Recipe r)
        {
            string rname = r.Name;
            List<RecipeIngredient> ring = r.Ingredients;

            string fname = CreateFileNameFromRecipeName(rname) + ".recipe";
            string[] flines = new string[1 + ring.Count];

            string frname = rname.ToLower();

            flines[0] = "name=" + frname;

            for (int i = 1; i < (1 + ring.Count); i++)
            {
                RecipeIngredient ri = ring[i - 1];

                string unit = "";

                switch (ri.MeasurementUnit)
                {
                    case MeasurementUnits.Units.Pinch:
                        unit = "pinch";
                        break;
                    case MeasurementUnits.Units.Grams:
                        unit = "g";
                        break;
                    case MeasurementUnits.Units.Kilograms:
                        unit = "kg";
                        break;
                    case MeasurementUnits.Units.Gallons:
                        unit = "gal";
                        break;
                    case MeasurementUnits.Units.Cups:
                        unit = "cup";
                        break;
                    case MeasurementUnits.Units.Teaspoons:
                        unit = "tsp";
                        break;
                    case MeasurementUnits.Units.Tablespoons:
                        unit = "tbsp";
                        break;
                    case MeasurementUnits.Units.Liters:
                        unit = "l";
                        break;
                    case MeasurementUnits.Units.Ounces:
                        unit = "oz";
                        break;
                    case MeasurementUnits.Units.Fluid_Ounces:
                        unit = "fl_oz";
                        break;
                    case MeasurementUnits.Units.Pounds:
                        unit = "lb";
                        break;
                    case MeasurementUnits.Units.Milliliters:
                        unit = "ml";
                        break;
                }

                flines[i] = "ingredients=" + ri.Name.ToLower() + "," + ri.Description.ToLower() + "," + ri.Measurement + "," + unit;
            }

            File.WriteAllLines("recipes/" + fname, flines);
        }

        public void DeleteRecipe(Recipe r)
        {
            string fname = r.Name + ".recipe";

            bool exists = false;

            if(File.Exists("recipes/" + fname))
            {
                exists = true;
            }

            if (exists)
            {
                File.Delete("recipes/" + fname);
            }
        }

        // ESSENTIALS

        private string CreateFileNameFromRecipeName(string recipeName)
        {
            recipeName.ToLower();
            recipeName.Replace(' ', '_');
            recipeName.Replace('.', '_');
            recipeName.Replace(',', '_');

            return recipeName;
        }

        public Recipe GetRecipeByIndex(int index)
        {
            int i = 0;

            Recipe? recipe = null;

            foreach (Recipe r in recipes)
            {
                if (i == index)
                {
                    recipe = r;
                }

                i++;
            }

            return recipe;
        }

        public List<Recipe> Recipes
        {
            get { return recipes; }
            private set { recipes = value; }
        }
    }
}
