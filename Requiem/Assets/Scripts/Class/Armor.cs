using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents a piece of armor
    /// </summary>
    public class Armor : Item
    {
        //Variable
        public string part;
        public string type;

        //Constructor
        public Armor(string _name, int _value, int _weight, string _description, string _part, string _type, Dictionary<string, int> _effects = null)
            : base(_name, _value, _weight, _description, 1, _effects)
        {
            use = "armor";
            part = _part;
            type = _type;
        }
    }
}
