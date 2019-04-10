using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Requiem.Class;
using Requiem;

namespace Requiem
{
    /// <summary>
    /// Class which manage all related to the movements and basic interactions of the player
    /// </summary>
    public class MovementManager
    {
        //Objects
        List<Location> path;

        /// <summary>
        /// Method which calculate the better path to the target
        /// </summary>
        /// <param name="start">Basic position</param>
        /// <param name="target">Target position</param>
        public void CalculateMove(Location start, Location target)
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

            while(current != null)
            {
                path.Add(current);
                current = current.parent;
            }
        }

        /// <summary>
        /// Start moving from your position
        /// </summary>
        public void StartMove()
        {
            Location current = path.Last();
            Globals.timeManager.AddAction(new Act("movement", Globals.currentCharacter.dices[0]/2, "move", Globals.currentCharacter, 
                current.x != Globals.currentCharacter.x ? current.x > Globals.currentCharacter.x ? "up" : "down" : current.y > Globals.currentCharacter.y ? "left" : "right"));
            path.Remove(path.Last());
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
                    returnable.Add(new Location(x - 1, y));
                }
            }

            //Right
            if (x < Globals.currentScene.weight - 1)
            {
                if (Globals.currentScene.cases[x + 1, y].type != "wall")
                {
                    returnable.Add(new Location(x + 1, y));
                }
            }

            //Up
            if (y > 0)
            {
                if (Globals.currentScene.cases[x, y - 1].type != "wall")
                {
                    returnable.Add(new Location(x, y - 1));
                }
            }

            //Down
            if (y < Globals.currentScene.height - 1)
            {
                if (Globals.currentScene.cases[x, y + 1].type != "wall")
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
        /// <returns></returns>
        private int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
