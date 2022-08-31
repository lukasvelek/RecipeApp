namespace RecipeApp
{
    public class RecipeIngredient
    {
        private string name;
        private string description;

        private double measurement;
        private string measurementUnit;

        public RecipeIngredient(string name, string description, double measurement, string measurementUnit)
        {
            this.name = name;
            this.description = description;
            this.measurement = measurement;
            this.measurementUnit = measurementUnit;
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        public double Measurement
        {
            get { return measurement; }
            private set { measurement = value; }
        }

        public string MeasurementUnit
        {
            get { return measurementUnit; }
            private set { measurementUnit = value; }
        }

        public override string ToString()
        {
            string s = Name + " " + Measurement + " " + MeasurementUnit.ToString();

            return s;
        }
    }
}
