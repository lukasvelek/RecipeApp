using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class RecipeIngredient
    {
        private string name;
        private string description;

        private double measurement;
        private MeasurementUnits measurementUnit;

        public RecipeIngredient(string name, string description, double measurement, MeasurementUnits measurementUnit)
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

        public MeasurementUnits MeasurementUnit
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
