using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Requiem.Class;
using UnityEngine;

namespace Requiem
{
    public class ScriptManager
    {
        public void ExecuteScript(LayerScript script, Entity trigger = null)
        {
            switch (script.name)
            {
                //Open a chest and get what is inside //=> Show it and choose
                case "openChest":
                    foreach(string parameter in script.parameters)
                    {
                        string[] temp = parameter.Split(';');
                        switch (temp[0])
                        {
                            case "weapon":
                                foreach(Weapon weapon in Globals.weapons)
                                {
                                    if(weapon.name == temp[1])
                                    {
                                        trigger.bag.Add(weapon, 1);
                                        break;
                                    }
                                }
                                break;

                            case "armor":
                                foreach(Armor armor in Globals.armors)
                                {
                                    if(armor.name == temp[1])
                                    {
                                        trigger.bag.Add(armor, 1);
                                        break;
                                    }
                                }
                                break;

                            case "usable":
                                foreach(Item useable in Globals.useables)
                                {
                                    if(useable.name == temp[1])
                                    {
                                        trigger.bag.Add(useable, Convert.ToInt32(temp[2]));
                                        break;
                                    }
                                }
                                break;

                            case "trap":
                                foreach(LayerScript trap in Globals.currentScene.scripts)
                                {
                                    if(trap.name == temp[1])
                                    {
                                        ExecuteScript(trap);
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case "caseState":
                    foreach(string parameter in script.parameters)
                    {
                        string[] temp = parameter.Split(';');
                        switch (temp[0])
                        {
                            case "circle":
                                //TODO Change case state with temp[1]
                                Debug.Log("hey");
                                break;
                        }
                    }
                    break;
            }
            script.state = false;
        }
    }
}
