using Requiem.Class;
using System.Collections.Generic;
using System.Linq;

namespace Requiem
{
    public class TimeManager
    {
        public List<Act> actions = new List<Act>();

        /// <summary>
        /// Add an action to the queue
        /// </summary>
        /// <param name="action">Action to add</param>
        public void AddAction(Act action)
        {
            actions.Add(action);
            actions = actions.OrderBy(a => a.time).ToList();
        }

        /// <summary>
        /// Execute an action
        /// </summary>
        /// <param name="action">Action to execute </param>
        public void Execute(Act action)
        {
            //TODO Add more actions
            switch (action.manager)
            {
                case "movement":
                    Globals.movementManager.Execute(action);
                    break;
            }
            actions.Remove(action);
        }
    }
}
