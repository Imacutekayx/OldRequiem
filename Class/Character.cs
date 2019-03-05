using System.Collections.Generic;
using Tester;
using Tester.Class;

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
    public int strength;

    //Constructor
    public Character(string _name, bool _sex, int _age, string _story, string _cl, string _race, string _personality, string _origin, int[] _dices, 
        Armor[] _armor, bool[] _armorChange, string _armortype, Weapon[] _weapon, string _weapontype, List<Power> _powers, Dictionary<Item, int> _bag)
    {
        name = _name;
        sex = _sex;
        age = _age;
        story = _story;
        cl = _cl;
        race = _race;
        personality = _personality;
        origin = _origin;
        type = "character";
        dices = _dices;
        hp = dices[0]/3;
        mp = dices[1]/2;
        strength = (100-dices[0])/10;
        armor = _armor;
        armorChange = _armorChange;
        armortype = _armortype;
        weapon = _weapon;
        weapontype = _weapontype;
        powers = _powers;
        bag = _bag;

        //TODO skin
    }
}
