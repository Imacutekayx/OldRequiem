using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represent all the powers and attacks
    /// </summary>
    public class Power
    {
        //Variables
        public string name;
        public int mana;
        public int scope;
        public string areaType;
        public int areaLength;
        public int cast;
        public int speed;
        public Dictionary<string, int> effects;
        public List<string> options;

        //Constructor
        public Power(string _name, int _mana, int _scope, string _areaType, int _areaLength, int _cast, int _speed, Dictionary<string, int> _effects, List<string> _options = null)
        {
            name = _name;
            mana = _mana;
            scope = _scope;
            areaType = _areaType;
            areaLength = _areaLength;
            cast = _cast;
            speed = _speed;
            effects = _effects;
            options = _options;
        }
    }
}