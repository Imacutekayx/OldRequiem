using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Parent class of entity
    /// </summary>
    public class Entity
    {
        //Variables
        public string name;
        public bool sex = true; //true = male
        public int age = 0;
        public string type;
        public byte face = 0; //0=S/1=W/2=N/3=E
        public int x;
        public int y;
        public int weight;
        public int height;
        public bool dead;
        public bool busy = false;

        //Objects
        public Dictionary<Item, int> bag = new Dictionary<Item, int>();

        /// <summary>
        /// Deal damage to an entity
        /// </summary>
        /// <param name="hp">Nbr of damage</param>
        public void ChangeHP(int hp)
        {
            if(type == "npc")
            {
                bool isEnnemy = false;
                foreach(Ennemy ennemy in Globals.ennemies)
                {
                    if(ennemy.name == name)
                    {
                        //TODO Summon ennemy
                        isEnnemy = true;
                        break;
                    }
                }
                if (!isEnnemy)
                {
                    dead = true;
                }
            }
            else
            {
                Fighter fighter = ((Fighter)this);
                fighter.hp -= hp;
                if(fighter.hp <= 0)
                {
                    fighter.dead = true;
                }
            }
        }

        /// <summary>
        /// Check if an item can be added to the bag and add it if possible
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="nbr">Nbr of items</param>
        /// <returns>Object added</returns>
        public bool AddItem(Item item, int nbr)
        {
            bool valid = true;
            if (type == "character")
            {
                Character character = ((Character)this);
                if (character.strength + item.weight * nbr > character.dices[0] * 10)
                {
                    valid = false;
                }
                else
                {
                    character.strength += item.weight * nbr;
                }
            }
            if (valid)
            {
                if (bag.ContainsKey(item))
                {
                    bag[item] += nbr;
                }
                else
                {
                    bag.Add(item, nbr);
                }
            }
            return valid;
        }

        /// <summary>
        /// Drop an item from the bag of the entity to the ground
        /// </summary>
        /// <param name="item">Item to drop</param>
        /// <param name="nbr">Nbr of this item</param>
        public void DropItem(Item item, int nbr, bool inBag = true)
        {
            if(Globals.currentScene.cases[x, y].items.ContainsKey(item))
            {
                Globals.currentScene.cases[x, y].items[item] += nbr;
            }
            else
            {
                Globals.currentScene.cases[x, y].items.Add(item, nbr);
            }
            if (inBag)
            {
                if (nbr < bag[item])
                {
                    bag[item] -= nbr;
                }
                else
                {
                    bag.Remove(item);
                }
                if (type == "character")
                {
                    ((Character)this).strength -= item.weight * nbr;
                }
            }
        }

        /// <summary>
        /// Take item from the ground and put it in the bag
        /// </summary>
        /// <param name="item">Item to take</param>
        /// <param name="nbr">Nbr of this item</param>
        public void TakeItem(Item item, int nbr)
        {
            if (AddItem(item, nbr))
            {
                if (nbr < Globals.currentScene.cases[x, y].items[item])
                {
                    Globals.currentScene.cases[x, y].items[item] -= nbr;
                }
                else
                {
                    Globals.currentScene.cases[x, y].items.Remove(item);
                }
            }
        }

        public void CheckBag()
        {
            //TODO Show items of entity
        }
    }
}