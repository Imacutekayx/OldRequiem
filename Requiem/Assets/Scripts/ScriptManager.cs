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
        /// <summary>
        /// Execute a given script
        /// </summary>
        /// <param name="script">Script to execute</param>
        /// <param name="trigger">Entity which triggered the script</param>
        public void ExecuteScript(LayerScript script, Entity trigger = null)
        {
            switch (script.name)
            {
                //Open a chest and get what is inside //=> Show it and choose
                case "openChest":
                    Debug.Log("Before:" + Globals.currentCharacter.strength + ";" + Globals.currentCharacter.bag.Count());
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
                                        AddToBag(weapon, 1);
                                        break;
                                    }
                                }
                                break;

                            case "armor":
                                foreach(Armor armor in Globals.armors)
                                {
                                    if(armor.name == temp[1])
                                    {
                                        AddToBag(armor, 1);
                                        break;
                                    }
                                }
                                break;

                            case "useable":
                                foreach(Item useable in Globals.useables)
                                {
                                    if(useable.name == temp[1])
                                    {
                                        AddToBag(useable, Convert.ToInt32(temp[2]));
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
                    Debug.Log("After:" + Globals.currentCharacter.strength + ";" + Globals.currentCharacter.bag.Count());
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

        /// <summary>
        /// Check if an item can go to the character's bag and add it if possible
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="nbr">Number of the item</param>
        public void AddToBag(Item item, int nbr)
        {
            if (Globals.currentCharacter.strength + (item.weight * nbr) <= Globals.currentCharacter.dices[0] * 10)
            {
                Globals.currentCharacter.strength += item.weight * nbr;
                if (Globals.currentCharacter.bag.ContainsKey(item))
                {
                    Globals.currentCharacter.bag[item] += nbr;
                }
                else
                {
                    Globals.currentCharacter.bag.Add(item, nbr);
                }
            }
        }
    }
}
