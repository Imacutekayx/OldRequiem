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
            //Variables
            bool active = false;

            //Objects
            List<string> parametersFailed = new List<string>();

            //Switch script
            switch (script.name)
            {
                //Open a chest and get what is inside //=> Show it and choose
                case "openChest":
                    Debug.Log("Before:" + Globals.currentCharacter.strength + ";" + Globals.currentCharacter.bag.Count());
                    foreach(string parameter in script.parameters)
                    {
                        bool succeed = true;
                        string[] temp = parameter.Split(';');
                        switch (temp[0])
                        {
                            case "weapon":
                                foreach(Weapon weapon in Globals.weapons)
                                {
                                    if(weapon.name == temp[1])
                                    {
                                        if(!Globals.currentCharacter.AddItem(weapon, 1))
                                        {
                                            active = true;
                                            succeed = false;
                                        }
                                        break;
                                    }
                                }
                                break;

                            case "armor":
                                foreach(Armor armor in Globals.armors)
                                {
                                    if(armor.name == temp[1])
                                    {
                                        if (!Globals.currentCharacter.AddItem(armor, 1))
                                        {
                                            active = true;
                                            succeed = false;
                                        }
                                        break;
                                    }
                                }
                                break;

                            case "useable":
                                foreach(Item useable in Globals.useables)
                                {
                                    if(useable.name == temp[1])
                                    {
                                        if (!Globals.currentCharacter.AddItem(useable, Convert.ToInt32(temp[2])))
                                        {
                                            active = true;
                                            succeed = false;
                                        }
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
                        if (!succeed)
                        {
                            parametersFailed.Add(parameter);
                        }
                    }
                    Debug.Log("After:" + Globals.currentCharacter.strength + ";" + Globals.currentCharacter.bag.Count());
                    if(Globals.currentScene.gamemode == "fight")
                    {
                        Globals.timeManager.AddAction(new Act("time", 15, "", Globals.currentCharacter, ""));
                    }
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
            script.parameters = parametersFailed;
            script.state = active;
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
                            ((Fighter)act.launcher).mp -= p.mana;
                            //TODO find path to target (from possible basic) and new act like movement
                            break;
                        }
                    }
                    break;

                case "executePower":

                    Globals.timeManager.AddAction(new Act("time", 20, "", act.launcher, ""));
                    break;

                case "executeAttack":

                    Globals.timeManager.AddAction(new Act("time", 15, "", act.launcher, ""));
                    break;
            }
        }

        /// <summary>
        /// Show a given power's scope
        /// </summary>
        /// <param name="power">Power sent</param>
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
        /// Show a given power's area
        /// </summary>
        /// <param name="power">Power sent</param>
        /// <param name="target">Target of the spell</param>
        /// <param name="basic">Basic position of the spell</param>
        public void ShowPowerArea(Power power, Location target, Location basic = null)
        {
            if (!power.needBasic)
            {
                //TODO ShowPowerArea on enter
            }
            else
            {

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

                    case "stateCase":   //Change state of cases in area
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
    }
}
