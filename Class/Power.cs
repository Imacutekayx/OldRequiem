using System.Collections.Generic;

/// <summary>
/// Class which represent all the powers and attacks
/// </summary>
public class Power
{
    //Variables
    public string name;
    public int mana;
    public int scope;
    public int area;
    public int cast;
    public int speed;
    public Dictionary<string, int> effects;
    public List<string> options;

    //Constructor
    public Power(string _name, int _mana, int _scope, int _area, int _cast, int _speed, Dictionary<string, int> _effects, List<string> _options = null)
    {
        name = _name;
        mana = _mana;
        scope = _scope;
        area = _area;
        cast = _cast;
        speed = _speed;
        effects = _effects;
        options = _options;
    }
}
