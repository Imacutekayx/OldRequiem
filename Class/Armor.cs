using System.Collections.Generic;

namespace Tester.Class
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
        public Armor(string _name, int _value, string _description, string _part, string _type, Dictionary<string, int> _effects)
            : base(_name, _value, _description, _effects)
        {
            use = "armor";
            part = _part;
            type = _type;
        }
    }
}
