using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Recipe
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Units { get; set; }

        public Ingredient(string name, int value, string units)
        {
            Name = name;
            Value = value;
            Units = units;
        }

        public string GetString()
        {
            return Name + "-" + Value + "-" + Units;
        }

        public override string ToString()
        {
            return Name + " - " + Value + Units;
        }
    }
}
