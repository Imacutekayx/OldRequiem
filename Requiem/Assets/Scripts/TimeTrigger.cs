using Requiem.Class;
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
            //Decrease the time left for an action to be executed and check if it's triggered
            foreach(Act action in Globals.timeManager.actions)
            {
                if(--action.time == 0)
                {
                    Globals.timeManager.Execute(action);
                }
            }
        }
    }
}
