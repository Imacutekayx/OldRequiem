using Requiem.Class;
using System;
using UnityEngine;

namespace Requiem.Objects
{
    /// <summary>
    /// Class used to hold the CaseObject of a GameObject
    /// </summary>
    public class CaseObject : MonoBehaviour
    {
        public Case c;
        public Entity entity;
        public LayerImage layerImage;

        //Go to the designed case
        public void OnMouseUpAsButton()
        {
            switch (Globals.mode)
            {
                case "":
                    if(layerImage != null)
                    {
                        bool close = false;

                        //Check if the player is close
                        for (int i = layerImage.x; i < layerImage.x + layerImage.weight; ++i)
                        {
                            for (int j = layerImage.y; j < layerImage.y + layerImage.height; ++j)
                            {
                                if (Math.Abs(i - Globals.currentCharacter.x) + Math.Abs(j - Globals.currentCharacter.y) == 1) { close = true; }
                            }
                        }

                        //Execute each script if close
                        if (close && layerImage.scripts != null)
                        {
                            foreach (LayerScript script in layerImage.scripts)
                            {
                                if (script.state)
                                {
                                    Globals.scriptManager.ExecuteScript(script, Globals.currentCharacter);
                                }
                            }
                        }
                    }
                    break;

                case "movement":
                    if (c.type == "free" && Globals.movementManager.nextCase == 0)
                    {
                        Globals.movementManager.path = Globals.movementManager.CalculateMove(new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), new Location(c.x, c.y));
                        Globals.movementManager.nextCase = Globals.movementManager.path.Count - 1;
                        Globals.movementManager.StartMove();
                    }
                    break;

                case "power":
                    if(Globals.power != "" && c.possibility == 2)
                    {
                        Power power;
                        foreach (Power pow in Globals.currentCharacter.powers)
                        {
                            if (pow.name == Globals.power)
                            {
                                power = pow;
                                break;
                            }
                        }
                    }
                    break;

                case "attack":
                    if (Globals.power != "" && c.possibility == 1)
                    {

                    }
                    break;
            }
        }
    }
}
