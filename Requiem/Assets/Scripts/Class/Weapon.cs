using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents a weapon
    /// </summary>
    public class Weapon : Item
    {
        //Variables
        public string type;
        public int damage;
        public int range;
        public string typeAttack;
        public List<Power> powers;

        //Constructor
        public Weapon(string _name, int _value, int _weight, string _description, string _type, int _damage, int _range, string _typeAttack, List<Power> _powers = null, Dictionary<string, int> _effects = null)
            : base(_name, _value, _weight, _description, 1, _effects)
        {
            use = "weapon";
            type = _type;
            damage = _damage;
            range = _range;
            typeAttack = _typeAttack;
            powers = new List<Power> { new Power("attack", 0, range, 0, 1, 5, new Dictionary<string, int>() { { "damage;" + _typeAttack, damage } }) };
            if(_powers != null)
            {
                powers.AddRange(_powers);
            }
        }
    }
}
