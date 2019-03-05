using System.Collections.Generic;
using Tester;
using Tester.Class;

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
	public Ennemy(string _name, string _description, int[] _dices, List<Power> _powers, string _weapontype, Dictionary<Item, int> _bag,
        Armor[] _armor, bool[] _armorChange, string _armortype, Weapon[] _weapon, Dictionary<string, int> _scripts = null)
	{
        name = _name;
        description = _description;
        type = "ennemy";
        dices = _dices;
        hp = dices[0]/3;
        mp = dices[1]/2;
        armor = _armor;
        armorChange = _armorChange;
        armortype = _armortype;
        weapon = _weapon;
        weapontype = _weapontype;
        scripts = _scripts;
        powers = _powers;
        bag = _bag;

        //TODO skin
	}
}
