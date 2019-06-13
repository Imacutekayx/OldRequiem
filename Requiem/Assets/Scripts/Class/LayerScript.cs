using System;
using System.Collections.Generic;

namespace Requiem.Class
{
    [Serializable]
    /// <summary>
    /// Class which represents a script on a scene
    /// </summary>
    public class LayerScript
    {
        //Variable
        public string name;
        public bool state;
        public string typeTrigger;
        public int weight;
        public int height;
        public int x;
        public int y;
        public int range;
        public List<string> parameters;

        //Constructors
        public LayerScript(string _name, bool _state, string _typeTrigger, int _weight, int _height, int _x, int _y, int _range, List<string> _parameters = null)
        {
            name = _name;
            state = _state;
            typeTrigger = _typeTrigger;
            weight = _weight;
            height = _height;
            x = _x;
            y = _y;
            range = _range;
            parameters = _parameters;
        }
        public LayerScript(string _name, bool _state, List<string> _parameters = null)
        {
            name = _name;
            state = _state;
            parameters = _parameters;
        }
    }
}