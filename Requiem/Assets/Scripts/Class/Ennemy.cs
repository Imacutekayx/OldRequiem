using System;
using System.Collections.Generic;

namespace Requiem.Class
{
    [Serializable]
    /// <summary>
    /// Class which represent every ennemy entity and inherit from fighter
    /// </summary>
    public class Ennemy : Fighter
    {
        //Basic
        public string description;

        //Special
        public Dictionary<string, int> scripts;

        //Constructor
        public Ennemy(string _name, string _description, int[] _dices, int _armor, List<Power> _powers, string _weapontype, 
            Dictionary<Item, int> _bag, List<string> _languages, List<string> _competences, List<string> _immunities, List<string> _resistances, 
            List<string> _vulnerabilities, Armor[] _armors, bool[] _armorChange, string _armortype, Weapon[] _weapon, Dictionary<string, int> _scripts = null)
        {
            name = _name;
            description = _description;
            type = "ennemy";
            dices = _dices;
            hp = dices[0] / 3;
            mp = dices[1] / 2;
            armor = _armor;
            languages = _languages;
            competences = _competences;
            immunities = _immunities;
            resistances = _resistances;
            vulnerabilities = _vulnerabilities;
            armors = _armors;
            armorChange = _armorChange;
            armortype = _armortype;
            weapons = _weapon;
            weapontype = _weapontype;
            scripts = _scripts;
            powers = _powers;
            bag = _bag;
        }
    }
}