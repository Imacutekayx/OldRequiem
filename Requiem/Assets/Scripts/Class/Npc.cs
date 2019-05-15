using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents an npc
    /// </summary>
    public class Npc : Entity
    {
        //Variables
        public string job;
        public string temper;
        public bool unique;

        //Constructor
        public Npc(string _name, bool _sex, int _age, string _job, string _temper, int _weight, int _height, bool _unique, List<string> _languages, Dictionary<Item, int> _bag)
        {
            name = _name;
            sex = _sex;
            age = _age;
            job = _job;
            temper = _temper;
            weight = _weight;
            height = _height;
            unique = _unique;
            languages = _languages;
            type = "npc";
            bag = _bag;
        }
    }
}
