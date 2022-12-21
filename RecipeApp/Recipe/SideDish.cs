using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Recipe
{
    public class SideDish
    {
        public string Name { get; set; }

        public SideDish(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
