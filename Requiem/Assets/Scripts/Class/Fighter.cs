﻿using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Parent class of all fighting entity
    /// </summary>
    public class Fighter : Entity
    {
        //Combat
        public int hp;
        public int mp;
        public int[] dices;             //Constitution/Knowledge/Focus/Charisma/Agility/Arcane/Temper
        public Weapon[] weapons;
        public string weapontype;
        public List<Power> powers;
        public Armor[] armors;           //Helmet/Armor/Boots/Necklace/LRing/RRing
        public bool[] armorChange;
        public string armortype;
        
        /// <summary>
        /// Add a weapon to the fighter
        /// </summary>
        /// <param name="weapon">Weapon to add</param>
        public void AddWeapon(Weapon weapon, byte part = 0)
        {
            if (weapontype.Contains(weapon.type))
            {
                switch (weapon.type)
                {
                    case "one-handed":
                    case "dagger":
                    case "magic1":
                    case "crossbow1":
                        if (weapons[part] != null)
                        {
                            if (weapons[part].type.Contains("magic"))
                            {
                                if (!AddItem(weapons[part], 1))
                                {
                                    DropItem(weapons[part], 1, false);
                                }
                            }
                        }
                        else
                        {
                            if (part == 1 && weapons[0] != null)
                            {
                                switch (weapons[0].type)
                                {
                                    case "two-handed":
                                    case "magic2":
                                    case "bow":
                                    case "crossbow2":
                                        if (weapons[0].type != "magic2")
                                        {
                                            if (!AddItem(weapons[0], 1))
                                            {
                                                DropItem(weapons[0], 1, false);
                                            }
                                        }
                                        weapons[0] = null;
                                        break;
                                }
                            }
                        }
                        weapons[part] = weapon;
                        break;

                    case "two-handed":
                    case "magic2":
                    case "bow":
                    case "crossbow2":
                        for (int i = 0; i < 2; ++i)
                        {
                            if (weapons[i] != null)
                            {
                                if (!weapons[i].type.Contains("magic"))
                                {
                                    if (!AddItem(weapon, 1))
                                    {
                                        DropItem(weapons[i], 1, false);
                                    }
                                }
                            }
                        }
                        weapons[0] = weapon;
                        break;
                }
            }
        }

        /// <summary>
        /// Add an armor to the fighter
        /// </summary>
        /// <param name="armor">Armor to add</param>
        public void AddArmor(Armor armor, byte hand = 0)
        {
            if (armortype.Contains(armor.type))
            {
                byte part = 0;
                switch (armor.part)
                {
                    case "armor":
                        part = 1;
                        break;

                    case "boots":
                        part = 2;
                        break;

                    case "necklace":
                        part = 3;
                        break;

                    case "ring":
                        part = hand;
                        break;
                }
                if (armorChange[part])
                {
                    if(armors[part] != null)
                    {
                        if(armors[part].type != "magic")
                        {
                            if(!AddItem(armors[part], 1))
                            {
                                DropItem(armors[part], 1, false);
                            }
                        }
                    }
                    armors[part] = armor;
                }
            }
        }
    }
}