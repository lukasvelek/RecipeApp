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
    }
}
