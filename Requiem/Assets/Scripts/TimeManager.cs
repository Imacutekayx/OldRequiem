using Requiem.Class;
using System.Collections.Generic;
using System.Linq;

namespace Requiem
{
    public class TimeManager
    {
        public List<Act> actions = new List<Act>();
        public List<Act> add = new List<Act>();
        public List<Act> remove = new List<Act>();

        /// <summary>
        /// Add an action to the queue
        /// </summary>
        /// <param name="action">Action to add</param>
        public void AddAction(Act action)
        {
            add.Add(action);
        }

        /// <summary>
        /// Execute an action
        /// </summary>
        /// <param name="action">Action to execute </param>
        public void Execute(Act action)
        {
            switch (action.manager)
            {
                case "movement":
                    Globals.movementManager.Execute(action);
                    break;

                case "script":
                    Globals.scriptManager.Execute(action);
                    break;
            }
            remove.Add(action);
        }
    }
}
