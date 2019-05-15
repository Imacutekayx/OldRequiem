using Requiem.Class;
using System;
using System.Collections.Generic;
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
            if (!Globals.currentCharacter.busy)
            {
                switch (Globals.mode)
                {
                    case "movement":
                        if(c.type == "free")
                        {
                            bool first = true;
                            foreach (Location location in Globals.movementManager.path)
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
                        break;

                    case "power":
                        if(Globals.power != "" && c.possibility == 3)
                        {
                            foreach(Power pow in Globals.currentCharacter.powers)
                            {
                                if(pow.name == Globals.power)
                                {
                                    Globals.scriptManager.ShowPowerArea(pow, new Location(c.x, c.y));
                                    break;
                                }
                            }
                        }
                        break;

                    case "attack":
                        if(Globals.power != "" && c.possibility == 2)
                        {
                            string[] name = Globals.power.Split(':');
                            foreach(Power pow in Globals.currentCharacter.weapons[Convert.ToInt32(name[0])].powers)
                            {
                                if(pow.name == name[1])
                                {
                                    Globals.scriptManager.ShowPowerArea(pow, new Location(c.x, c.y));
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Trigger when the caseObject is clicked
        /// </summary>
        public void OnMouseUpAsButton()
        {
            if (!Globals.currentCharacter.busy)
            {
                //TODO Add item mode (item's effects on case and entities)
                switch (Globals.mode)
                {
                    case "":
                        bool close = false;

                        //Check if the player is close
                        for (int i = c.layerImage.x; i < c.layerImage.x + c.layerImage.weight; ++i)
                        {
                            for (int j = c.layerImage.y; j < c.layerImage.y + c.layerImage.height; ++j)
                            {
                                if (Math.Abs(i - Globals.currentCharacter.x) + Math.Abs(j - Globals.currentCharacter.y) <= 1) { close = true; }
                            }
                        }
                        if (c.layerImage != null)
                        {
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
                        else if (close && c.items.Count != 0)
                        {
                            if(c.entity == null)
                            {
                                foreach(KeyValuePair<Item, int> item in c.items)
                                {
                                    Globals.currentCharacter.TakeItem(item.Key, item.Value);
                                }
                            }
                            else
                            {
                                c.entity.CheckBag();
                            }
                        }
                        break;

                    case "movement":
                        if (c.type == "free")
                        {
                            foreach (Location location in Globals.movementManager.path)
                            {
                                Globals.currentScene.cases[location.x, location.y].possibility = 0;
                                Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                            }
                            Globals.movementManager.nextCase = Globals.movementManager.path.Count - 1;
                            Globals.movementManager.StartMove();
                        }
                        break;

                    case "power":
                        if (Globals.power != "" && c.possibility == 3)
                        {
                            foreach (Power pow in Globals.currentCharacter.powers)
                            {
                                if (pow.name == Globals.power)
                                {
                                    if(pow.mana <= Globals.currentCharacter.mp)
                                    {
                                        double multiple = UnityEngine.Random.Range(1, 100) <= Globals.currentCharacter.dices[2] ? 1 : 1.5f;
                                        //TODO Power.basic
                                        Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(pow.cast * multiple), "castPower", Globals.currentCharacter, pow.name + ";" + c.x + ":" + c.y));
                                        Globals.power = "";
                                        Globals.cameraManager.CleanCases();
                                    }
                                    break;
                                }
                            }
                        }
                        break;

                    case "attack":
                        if (Globals.power != "" && c.possibility == 2)
                        {
                            string[] name = Globals.power.Split(':');
                            foreach (Power pow in Globals.currentCharacter.weapons[Convert.ToInt32(name[0])].powers)
                            {
                                if (pow.name == name[1])
                                {
                                    if (pow.mana <= Globals.currentCharacter.mp)
                                    {
                                        Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(pow.cast), "castAttack", Globals.currentCharacter, Globals.power + ";" + c.x + ":" + c.y));
                                        Globals.power = "";
                                        Globals.cameraManager.CleanCases();
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
