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
        //Objects
        public Case c;

        /// <summary>
        /// Triggered when the mouse enter the caseObject
        /// </summary>
        public void OnMouseEnter()
        {
            if(Globals.mode == "movement" && c.type == "free" && Globals.movementManager.nextCase == 0)
            {
                bool first = true;
                foreach(Location location in Globals.movementManager.path)
                {
                    if (!first)
                    {
                        Globals.currentScene.cases[location.x, location.y].possibility = 0;
                        Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                    }
                    else { first = false; }
                }
                Globals.movementManager.path = Globals.movementManager.CalculateMove(new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), new Location(c.x, c.y));
                first = true;
                foreach (Location location in Globals.movementManager.path)
                {
                    if (!first)
                    {
                        Globals.currentScene.cases[location.x, location.y].possibility = 1;
                        Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                    }
                    else { first = false; }
                }
            }
        }

        /// <summary>
        /// Trigger when the caseObject is clicked
        /// </summary>
        public void OnMouseUpAsButton()
        {
            switch (Globals.mode)
            {
                case "":
                    if(c.layerImage != null)
                    {
                        bool close = false;

                        //Check if the player is close
                        for (int i = c.layerImage.x; i < c.layerImage.x + c.layerImage.weight; ++i)
                        {
                            for (int j = c.layerImage.y; j < c.layerImage.y + c.layerImage.height; ++j)
                            {
                                if (Math.Abs(i - Globals.currentCharacter.x) + Math.Abs(j - Globals.currentCharacter.y) == 1) { close = true; }
                            }
                        }

                        //Execute each script if close
                        if (close && c.layerImage.scripts != null)
                        {
                            foreach (LayerScript script in c.layerImage.scripts)
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
                        foreach(Location location in Globals.movementManager.path)
                        {
                            Globals.currentScene.cases[location.x, location.y].possibility = 0;
                            Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                        }
                        Globals.movementManager.nextCase = Globals.movementManager.path.Count - 1;
                        Globals.movementManager.StartMove();
                    }
                    break;

                case "power":
                    if(Globals.power != "" && c.possibility == 3)
                    {
                        Power power;
                        foreach (Power pow in Globals.currentCharacter.powers)
                        {
                            if (pow.name == Globals.power)
                            {
                                power = pow;
                                //TODO Add power to the timer
                                Globals.power = "";
                                Globals.cameraManager.CleanCases();
                                break;
                            }
                        }
                    }
                    break;

                case "attack":
                    if (Globals.power != "" && c.possibility == 2)
                    {

                    }
                    break;
            }
        }
    }
}
