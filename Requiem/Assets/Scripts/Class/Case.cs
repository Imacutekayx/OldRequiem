using System;
using System.Collections.Generic;

namespace Requiem.Class
{
    [Serializable]
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
        public bool lightsource;
        public int lpower;
        public string state;
        public int timeState;
        public byte possibility = 0; //0=None/1=Movement/2=Attack/3=Power/4=Item
        public bool visible = false;
        //TODO State time and triggered when entered
        //Objects
        public Dictionary<Item, int> items;
        public Entity entity;
        public LayerImage layerImage;

        //Constructor
        public Case(int _x, int _y, string _type = "free", float _high = 0, bool _lightsource = false, int _lpower = 0, string _state = "", int _timeState = 0, Dictionary<Item, int> _items = null)
        {
            x = _x;
            y = _y;
            type = _type;
            high = _high;
            lightsource = _lightsource;
            lpower = _lpower;
            state = _state;
            items = _items;
        }

        /// <summary>
        /// Change the state of the case
        /// </summary>
        /// <param name="_state"></param>
        public void ChangeState(string _state)
        {
            state = _state;
            if(state == "fire" && !lightsource)
            {
                lightsource = true;
                lpower = 1;
            }
        }

        public void ShowItems()
        {
        }
    }
}