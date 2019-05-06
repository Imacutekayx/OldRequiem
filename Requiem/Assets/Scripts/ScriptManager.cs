using Requiem.Class;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                foreach (Case c in Globals.currentScene.cases)
                                {
                                    if (Math.Abs(c.x - script.x) + Math.Abs(c.y - script.y) <= script.range && c.type != "wall")
                                    {
                                        c.state = temp[1];
                                        Globals.cameraManager.ChangeObject("grid", c.x + ";" + c.y, "redraw");
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
            script.state = false;
        }

        /// <summary>
        /// Method which execute an action sent by the timeManager
        /// </summary>
        /// <param name="act">Action to execute</param>
        public void Execute(Act act)
        {
            switch (act.type)
            {
                case "castPower":
                    string[] parameters = act.parameters.Split(';');
                    Power pow;
                    foreach(Power p in ((Fighter)act.launcher).powers)
                    {
                        if(p.name == parameters[0])
                        {
                            pow = p;
                            //TODO find path to target (from possible basic) and new act like movement
                            break;
                        }
                    }
                    break;

                case "executePower":
                    break;

                case "executeAttack":
                    break;
            }
        }

        /// <summary>
        /// Show a given power's scope
        /// </summary>
        /// <param name="power">Power send</param>
        /// <param name="current">Current location of the caster</param>
        public void ShowPower(Power power, Location current)
        {
            foreach(Case c in Globals.currentScene.cases)
            {
                if(Math.Abs(c.x - current.x) + Math.Abs(c.y - current.y) <= power.scope)
                {
                    c.possibility = 3;
                    Globals.cameraManager.ChangeObject("grid", c.x + ";" + c.y, "redraw");
                }
            }
        }

        /// <summary>
        /// Execute a power given
        /// </summary>
        /// <param name="power">Power to execute</param>
        /// <param name="caster">Fighter that casted the power</param>
        /// <param name="target">Target location of the power</param>
        private void ExecutePower(Power power, Fighter caster, Location target, Location basic = null)
        {
            //TODO Execute power Method
            foreach(KeyValuePair<string, int> effect in power.effects)
            {
                switch (effect.Key)
                {
                    case "weapon":  //Summon weapon
                        if(Globals.currentScene.cases[target.x, target.y].entity != null)
                        {
                            if (Globals.currentScene.cases[target.x, target.y].entity.type != "npc")
                            {
                                Fighter fighter = ((Fighter)Globals.currentScene.cases[target.x, target.y].entity);
                            }
                        }
                        break;

                    case "areaDamage":  //Give damage at center and less the further
                        foreach (Case c in Globals.currentScene.cases)
                        {
                            if (Math.Abs(c.x - target.x) + Math.Abs(c.y - target.y) <= power.area)
                            {
                                if (c.entity != null)
                                {
                                    c.entity.ChangeHP(effect.Value - (power.area - Math.Abs(c.x - target.x) + Math.Abs(c.y - target.y)));
                                }
                            }
                        }
                        break;

                    case "damage":  //Give same damage to all area
                        foreach (Case c in Globals.currentScene.cases)
                        {
                            if (Math.Abs(c.x - target.x) + Math.Abs(c.y - target.y) <= power.area)
                            {
                                if (c.entity != null)
                                {
                                    c.entity.ChangeHP(effect.Value);
                                }
                            }
                        }
                        break;

                    case "entity":  //Summon entity
                        break;

                    case "stateCaseArea":   //Change state of cases in area
                        foreach (Case c in Globals.currentScene.cases)
                        {
                            if (Math.Abs(c.x - target.x) + Math.Abs(c.y - target.y) <= power.area && c.type != "wall")
                            {
                                c.state = power.options[0];
                                Globals.cameraManager.ChangeObject("grid", c.x + ";" + c.y, "redraw");
                            }
                        }
                        break;

                    case "transport":
                        break;

                    case "lineDamage":
                        break;

                    case "lineStateCase":
                        break;
                }
            }
        }

        private void ExecuteAttack()
        {
            //TODO Execute attack
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
