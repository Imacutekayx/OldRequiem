using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents a case
    /// </summary>
    public class Case
    {
        //Variables
        public int x;
        public int y;
        public string type;
        public float high;
        public string state;
        public byte possibility = 0; //0=None/1=Movement/2=Attack/3=Power

        //Objects
        public Dictionary<Item, int> items;
        public Entity entity;
        public LayerImage layerImage;

        //Constructor
        public Case(int _x, int _y, string _type = "free", float _high = 0, string _state = "", Dictionary<Item, int> _items = null)
        {
            x = _x;
            y = _y;
            type = _type;
            high = _high;
            state = _state;
            items = _items;
        }

        public void ShowItems()
        {
        }
    }
}