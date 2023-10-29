using System.Collections.Generic;

namespace RecipeApp
{
    public class ExportDocument
    {
        public string Name { get; private set; }
        public List<string> Data { get; private set; }

        public ExportDocument(string name, List<string> data)
        {
            Name = name;
            Data = data;
        }

        public override string ToString()
        {
            //return base.ToString();

            string text = "";

            foreach(string d in Data)
            {
                text += d;
            }

            return text;
        }
    }
}
