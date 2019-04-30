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
        public string script;
        public byte possibility = 0; //0=None/1=Attack/2=Power

        //Constructor
        public Case(int _x, int _y, string _type = "free", float _high = 0, string _state = "", string _script = null)
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