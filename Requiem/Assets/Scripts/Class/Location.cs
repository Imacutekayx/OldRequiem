namespace Requiem.Class
{
    /// <summary>
    /// Class which represent a location used in the movement manager to calculate the best path
    /// </summary>
    public class Location
    {
        //Variables
        public int x;
        public int y;
        public int f;
        public int g;
        public int h;

        //Objects
        public Location parent;

        //Constructor
        public Location(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
