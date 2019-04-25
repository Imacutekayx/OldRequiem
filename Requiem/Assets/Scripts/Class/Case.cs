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
        public int high;
        public string state;
        public string script;

        //Constructor
        public Case(int _x, int _y, string _type = null, int _high = 0, string _state = null, string _script = null)
        {
            x = _x;
            y = _y;
            type = _type;
            high = _high;
            state = _state;
            script = _script;
        }
    }
}