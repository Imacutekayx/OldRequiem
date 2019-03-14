using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Parent class of all fighting entity
    /// </summary>
    public class Fighter : Entity
    {
        //Combat
        public int hp;
        public int mp;
        public int[] dices;
        public Weapon[] weapon;
        public string weapontype;
        public List<Power> powers;
        public Armor[] armor;
        public bool[] armorChange;
        public string armortype;
    }
}