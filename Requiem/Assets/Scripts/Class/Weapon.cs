﻿using System.Collections.Generic;

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

        //Constructor
        public Weapon(string _name, int _value, int _weight, string _description, string _type, int _damage, int _range, Dictionary<string, int> _effects = null)
            : base(_name, _value, _weight, _description, _effects)
        {
            use = "weapon";
            type = _type;
            damage = _damage;
            range = _range;
        }
    }
}
