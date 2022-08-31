using System.Collections.Generic;

namespace RecipeApp
{
    public class MeasurementUnits
    {
        private List<Unit> units;

        public MeasurementUnits(List<Unit> units)
        {
            this.units = units;
        }

        public List<Unit> Units
        {
            get { return units; }
            private set { units = value; }
        }
    }
}
