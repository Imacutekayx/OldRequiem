namespace Requiem.Class
{
    /// <summary>
    /// Class which represent the actions in the time manager
    /// </summary>
    public class Act
    {
        //Variables
        public string manager;
        public int time;
        public string type;

        //Objects
        public Entity launcher;
        public string parameters;

        //Constructor
        public Act(string _manager, int _time, string _type, Entity _launcher, string _parameters)
        {
            manager = _manager;
            time = _time;
            type = _type;
            launcher = _launcher;
            parameters = _parameters;
        }
    }
}
