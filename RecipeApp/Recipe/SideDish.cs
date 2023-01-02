namespace RecipeApp.Recipe
{
    public class SideDish
    {
        public string Name { get; private set; }

        public SideDish(string name)
        {
            Name = name;
        }

        public string GetString()
        {
            return Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
