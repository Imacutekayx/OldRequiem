using Requiem.Class;
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
        int nextCase = 0;

        //Objects
        List<Location> path = new List<Location>();

        //TEMP/////////////////////////
        //Location[] movingPatern = { new Location(2, 7), new Location(7, 1), new Location(2, 2), new Location(7, 8)};
        Location[] movingPatern = { new Location(2, 7), new Location(7, 8)};
        int currentMov = -1;
        public void TempPatern()
        {
            ++currentMov;
            if (currentMov == movingPatern.Length) { currentMov = 0; }
            path = CalculateMove(new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), movingPatern[currentMov]);
            nextCase = path.Count - 1;
            StartMove();
        }
        ///////////////////////////////

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
                    if(Globals.currentScene.cases[path[nextCase].x, path[nextCase].y - 1].type == "obstacle")
                    {
                        action.type = "obstacle";
                        //TODO Find the good algo for successfully passing an obstacle
                    }
                    break;

                case "down":
                    if (Globals.currentScene.cases[path[nextCase].x, path[nextCase].y + 1].type == "obstacle")
                    {
                        action.type = "obstacle";
                    }
                    break;

                case "left":
                    if (Globals.currentScene.cases[path[nextCase].x - 1, path[nextCase].y].type == "obstacle")
                    {
                        action.type = "obstacle";
                    }
                    break;

                case "right":
                    if (Globals.currentScene.cases[path[nextCase].x + 1, path[nextCase].y].type == "obstacle")
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
                path = CalculateMove(new Location(path[nextCase].x, path[nextCase].y), movingPatern[currentMov]);
                nextCase = path.Count - 1;
                StartMove();
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
                    int nbr = action.type == "move" ? 1 : 2;
                    Globals.cameraManager.ChangeObject("baseGrid", action.launcher.x + ";" + action.launcher.y, "redraw");
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
                    if(nextCase != 0)
                    {
                        StartMove();
                    }
                    else
                    {
                        TempPatern();
                    }
                    Globals.cameraManager.ChangeObject("characterGrid", action.launcher.x + ";" + action.launcher.y, "redraw");
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
                if (Globals.currentScene.cases[x - 1, y].type != "wall")
                {
                    if (x > 1 && Globals.currentScene.cases[x - 1, y].type == "obstacle")
                    {
                        if (Globals.currentScene.cases[x - 1, y].high <= Globals.currentCharacter.dices[4])
                        {
                            returnable.Add(new Location(x - 2, y));
                        }
                    }
                    else
                    {
                        returnable.Add(new Location(x - 1, y));
                    }
                }
            }

            //Right
            if (x < Globals.currentScene.weight - 1)
            {
                if (Globals.currentScene.cases[x + 1, y].type != "wall")
                {
                    if (x < Globals.currentScene.weight - 2 && Globals.currentScene.cases[x + 1, y].type == "obstacle")
                    {
                        if (Globals.currentScene.cases[x + 1, y].high <= Globals.currentCharacter.dices[4])
                        {
                            returnable.Add(new Location(x + 2, y));
                        }
                    }
                    else
                    {
                        returnable.Add(new Location(x + 1, y));
                    }
                }
            }

            //Up
            if (y > 0)
            {
                if (Globals.currentScene.cases[x, y - 1].type != "wall")
                {
                    if (y > 1 && Globals.currentScene.cases[x, y - 1].type == "obstacle")
                    {
                        if (Globals.currentScene.cases[x, y - 1].high <= Globals.currentCharacter.dices[4])
                        {
                            returnable.Add(new Location(x, y - 2));
                        }
                    }
                    else
                    {
                        returnable.Add(new Location(x, y - 1));
                    }
                }
            }

            //Down
            if (y < Globals.currentScene.height - 1)
            {
                if (Globals.currentScene.cases[x, y + 1].type != "wall")
                {
                    if (y > Globals.currentScene.height - 2 && Globals.currentScene.cases[x, y + 1].type == "obstacle")
                    {
                        if (Globals.currentScene.cases[x, y + 1].high <= Globals.currentCharacter.dices[4])
                        {
                            returnable.Add(new Location(x, y + 2));
                        }
                    }
                    else
                    {
                        returnable.Add(new Location(x, y + 1));
                    }
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
