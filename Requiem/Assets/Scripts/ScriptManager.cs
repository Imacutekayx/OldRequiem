using Requiem.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class which manage the scripts and powers
    /// </summary>
    public class ScriptManager
    {
        //Variables
        int nextCase = 0;

        //Objects
        List<Location> path = new List<Location>();
        Location current;

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
                                        c.ChangeState(temp[1]);
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
                case "castAttack":
                    string[] psC = act.parameters.Split(';');
                    string[] nameC = psC[0].Split(':');
                    string[] coordC = psC[1].Split(':');
                    foreach (Power p in act.type == "castPower" ? ((Fighter)act.launcher).powers : ((Fighter)act.launcher).weapons[Convert.ToInt32(nameC[0])].powers)
                    {
                        if(p.name == nameC[act.type == "castPower" ? 0 : 1])
                        {
                            //TODO Find path from possible basic
                            path = Globals.movementManager.CalculateMove(new Location(act.launcher.x, act.launcher.y), new Location(Convert.ToInt32(coordC[0]), Convert.ToInt32(coordC[1])));
                            nextCase = path.Count - 1;
                            current = path[nextCase];
                            int temp = ((Fighter)act.launcher).mp;
                            if (act.type == "castPower") { ((Fighter)act.launcher).ChangeMP(p.mana); }
                            Globals.timeManager.AddAction(new Act("script", p.speed, act.type == "castPower" ? "executePower" : "executeAttack", act.launcher, act.parameters));
                            Debug.Log(act.launcher.name + ":" + temp + "=>" + ((Fighter)act.launcher).mp);
                            break;
                        }
                    }
                    break;

                case "executePower":
                case "executeAttack":
                    string[] psE = act.parameters.Split(';');
                    string[] nameE = psE[0].Split(':');
                    string[] coordE = psE[1].Split(':');
                    foreach(Power p in act.type == "executePower" ? ((Fighter)act.launcher).powers : ((Fighter)act.launcher).weapons[Convert.ToInt32(nameE[0])].powers)
                    {
                        if(p.name == nameE[act.type == "executePower" ? 0 : 1])
                        {
                            if (path.Count != 1)
                            {
                                Location temp = current;
                                current = path[--nextCase];
                                //If is obstacle
                                if(Math.Abs(current.x - temp.x) + Math.Abs(current.y - temp.y) == 2)
                                {
                                    int obsX = current.x == temp.x ? current.x : (current.x > temp.x ? temp.x + 1 : current.x + 1); 
                                    int obsY = current.y == temp.y ? current.y : (current.y > temp.y ? temp.y + 1 : current.y + 1);
                                    //TODO UP Better way to pass obstacle?
                                    if (UnityEngine.Random.Range(1, 100) > 100 - Globals.currentScene.cases[obsX, obsY].high)
                                    {
                                        Debug.Log("Failure");
                                        nextCase = 0;
                                        coordE[0] = Convert.ToString(obsX);
                                        coordE[1] = Convert.ToString(obsY);
                                    }
                                    else
                                    {
                                        Debug.Log("Success");
                                    }
                                }
                            }
                            if (nextCase == 0)
                            {
                                ExecutePower(p, (Fighter)act.launcher, new Location(Convert.ToInt32(coordE[0]), Convert.ToInt32(coordE[1])));
                                Globals.timeManager.AddAction(new Act("time", act.type == "executePower" ? 20 : 15, "", act.launcher, ""));
                            }
                            else
                            {
                                foreach(KeyValuePair<string, int> effect in p.effects)
                                {
                                    if (effect.Key.Contains("Path"))
                                    {
                                        string[] effectType = effect.Key.Split(';');
                                        switch (effectType[0])
                                        {
                                            case "statePath":
                                                Globals.currentScene.cases[current.x, current.y].ChangeState(effectType[1]);
                                                Globals.cameraManager.ChangeObject("grid", current.x + ";" + current.y, "redraw");
                                                break;
                                        }
                                    }
                                }
                                Globals.timeManager.AddAction(new Act("script", p.speed, act.type, act.launcher, act.parameters));
                            }
                            break;
                        }
                    }
                    break;

                case "useItem":
                    string[] split = act.parameters.Split(';');
                    string[] cases = split[1].Split(':');
                    foreach(Item item in Globals.useables)
                    {
                        if(item.name == split[0])
                        {
                            UseItem(item, new Location(Convert.ToInt32(cases[0]), Convert.ToInt32(cases[1])));
                            break;
                        }
                    }
                    break;
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
                Globals.visibilityManager.Compute(5, target, power.area);
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
            //ConeX => increment area each X cases
            //TODO Add effects (entity/transport/lineDamage/lineStateCase/coneDamage/coneStateCase)
            foreach(KeyValuePair<string, int> effect in power.effects)
            {
                string[] effectType = effect.Key.Split(';');
                switch (effectType[0])
                {
                    case "weapon":  //Summon weapon
                        if(Globals.currentScene.cases[target.x, target.y].entity != null)
                        {
                            if (Globals.currentScene.cases[target.x, target.y].entity.type != "npc")
                            {
                                Fighter fighter = ((Fighter)Globals.currentScene.cases[target.x, target.y].entity);
                                string[] options = power.options[0].Split(';');
                                for(int i = 0; i < options.Length; ++i)
                                {
                                    foreach (Weapon weapon in Globals.weapons)
                                    {
                                        if (weapon.name == options[i])
                                        {
                                            fighter.AddWeapon(weapon);
                                            Debug.Log(fighter.name + ":" + weapon.name);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case "areaDamage":
                    case "damage":
                    case "stateCase":
                        List<Case> cases = Globals.visibilityManager.Compute(6, target, power.area);
                        foreach(Case c in cases)
                        {
                            if(effectType[0] == "stateCase")
                            {
                                c.ChangeState(effectType[1]);
                                Globals.cameraManager.ChangeObject("grid", c.x + ";" + c.y, "redraw");
                            }
                            else if(c.entity != null)
                            {
                                int dmg = effect.Value + (caster.boosts.ContainsKey(effectType[1] + "Damage") ? caster.boosts[effectType[1] + "Damage"] : 0);
                                c.entity.ChangeHP(effectType[0] == "damage" ? dmg : dmg - (power.area - Math.Abs(c.x - target.x) + Math.Abs(c.y - target.y)), effectType[1]);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Use an item on a given case
        /// </summary>
        /// <param name="item">Item used</param>
        /// <param name="target">Case targeted</param>
        public void UseItem(Item item, Location target)
        {
            foreach(KeyValuePair<string, int> effect in item.effects)
            {
                switch (effect.Key)
                {
                    case "hp":
                        if(Globals.currentScene.cases[target.x, target.y].entity != null)
                        {
                            Globals.currentScene.cases[target.x, target.y].entity.ChangeHP(effect.Value, "", true);
                        }
                        break;
                }
            }
        }
    }
}
