using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Requiem.Class;

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
