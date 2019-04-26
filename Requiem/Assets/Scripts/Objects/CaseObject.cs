using Requiem.Class;
using UnityEngine;

namespace Requiem.Objects
{
    /// <summary>
    /// Class used to hold the CaseObject of a GameObject
    /// </summary>
    public class CaseObject : MonoBehaviour
    {
        public Case c;

        //Go to the designed case
        public void OnMouseUpAsButton()
        {
            Globals.movementManager.path = Globals.movementManager.CalculateMove(new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), new Location(c.x, c.y));
            Globals.movementManager.nextCase = Globals.movementManager.path.Count - 1;
            Globals.movementManager.StartMove();
        }
    }
}
