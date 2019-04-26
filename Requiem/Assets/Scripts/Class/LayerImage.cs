using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents an image added to the scene
    /// </summary>
    public class LayerImage
    {
        //Variables
        public string name;
        public int weight;
        public int height;
        public float high;
        public int x;
        public int y;
        public byte face = 0; //0=S/1=W/2=N/3=E
        public List<LayerScript> scripts;

        //Constructor
        public LayerImage(string _name, int _weight, int _height, int _x, int _y, byte _face = 0, int _high = 0, List<LayerScript> _scripts = null)
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