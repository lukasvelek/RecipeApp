using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecipeApp
{
    public class LanguageHandler
    {
        public string RECIPEFORM_WINDOW_TITLE;
        public string SETTINGS_WINDOW_TITLE;
        public string MAIN_WINDOW_TITLE;

        public string GENERAL_BACK;
        public string GENERAL_SAVE;
        public string GENERAL_EDIT;
        public string GENERAL_DELETE;
        public string GENERAL_NEW;
        public string GENERAL_CANCEL;
        public string GENERAL_OPEN;

        public string MAINWINDOW_MANAGE_RECIPES;
        public string MAINWINDOW_GENERATE_RANDOM_RECIPE;
        public string MAINWINDOW_SETTINGS;

        public string SETTINGS_LANGUAGE;
            
        public string RECIPE_NAME;
        public string RECIPE_NOTE;
        public string RECIPE_SERVINGS;
        public string RECIPE_TIME_NEEDED;
        public string RECIPE_INGREDIENTS;
        public string RECIPE_SIDE_DISHES;

        public string RECIPEFORM_MANAGE_INGREDIENTS;
        public string RECIPEFORM_MANAGE_SIDE_DISHES;

        public string INGREDIENT_NAME;
        public string INGREDIENT_VALUE;
        public string INGREDIENT_UNIT;

        public string SIDE_DISHES_NAME;

        private const string SOURCE_CZ = "lang/czech.lang";
        private const string SOURCE_EN = "lang/english.lang";
        private const string SOURCE_DE = "lang/german.lang";

        public LanguageHandler() {}

        public void Initialize(string language)
        {
            LoadLanguageData(language);
        }

        private void LoadLanguageData(string lang)
        {
            string source = "";

            if(lang == "cz")
            {
                source = SOURCE_CZ;
            }
            else if(lang == "en")
            {
                source = SOURCE_EN;
            }
            else if (lang == "de")
            {
                source = SOURCE_DE;
            }

            string[] lines = File.ReadAllLines(source);

            foreach(string line in lines)
            {
                string n = line.Split('=')[0];
                string v = line.Split('=')[1];

                switch (n)
                {
                    case "recipeform_window_title":
                        RECIPEFORM_WINDOW_TITLE = v;
                        break;

                    case "settings_window_title":
                        SETTINGS_WINDOW_TITLE = v;
                        break;

                    case "main_window_title":
                        MAIN_WINDOW_TITLE = v;
                        break;

                    case "general_back":
                        GENERAL_BACK = v;
                        break;

                    case "general_save":
                        GENERAL_SAVE = v;
                        break;

                    case "general_edit":
                        GENERAL_EDIT = v;
                        break;

                    case "general_delete":
                        GENERAL_DELETE = v;
                        break;

                    case "general_new":
                        GENERAL_NEW = v;
                        break;

                    case "general_cancel":
                        GENERAL_CANCEL = v;
                        break;

                    case "general_open":
                        GENERAL_OPEN = v;
                        break;

                    case "mainwindow_manage_recipes":
                        MAINWINDOW_MANAGE_RECIPES = v;
                        break;

                    case "mainwindow_generate_random_recipe":
                        MAINWINDOW_GENERATE_RANDOM_RECIPE = v;
                        break;

                    case "mainwindow_settings":
                        MAINWINDOW_SETTINGS = v;
                        break;

                    case "settings_language":
                        SETTINGS_LANGUAGE = v;
                        break;

                    case "recipe_name":
                        RECIPE_NAME = v;
                        break;

                    case "recipe_note":
                        RECIPE_NOTE = v;
                        break;

                    case "recipe_servings":
                        RECIPE_SERVINGS = v;
                        break;

                    case "recipe_time_needed":
                        RECIPE_TIME_NEEDED = v;
                        break;

                    case "recipe_ingredients":
                        RECIPE_INGREDIENTS = v;
                        break;

                    case "recipe_side_dishes":
                        RECIPE_SIDE_DISHES = v;
                        break;

                    case "recipeform_manage_ingredients":
                        RECIPEFORM_MANAGE_INGREDIENTS = v;
                        break;

                    case "recipeform_manage_side_dishes":
                        RECIPEFORM_MANAGE_SIDE_DISHES = v;
                        break;

                    case "ingredient_name":
                        INGREDIENT_NAME = v;
                        break;

                    case "ingredient_value":
                        INGREDIENT_VALUE = v;
                        break;

                    case "ingredient_unit":
                        INGREDIENT_UNIT = v;
                        break;

                    case "side_dishes_name":
                        SIDE_DISHES_NAME = v;
                        break;
                }
            }
        }
    }
}
