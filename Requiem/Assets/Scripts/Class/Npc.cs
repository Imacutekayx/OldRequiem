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

        //Constructor
        public Npc(string _name, bool _sex, int _age, string _job, string _temper, Dictionary<Item, int> _bag)
        {
            name = _name;
            sex = _sex;
            age = _age;
            job = _job;
            temper = _temper;
            type = "npc";
            bag = _bag;
        }
    }
}
