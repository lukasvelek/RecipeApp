namespace RecipeApp.Recipe
{
    public class Ingredient
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public string Units { get; private set; }

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
