using System.Collections.Generic;

namespace Requiem.Class
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
        public int weight;
        public Dictionary<string, int> effects;

        //Constructor
        public Item(string _name, int _value, int _weight, string _description, Dictionary<string, int> _effects = null)
        {
            name = _name;
            value = _value;
            weight = _weight;
            description = _description;
            use = "useable";
            effects = _effects;
        }
    }
}
