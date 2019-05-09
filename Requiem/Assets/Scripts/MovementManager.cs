using Requiem.Class;
using Requiem.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class which manage all related to the movements and basic interactions of the player
    /// </summary>
    public class MovementManager
    {
        //Variable
        public int nextCase = 0;

        //Objects
        public List<Location> path = new List<Location>();

        /// <summary>
        /// Method which calculate the better path to the target
        /// </summary>
        /// <param name="start">Basic position</param>
        /// <param name="target">Target position</param>
        public List<Location> CalculateMove(Location start, Location target)
        {
            Location current = null;
            List<Location> openList = new List<Location>();
            List<Location> closedList = new List<Location>();
            int varG = 0;

            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.f);
                current = openList.First(l => l.f == lowest);

                // add the current square to the closed list
                closedList.Add(current);

                // remove it from the open list
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.x == target.x && l.y == target.y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.x, current.y);
                varG++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.x == adjacentSquare.x
                            && l.y == adjacentSquare.y) != null)
                        continue;

                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.x == adjacentSquare.x
                            && l.y == adjacentSquare.y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.g = varG;
                        adjacentSquare.h = ComputeHScore(adjacentSquare.x, adjacentSquare.y, target.x, target.y);
                        adjacentSquare.f = adjacentSquare.g + adjacentSquare.h;
                        adjacentSquare.parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (varG + adjacentSquare.h < adjacentSquare.f)
                        {
                            adjacentSquare.g = varG;
                            adjacentSquare.f = adjacentSquare.g + adjacentSquare.h;
                            adjacentSquare.parent = current;
                        }
                    }
                }
            }
            List<Location> newPath = new List<Location>();
            while(current != null)
            {
                newPath.Add(current);
                current = current.parent;
            }
            return newPath;
        }

        /// <summary>
        /// Start moving from your position
        /// </summary>
        public void StartMove()
        {
            bool success = true;
            string direction = path[nextCase - 1].x != path[nextCase].x ? (path[nextCase - 1].x > path[nextCase].x ? "right" : "left") : (path[nextCase - 1].y > path[nextCase].y ? "down" : "up");
            Act action = new Act("movement", Globals.currentCharacter.dices[0]/2, "move", Globals.currentCharacter, direction);
            switch (direction)
            {
                case "up":
                    if(Globals.currentScene.cases[path[nextCase].x, path[nextCase].y - 1].type == "add1")
                    {
                        action.type = "obstacle";
                        //TODO Find the good algo for successfully passing an obstacle
                    }
                    break;

                case "down":
                    if (Globals.currentScene.cases[path[nextCase].x, path[nextCase].y + 1].type == "add1")
                    {
                        action.type = "obstacle";
                    }
                    break;

                case "left":
                    if (Globals.currentScene.cases[path[nextCase].x - 1, path[nextCase].y].type == "add1")
                    {
                        action.type = "obstacle";
                    }
                    break;

                case "right":
                    if (Globals.currentScene.cases[path[nextCase].x + 1, path[nextCase].y].type == "add1")
                    {
                        action.type = "obstacle";
                    }
                    break;
            }

            if (success)
            {
                Globals.timeManager.add.Add(action);
                --nextCase;
            }
            else
            {
                Debug.Log("Failure");
            }
        }

        /// <summary>
        /// Execute an action triggered by the time manager
        /// </summary>
        /// <param name="action">Action to execute</param>
        public void Execute(Act action)
        {
            //Sort by the type of the action
            switch (action.type)
            {
                //Move the launcher based on the direction
                case "move":
                case "obstacle":
                    Debug.Log(action.type + "/" + action.parameters);
                    int nbr = action.type == "move" ? 1 : 2;
                    Globals.cameraManager.grid[action.launcher.x, action.launcher.y].GetComponent<CaseObject>().c.entity = null;
                    Globals.currentScene.cases[action.launcher.x, action.launcher.y].type = "free";
                    Globals.cameraManager.ChangeObject("grid", action.launcher.x + ";" + action.launcher.y, "redraw");
                    switch (action.parameters)
                    {
                        case "up":
                            action.launcher.y = action.launcher.y - nbr;
                            action.launcher.face = 0;
                            break;

                        case "down":
                            action.launcher.y = action.launcher.y + nbr;
                            action.launcher.face = 2;
                            break;

                        case "left":
                            action.launcher.x = action.launcher.x - nbr;
                            action.launcher.face = 3;
                            break;

                        case "right":
                            action.launcher.x = action.launcher.x + nbr;
                            action.launcher.face = 1;
                            break;
                    }
                    Globals.currentScene.cases[action.launcher.x, action.launcher.y].type = action.launcher.type;
                    if (nextCase != 0)
                    {
                        StartMove();
                    }
                    else
                    {
                        Globals.timeManager.AddAction(new Act("time", 10, "", action.launcher, ""));
                    }

                    //Trigger scripts
                    foreach(LayerScript script in Globals.currentScene.scripts)
                    {
                        if (script.state)
                        {
                            switch (script.typeTrigger)
                            {
                                case "circle":      //Position + weight as area
                                    if (ComputeHScore(action.launcher.x, action.launcher.y, script.x, script.y) < script.weight)
                                    {
                                        Globals.scriptManager.ExecuteScript(script, action.launcher);
                                    }
                                    break;

                                case "square":      //PositionX + weight and PositionY + height
                                    if (action.launcher.x >= script.x && action.launcher.x < script.x + script.weight &&
                                        action.launcher.x >= script.y && action.launcher.y < script.y + script.height)
                                    {
                                        Globals.scriptManager.ExecuteScript(script, action.launcher);
                                    }
                                    break;
                            }
                        }
                    }

                    //Redraw
                    Globals.cameraManager.grid[action.launcher.x, action.launcher.y].GetComponent<CaseObject>().c.entity = action.launcher;
                    Globals.cameraManager.ChangeObject("grid", action.launcher.x + ";" + action.launcher.y, "redraw");
                    Globals.cameraManager.ChangeObject(action.launcher.type, action.launcher.name, "move");
                    break;
            }
        }

        /// <summary>
        /// Check accessible locations
        /// </summary>
        /// <param name="x">current x</param>
        /// <param name="y">current y</param>
        /// <returns>List of accessible location</returns>
        private List<Location> GetWalkableAdjacentSquares(int x, int y)
        {
            List<Location> returnable = new List<Location>();

            //Left
            if (x > 0)
            {
                if (x > 1 && Globals.currentScene.cases[x - 1, y].type == "add1")
                {
                    if (Globals.currentScene.cases[x - 2, y].type != "add1" && Globals.currentScene.cases[x - 2, y].type != "wall")
                    {
                        returnable.Add(new Location(x - 2, y));
                    }
                }
                else if(Globals.currentScene.cases[x-1, y].type == "free")
                {
                    returnable.Add(new Location(x - 1, y));
                }
            }

            //Right
            if (x < Globals.currentScene.weight - 1)
            {
                if (x > 1 && Globals.currentScene.cases[x + 1, y].type == "add1")
                {
                    if (Globals.currentScene.cases[x + 2, y].type != "add1" && Globals.currentScene.cases[x + 2, y].type != "wall")
                    {
                        returnable.Add(new Location(x + 2, y));
                    }
                }
                else if (Globals.currentScene.cases[x + 1, y].type == "free")
                {
                    returnable.Add(new Location(x + 1, y));
                }
            }

            //Up
            if (y > 0)
            {
                if (x > 1 && Globals.currentScene.cases[x, y - 1].type == "add1")
                {
                    if (Globals.currentScene.cases[x, y - 2].type != "add1" && Globals.currentScene.cases[x - 2, y].type != "wall")
                    {
                        returnable.Add(new Location(x, y - 2));
                    }
                }
                else if (Globals.currentScene.cases[x, y - 1].type == "free")
                {
                    returnable.Add(new Location(x, y - 1));
                }
            }

            //Down
            if (y < Globals.currentScene.height - 1)
            {
                if (x > 1 && Globals.currentScene.cases[x, y + 1].type == "add1")
                {
                    if (Globals.currentScene.cases[x, y + 2].type != "add1" && Globals.currentScene.cases[x + 2, y].type != "wall")
                    {
                        returnable.Add(new Location(x, y + 2));
                    }
                }
                else if (Globals.currentScene.cases[x, y + 1].type == "free")
                {
                    returnable.Add(new Location(x, y + 1));
                }
            }

            return returnable;
        }

        /// <summary>
        /// Calculate the h score of the location
        /// </summary>
        /// <param name="x">x of current</param>
        /// <param name="y">y of current</param>
        /// <param name="targetX">x of target</param>
        /// <param name="targetY">x of target</param>
        /// <returns>H score</returns>
        private int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
