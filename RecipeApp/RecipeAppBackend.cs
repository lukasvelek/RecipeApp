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
        private const string LANGUAGES_LOCATION = "languages";

        private List<Grid> grids;
        private List<Recipe> recipes;
        private List<Language> languages;

        public Grid? currentGrid;

        public Language? currentLanguage;

        public MeasurementUnits mUnits;

        public RecipeAppBackend()
        {
            recipes = new List<Recipe>();
            grids = new List<Grid>();
            languages = new List<Language>();
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

        // LANGUAGES

        public void LoadLanguages()
        {
            if (Directory.Exists(LANGUAGES_LOCATION))
            {
                var files = from file in Directory.EnumerateFiles(LANGUAGES_LOCATION) select file;

                foreach (var f in files)
                {
                    languages.Add(new Language(f));
                }
            }

            foreach (Language l in languages)
            {
                if (l.Name == "Czech")
                {
                    currentLanguage = l;
                }
            }

            List<Unit> units = new List<Unit>();
            units.Add(new Unit("cups", currentLanguage.MU_CUPS));
            units.Add(new Unit("pinch", currentLanguage.MU_PINCH));
            units.Add(new Unit("piece", currentLanguage.MU_PIECE));
            units.Add(new Unit("tbsp", currentLanguage.MU_TBSP));
            units.Add(new Unit("tsp", currentLanguage.MU_TSP));
            units.Add(new Unit("grams", currentLanguage.MU_GRAMS));
            units.Add(new Unit("kilograms", currentLanguage.MU_KILOGRAMS));
            units.Add(new Unit("milliliters", currentLanguage.MU_MILLILITERS));
            units.Add(new Unit("liters", currentLanguage.MU_LITERS));

            mUnits = new MeasurementUnits(units);
        }

        public string GetText(string id)
        {
            string s = "";

            switch (id)
            {
                case "btn_main_new_recipe":
                    s = currentLanguage.BTN_MAIN_NEW_RECIPE;
                    break;
                case "btn_main_delete_recipe":
                    s = currentLanguage.BTN_MAIN_DELETE_RECIPE;
                    break;
                case "lbl_main_recipe_name":
                    s = currentLanguage.LBL_MAIN_RECIPE_NAME;
                    break;
                case "lbl_main_ingredients":
                    s = currentLanguage.LBL_MAIN_INGREDIENTS;
                    break;
                case "lbl_main_instructions":
                    s = currentLanguage.LBL_MAIN_INSTRUCTIONS;
                    break;
                case "lbl_main_portion_count":
                    s = currentLanguage.LBL_MAIN_PORTION_COUNT;
                    break;
                case "btn_newrecipe_back":
                    s = currentLanguage.BTN_NEWRECIPE_BACK;
                    break;
                case "btn_newrecipe_save_recipe":
                    s = currentLanguage.BTN_NEWRECIPE_SAVE_RECIPE;
                    break;
                case "btn_newrecipe_add_instruction":
                    s = currentLanguage.BTN_NEWRECIPE_ADD_INSTRUCTION;
                    break;
                case "btn_newrecipe_delete_instruction":
                    s = currentLanguage.BTN_NEWRECIPE_DELETE_INSTRUCTION;
                    break;
                case "btn_newrecipe_edit_instruction":
                    s = currentLanguage.BTN_NEWRECIPE_EDIT_INSTRUCTION;
                    break;
                case "btn_newrecipe_save_instruction":
                    s = currentLanguage.BTN_NEWRECIPE_SAVE_INSTRUCTION;
                    break;
                case "btn_newrecipe_add_ingredient":
                    s = currentLanguage.BTN_NEWRECIPE_ADD_INGREDIENT;
                    break;
                case "btn_newrecipe_delete_ingredient":
                    s = currentLanguage.BTN_NEWRECIPE_DELETE_INGREDIENT;
                    break;
                case "btn_newrecipe_save_ingredient":
                    s = currentLanguage.BTN_NEWRECIPE_SAVE_INGREDIENT;
                    break;
                case "btn_newrecipe_edit_ingredient":
                    s = currentLanguage.BTN_NEWRECIPE_EDIT_INGREDIENT;
                    break;
                case "lbl_newrecipe_name":
                    s = currentLanguage.LBL_NEWRECIPE_NAME;
                    break;
                case "lbl_newrecipe_portions":
                    s = currentLanguage.LBL_NEWRECIPE_PORTIONS;
                    break;
                case "lbl_newrecipe_instructions":
                    s = currentLanguage.LBL_NEWRECIPE_INSTRUCTIONS;
                    break;
                case "lbl_newrecipe_ingredient_name":
                    s = currentLanguage.LBL_NEWRECIPE_INGREDIENT_NAME;
                    break;
                case "lbl_newrecipe_ingredient_description":
                    s = currentLanguage.LBL_NEWRECIPE_INGREDIENT_DESCRIPTION;
                    break;
                case "lbl_newrecipe_ingredient_measurement":
                    s = currentLanguage.LBL_NEWRECIPE_INGREDIENT_MEASUREMENT;
                    break;
            }

            return s;
        }

        // RECIPE IO

        public void RefreshRecipes()
        {
            if (Directory.Exists(RECIPES_LOCATION))
            {
                var files = from file in Directory.EnumerateFiles(RECIPES_LOCATION) select file;

                foreach (var f in files)
                {
                    string[] lines = File.ReadAllLines(f);

                    bool exists = false;

                    string recipeName = "";
                    int recipePortions = -1;
                    List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
                    List<string> recipeInstructions = new List<string>();

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

                                string unit = "";

                                foreach (Unit u in mUnits.Units)
                                {
                                    if (u.Id == lineData.Split(',')[3])
                                    {
                                        unit = u.Name;
                                    }
                                }

                                recipeIngredients.Add(new RecipeIngredient(lineData.Split(',')[0],
                                                                           description,
                                                                           Convert.ToDouble(lineData.Split(',')[2]),
                                                                           unit));

                                break;

                            case "instructions":
                                recipeInstructions.Add(lineData);

                                break;

                            case "portions":
                                recipePortions = Convert.ToInt32(lineData);
                                break;
                        }

                    }

                    foreach (Recipe r in recipes)
                    {
                        if (r.Name == recipeName)
                        {
                            exists = true;
                        }
                    }

                    if (!exists)
                    {
                        recipes.Add(new Recipe(recipeName, recipePortions, recipeIngredients, recipeInstructions));
                    }
                }
            }
        }

        public void LoadRecipes()
        {
            recipes.Clear();

            if (Directory.Exists(RECIPES_LOCATION))
            {
                var files = from file in Directory.EnumerateFiles(RECIPES_LOCATION) select file;

                foreach (var f in files)
                {
                    string[] lines = File.ReadAllLines(f);

                    string recipeName = "";
                    int recipePortions = -1;
                    List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
                    List<string> recipeInstructions = new List<string>();

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

                                string unit = "";

                                foreach(Unit u in mUnits.Units)
                                {
                                    if(u.Id == lineData.Split(',')[3])
                                    {
                                        unit = u.Name;
                                    }
                                }

                                recipeIngredients.Add(new RecipeIngredient(lineData.Split(',')[0],
                                                                           description,
                                                                           Convert.ToDouble(lineData.Split(',')[2]),
                                                                           unit));

                                break;

                            case "instructions":
                                recipeInstructions.Add(lineData);

                                break;

                            case "portions":
                                recipePortions = Convert.ToInt32(lineData);
                                break;
                        }

                    }

                    recipes.Add(new Recipe(recipeName, recipePortions, recipeIngredients, recipeInstructions));
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
            List<string> rins = r.Instructions;

            string fname = CreateFileNameFromRecipeName(rname) + ".recipe";

            List<string> flinesl = new List<string>();

            string frname = rname.ToLower();

            flinesl.Add("name=" + frname);
            flinesl.Add("portions=" + r.PortionCount.ToString());

            foreach (RecipeIngredient ri in ring)
            {
                string unit = "";

                foreach(Unit u in mUnits.Units)
                {
                    if(u.Name == ri.MeasurementUnit)
                    {
                        unit = u.Id;
                    }
                }

                flinesl.Add("ingredients=" + ri.Name.ToLower() + "," + ri.Description.ToLower() + "," + ri.Measurement + "," + unit);
            }

            foreach (string s in rins)
            {
                flinesl.Add("instructions=" + s);
            }

            File.WriteAllLines("recipes/" + fname, flinesl.ToArray());
        }

        public void DeleteRecipe(Recipe r)
        {
            string fname = r.Name + ".recipe";

            bool exists = false;

            if (File.Exists("recipes/" + fname))
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
