using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkingApp
{
    public class Drink
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Description { get; set; }
        public bool IsSelected {  get; set; }
    }
}
