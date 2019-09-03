using System;
using System.Collections.Generic;
using UnityEngine;

namespace Requiem.Class
{
    [Serializable]
    /// <summary>
    /// Class which represent every playable character and inherit from fighter
    /// </summary>
    public class Character : Fighter
    {
        //Description
        public string story;
        public string cl;
        public string race;
        public string personality;
        public string origin;
        public int strength = 0;
        public int fov;

        //Constructor
        public Character(string _name, bool _sex, int _age, string _story, string _cl, string _race, string _personality, string _origin, int _fov,
            int[] _dices, int _armor, List<string> _languages, List<string> _competences, List<string> _immunities, List<string> _resistances, List<string> _vulnerabilities, 
            Armor[] _armors, bool[] _armorChange, string _armortype, Weapon[] _weapon, string _weapontype, List<Power> _powers, Dictionary<Item, int> _bag)
        {
            name = _name;
            sex = _sex;
            age = _age;
            story = _story;
            cl = _cl;
            race = _race;
            personality = _personality;
            origin = _origin;
            fov = _fov;
            type = "character";
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
            powers = _powers;
            bag = _bag;
            foreach(KeyValuePair<Item, int> item in bag)
            {
                strength += item.Key.weight * item.Value;
            }
        }
    }
}