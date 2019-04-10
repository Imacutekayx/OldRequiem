using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Requiem.Class;

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
            //TODO Execute actions and delete it
        }
    }
}
