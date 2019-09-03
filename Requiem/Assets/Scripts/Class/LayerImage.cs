using System;
using System.Collections.Generic;

namespace Requiem.Class
{
    [Serializable]
    /// <summary>
    /// Class which represents an image added to the scene
    /// </summary>
    public class LayerImage
    {
        //Variables
        public string name;
        public float high;
        public int x;
        public int y;
        public int weight;
        public int height;
        public byte face = 0; //0=S/1=W/2=N/3=E
        public List<LayerScript> scripts;

        //Constructor
        public LayerImage(string _name, int _x, int _y, byte _face = 0, int _high = 0, int _weight = 1, int _height = 1, List<LayerScript> _scripts = null)
        {
            name = _name;
            weight = _weight;
            height = _height;
            high = _high;
            x = _x;
            y = _y;
            face = _face;
            scripts = _scripts;
        }
    }
}