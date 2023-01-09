using RecipeApp.Recipe;
using System.Collections.Generic;

namespace RecipeApp.ExportFormats
{
    public class HTML
    {
        public List<ExportDocument> ExportRecipes(List<Recipe.Recipe> recipes)
        {
            List<ExportDocument> ed = new List<ExportDocument>();

            foreach (Recipe.Recipe r in recipes)
            {
                ed.Add(ExportRecipe(r));
            }

            return ed;
        }

        public ExportDocument ExportRecipe(Recipe.Recipe recipe)
        {
            List<string> text = new List<string>();

            string name = recipe.Name;
            string note = recipe.Note;
            string servings = recipe.Servings;
            string timeNeeded = recipe.TimeNeededMinutes;
            List<Ingredient> ingredients = recipe.Ingredients;
            List<SideDish> sideDishes = recipe.AvailableSideDish;

            text.Add("<!DOCTYPE html>");
            text.Add("<html>");
            text.Add("<head>");
            text.Add("<title>" + name + "</title>");
            text.Add("</head>");
            text.Add("<body>");
            text.Add("<h1>" + name + "</h1>");
            text.Add("<h2><b>Poznámka: </b>" + note + "</h2>");
            text.Add("<p><b>Počet porcí: </b>" + servings.ToString() + "</p>");
            text.Add("<p><b>Potřebný čas (minuty): </b>" + timeNeeded.ToString() + "</p>");
            text.Add("<br>");
            text.Add("<b><u>Ingredience:</u></b>");
            text.Add("<table border=\"1\">");
            text.Add("<tr><th>Název</th><th>Počet</th></tr>");

            foreach (Ingredient i in ingredients)
            {
                text.Add("<tr><td>" + i.Name + "</td><td>" + i.Value.ToString() + " " + i.Units + "</td></tr>");
            }

            text.Add("<b><u>Přílohy:</u></b>");
            text.Add("<table border=\"1\">");
            text.Add("<tr><th>Název</th></tr>");

            foreach (SideDish sd in sideDishes)
            {
                text.Add("<tr><td>" + sd.Name + "</td></tr>");
            }

            text.Add("</body>");
            text.Add("</html>");

            return new ExportDocument(recipe.Name, text);
        }
    }
}
