using System.IO;

namespace RecipeApp
{
    public class Language
    {
        public string Name;

        public string BTN_MAIN_NEW_RECIPE;
        public string BTN_MAIN_DELETE_RECIPE;
        public string LBL_MAIN_RECIPE_NAME;
        public string LBL_MAIN_INGREDIENTS;
        public string LBL_MAIN_INSTRUCTIONS;
        public string LBL_MAIN_PORTION_COUNT;

        public string BTN_NEWRECIPE_BACK;
        public string BTN_NEWRECIPE_SAVE_RECIPE;
        public string BTN_NEWRECIPE_ADD_INSTRUCTION;
        public string BTN_NEWRECIPE_DELETE_INSTRUCTION;
        public string BTN_NEWRECIPE_EDIT_INSTRUCTION;
        public string BTN_NEWRECIPE_SAVE_INSTRUCTION;
        public string BTN_NEWRECIPE_ADD_INGREDIENT;
        public string BTN_NEWRECIPE_DELETE_INGREDIENT;
        public string BTN_NEWRECIPE_EDIT_INGREDIENT;
        public string BTN_NEWRECIPE_SAVE_INGREDIENT;
        public string LBL_NEWRECIPE_NAME;
        public string LBL_NEWRECIPE_PORTIONS;
        public string LBL_NEWRECIPE_INSTRUCTIONS;
        public string LBL_NEWRECIPE_INGREDIENT_NAME;
        public string LBL_NEWRECIPE_INGREDIENT_DESCRIPTION;
        public string LBL_NEWRECIPE_INGREDIENT_MEASUREMENT;

        public Language(string filePath)
        {
            LoadData(filePath);
        }

        private void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string data = line.Split('=')[1];

                    if (name[0] == '#')
                    {
                        break;
                    }

                    switch (name)
                    {
                        case "language_name":
                            Name = data;
                            break;
                        case "btn_main_new_recipe":
                            BTN_MAIN_NEW_RECIPE = data;
                            break;
                        case "btn_main_delete_recipe":
                            BTN_MAIN_DELETE_RECIPE = data;
                            break;
                        case "lbl_main_recipe_name":
                            LBL_MAIN_RECIPE_NAME = data;
                            break;
                        case "lbl_main_ingredients":
                            LBL_MAIN_INGREDIENTS = data;
                            break;
                        case "lbl_main_instructions":
                            LBL_MAIN_INSTRUCTIONS = data;
                            break;
                        case "lbl_main_portion_count":
                            LBL_MAIN_PORTION_COUNT = data;
                            break;
                        case "btn_newrecipe_back":
                            BTN_NEWRECIPE_BACK = data;
                            break;
                        case "btn_newrecipe_save_recipe":
                            BTN_NEWRECIPE_SAVE_RECIPE = data;
                            break;
                        case "btn_newrecipe_add_instruction":
                            BTN_NEWRECIPE_ADD_INSTRUCTION = data;
                            break;
                        case "btn_newrecipe_delete_instruction":
                            BTN_NEWRECIPE_DELETE_INSTRUCTION = data;
                            break;
                        case "btn_newrecipe_edit_instruction":
                            BTN_NEWRECIPE_EDIT_INSTRUCTION = data;
                            break;
                        case "btn_newrecipe_save_instruction":
                            BTN_NEWRECIPE_SAVE_INSTRUCTION = data;
                            break;
                        case "btn_newrecipe_add_ingredient":
                            BTN_NEWRECIPE_ADD_INGREDIENT = data;
                            break;
                        case "btn_newrecipe_delete_ingredient":
                            BTN_NEWRECIPE_DELETE_INGREDIENT = data;
                            break;
                        case "btn_newrecipe_save_ingredient":
                            BTN_NEWRECIPE_SAVE_INGREDIENT = data;
                            break;
                        case "btn_newrecipe_edit_ingredient":
                            BTN_NEWRECIPE_EDIT_INGREDIENT = data;
                            break;
                        case "lbl_newrecipe_name":
                            LBL_NEWRECIPE_NAME = data;
                            break;
                        case "lbl_newrecipe_portions":
                            LBL_NEWRECIPE_PORTIONS = data;
                            break;
                        case "lbl_newrecipe_instructions":
                            LBL_NEWRECIPE_INSTRUCTIONS = data;
                            break;
                        case "lbl_newrecipe_ingredient_name":
                            LBL_NEWRECIPE_INGREDIENT_NAME = data;
                            break;
                        case "lbl_newrecipe_ingredient_description":
                            LBL_NEWRECIPE_INGREDIENT_DESCRIPTION = data;
                            break;
                        case "lbl_newrecipe_ingredient_measurement":
                            LBL_NEWRECIPE_INGREDIENT_MEASUREMENT = data;
                            break;
                    }
                }
            }
        }
    }
}
