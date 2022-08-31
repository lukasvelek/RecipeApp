using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Unit
    {
        private string id;
        private string name;

        public Unit(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }
    }
}
