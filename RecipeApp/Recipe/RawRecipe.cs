﻿using System;
using System.Collections.Generic;

namespace RecipeApp.Recipe
{
    public class RawRecipe
    {
        public string Name { get; private set; }
        public string Note { get; private set; }
        public string Servings { get; private set; }
        public string TimeNeededMinutes { get; private set; }

        public List<string> Ingredients { get; private set; }
        public List<string> SideDishes { get; private set; }

        public RawRecipe(string name, string note, string servings, string timeNeededMinutes, List<string>? ingredients = null, List<string>? sideDishes = null)
        {
            Name = name;
            Note = note;
            Servings = servings;
            TimeNeededMinutes = timeNeededMinutes;

            if (ingredients != null)
            {
                Ingredients = ingredients;
            }
            else
            {
                Ingredients = new List<string>();
            }

            if (sideDishes != null)
            {
                SideDishes = sideDishes;
            }
            else
            {
                SideDishes = new List<string>();
            }
        }

        public Recipe GetRecipe()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            List<SideDish> sideDishes = new List<SideDish>();

            foreach (string i in Ingredients)
            {
                string name = i.Split('-')[0];
                int value = Convert.ToInt32(i.Split('-')[1]);
                string unit = i.Split('-')[2];

                ingredients.Add(new Ingredient(name, value, unit));
            }

            foreach (string s in SideDishes)
            {
                string name = s;

                sideDishes.Add(new SideDish(name));
            }

            Recipe r = new Recipe(Name, Note, Servings, TimeNeededMinutes, ingredients, sideDishes);

            return r;
        }
    }
}
