using System.Collections.Generic;

namespace Requiem.Class
{
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
        public Ennemy(string _name, string _description, int _weight, int _height, int[] _dices, List<Power> _powers, string _weapontype, Dictionary<Item, int> _bag,
            Armor[] _armor, bool[] _armorChange, string _armortype, Weapon[] _weapon, Dictionary<string, int> _scripts = null)
        {
            name = _name;
            description = _description;
            weight = _weight;
            height = _height;
            type = "ennemy";
            dices = _dices;
            hp = dices[0] / 3;
            mp = dices[1] / 2;
            armors = _armor;
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