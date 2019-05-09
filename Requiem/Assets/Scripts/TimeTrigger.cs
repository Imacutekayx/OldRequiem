using Requiem.Class;
using System.Collections.Generic;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class triggering the actions and their speed
    /// </summary>
    public class TimeTrigger : MonoBehaviour
    {
        //Update is called each frame
        private void Update()
        {
            //Create copies of the actions to add and remove
            List<Act> add = new List<Act>(Globals.timeManager.add);
            Globals.timeManager.add = new List<Act>();
            List<Act> remove = new List<Act>(Globals.timeManager.remove);
            Globals.timeManager.remove = new List<Act>();

            //Decrease the time left for an action to be executed and check if it's triggered
            foreach(Act action in Globals.timeManager.actions)
            {
                if(--action.time == 0)
                {
                    Globals.timeManager.Execute(action);
                }
            }

            //Remove action of list
            foreach (Act a in remove)
            {
                Globals.timeManager.actions.Remove(a);
            }

            //Add actions to list
            foreach (Act a in add)
            {
                Globals.timeManager.actions.Add(a);
            }
        }
    }
}
