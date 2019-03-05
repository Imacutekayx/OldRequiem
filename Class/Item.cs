using System.Collections.Generic;

namespace Tester.Class
{
    /// <summary>
    /// Class which represents an item
    /// </summary>
    public class Item
    {
        //Variables
        public string name;
        public int value;
        public string description;
        public string use;
        public Dictionary<string, int> effects;

        //Constructor
        public Item(string _name, int _value, string _description, Dictionary<string, int> _effects)
        {
            name = _name;
            value = _value;
            description = _description;
            use = "useable";
            effects = _effects;
        }
    }
}
