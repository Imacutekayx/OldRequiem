using System.Collections.Generic;
using UnityEngine;

namespace Requiem.Class
{
    /// <summary>
    /// Parent class of entity
    /// </summary>
    public class Entity
    {
        //Variables
        public string name;
        public bool sex = true; //true = male
        public int age = 0;
        public string type;
        public byte face = 0; //0=S/1=W/2=N/3=E
        public int x;
        public int y;
        public int weight;
        public int height;
        public bool dead;

        //Objects
        public Dictionary<Item, int> bag = new Dictionary<Item, int>();
    }
}