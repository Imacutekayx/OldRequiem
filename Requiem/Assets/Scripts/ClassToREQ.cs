using Requiem.Class;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class which manage the class to xml convertor
    /// </summary>
    public static class ClassToREQ
    {
        /// <summary>
        /// Basic method of the class
        /// </summary>
        public static void Save()
        {
            //USEABLES
            Item coin = new Item("gold", 1, 1, "A simple coin of gold");
            Globals.useables.Add(coin);
            Useables();
            
            //ARMORS
            Armor charHelmet = new Armor("blackScarf", 10, 5, "A scarf used to cover Lennj's face", "helmet", "mage", null);
            Armor charDress = new Armor("blackDress", 20, 10, "A basic mage dress tainted in black", "armor", "mage", null);
            Dictionary<string, int> neckEffects = new Dictionary<string, int>
            {
                {"mp", 3 }
            };
            Armor charNecklace = new Armor("psychalCrystal", 0, 2, "A crystal which gives Lennj his powers", "necklace", "magic", neckEffects);
            Armor ennTunic = new Armor("goblinTunic", 20, 4, "A disgusting, small tunic", "armor", "light", null);
            Globals.armors.Add(charHelmet);
            Globals.armors.Add(charDress);
            Globals.armors.Add(charNecklace);
            Globals.armors.Add(ennTunic);
            Armors();
            
            //WEAPONS
            Weapon psychWeapon = new Weapon("psychalSpear", 0, 0, "A psychologic representation of a spear that Lennj can mentally control", "magic2", 3, 2, "pierce");
            Weapon sword = new Weapon("sword", 15, 15, "A simple iron sword", "one-handed", 2, 1, "sharp");
            Globals.weapons.Add(psychWeapon);
            Globals.weapons.Add(sword);
            Weapons();
            
            //CHARACTERS
            Dictionary<string, int> charEffects = new Dictionary<string, int>
            {
                { "weapon", 0 }
            };
            List<string> charOptions = new List<string>
            {
                "psychalSpear"
            };
            List<Power> charPowers = new List<Power>
            {
                new Power("Summon Weapon", 3, 0, 1, 90, 1, charEffects, "", false, charOptions)
            };
            charEffects = new Dictionary<string, int>
            {
                {"areaDamage;psychic", 4 }
            };
            charPowers.Add(new Power("Psychic Explosion", 5, 3, 3, 60, 10, charEffects));
            charEffects = new Dictionary<string, int>
            {
                {"damage;fire", 3},
                {"stateCase;fire", 0 },
                {"statePath;fire", 0 }
            };
            charPowers.Add(new Power("Pyrokinesy", 5, 5, 1, 15, 20, charEffects, "", false, charOptions));
            charEffects = new Dictionary<string, int>
            {
                {"stateCase;fire", 0 },
                {"damagePath;fire", 1 },
                {"stateCast;fire", 0 }
            };
            charPowers.Add(new Power("FireTransport", 1, 10, 0, 10, 15, charEffects, "fire", true));
            int[] charDices = { 35, 70, 85, 60, 47, 25, 55 };
            Dictionary<Item, int> charBag = new Dictionary<Item, int>
            {
                { Globals.useables[0], 100 }
            };
            Armor[] charArmor = new Armor[6];
            charArmor[0] = charHelmet;
            charArmor[1] = charDress;
            charArmor[3] = charNecklace;
            bool[] charArmorChange = new bool[6];
            charArmorChange[0] = true;
            charArmorChange[1] = true;
            charArmorChange[2] = true;
            charArmorChange[4] = true;
            charArmorChange[5] = true;
            Weapon[] charWeapon = new Weapon[2];
            Globals.characters.Add(new Character("Lennj", true, 300, "Lennj lost his family at his birthday when Angels arrived to kill his race", "Psychomancien", "513", 
                "calm;sadistic;logical", "Soft-Cliff", 10, charDices, 0, new List<string>() { "common" }, new List<string>(), new List<string>(), new List<string>(), new List<string>(), charArmor, charArmorChange, "mage;magic", charWeapon, "magic1;magic2;dagger", charPowers, charBag));
            Characters();
            
            //ENNEMIES
            Dictionary<string, int> ennEffects = new Dictionary<string, int>
            {
                { "damage;sharp", 1 }
            };
            List<Power> ennPowers = new List<Power>
            {
                new Power("Scratch", 0, 1, 1, 0, 2, ennEffects)
            };
            Dictionary<string, int> ennScripts = new Dictionary<string, int>
            {
                { "callFriends", 100 }
            };
            int[] ennDices = { 30, 20, 25, 15, 40, 30, 40 };
            Dictionary<Item, int> ennBag = new Dictionary<Item, int>
            {
                { Globals.useables[0], 3}
            };
            Armor[] ennArmor = new Armor[6];
            bool[] ennArmorChange = new bool[6];
            Weapon[] ennWeapon = new Weapon[2];
            Globals.ennemies.Add(new Ennemy("CavernGoblin", "Small and twisted, this evil creature haunts many dark places",
                ennDices, 0, ennPowers, "", ennBag, new List<string>() { "common" }, new List<string>(), new List<string>(), 
                new List<string>(), new List<string>() { "fire" }, ennArmor, ennArmorChange, "", ennWeapon, ennScripts));
            Ennemies();
            
            //NPCS
            Dictionary<Item, int> npcBag = new Dictionary<Item, int>
            {
                {Globals.useables[0], 30}
            };
            Npc npc = new Npc("Butcher", true, 41, "butcher", "hotblood", true, new List<string>() { "common" }, npcBag);
            Globals.npcs.Add(npc);
            Npcs();
            
            //SCENES
            LayerImage sceBackground = new LayerImage("grass", 20, 20);
            List<string> sceParameters = new List<string>
            {
                "weapon;sword",
                "useable;gold;5"
            };
            List<LayerImage> sceAdds1 = new List<LayerImage>
            {
                new LayerImage("bigChest", 1, 7, 1, 50, 1, 1, new List<LayerScript>{ new LayerScript("openChest", true, sceParameters)}),
                new LayerImage("bigRock", 3, 7, 1, 45)
            };
            List<LayerImage> sceWalls = new List<LayerImage>
            {
                new LayerImage("ennemyWestWall", 1, 2, 1, 100, 1, 2),
                new LayerImage("ennemyEastWall", 3, 1, 3, 100, 1, 3),
                new LayerImage("ennemySouthWall", 1, 4, 0, 100, 3, 1),
                new LayerImage("npcNorthWall", 7, 0, 2, 100, 3, 1),
                new LayerImage("npcEastWall", 9, 1, 3, 100, 1, 2)
            };
            List<Case> sceCases = new List<Case>
            {
                new Case(4, 2, "free", 0, true, 2)
            };
            List<LayerScript> sceScripts = new List<LayerScript>
            {
                new LayerScript("caseState", true, "circle", 3, 0, 7, 3, 3, new List<string>{"circle;fire"})
            };
            Globals.characters[0].x = 7;
            Globals.characters[0].y = 8;
            Globals.characters[0].face = 0;
            Globals.ennemies[0].x = 2;
            Globals.ennemies[0].y = 3;
            Globals.ennemies[0].face = 0;
            Globals.npcs[0].x = 8;
            Globals.npcs[0].y = 1;
            Globals.npcs[0].face = 3;
            List<Entity> entities = new List<Entity>
            {
                Globals.characters[0], Globals.ennemies[0], Globals.npcs[0]
            };
            Globals.scenes.Add(new Scene("forestStart", 10, 10, "exploration", 4, new List<LayerImage>(),
                sceAdds1, new List<LayerImage>(), sceWalls, sceCases, sceScripts, entities));
            Scenes();
            
            //DIALOGUES
            Dictionary<string, string> diaAnswers = new Dictionary<string, string>
            {
                { "Kill", "I will kill you!" }
            };
            Dictionary<string, Dictionary<string, string>> diaChars = new Dictionary<string, Dictionary<string, string>>
            {
                { "Lennj", diaAnswers }
            };
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> diaParts = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
            {
                { "Butcher;Hello adventurers!", diaChars }
            };
            Globals.dialogues.Add(diaParts);
            Dialogues();
        }

        /// <summary>
        /// Create the useables.req
        /// </summary>
        private static void Useables()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("useables.req", FileMode.Create, FileAccess.Write);
            foreach(Item useable in Globals.useables)
            {
                formatter.Serialize(stream, useable);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the armors.req
        /// </summary>
        private static void Armors()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("armors.req", FileMode.Create, FileAccess.Write);
            foreach (Armor armor in Globals.armors)
            {
                formatter.Serialize(stream, armor);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the weapons.req
        /// </summary>
        private static void Weapons()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("weapons.req", FileMode.Create, FileAccess.Write);
            foreach (Weapon weapon in Globals.weapons)
            {
                formatter.Serialize(stream, weapon);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the characters.req
        /// </summary>
        private static void Characters()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("characters.req", FileMode.Create, FileAccess.Write);
            foreach (Character character in Globals.characters)
            {
                formatter.Serialize(stream, character);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the ennemies.req
        /// </summary>
        private static void Ennemies()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("ennemies.req", FileMode.Create, FileAccess.Write);
            foreach (Ennemy ennemy in Globals.ennemies)
            {
                formatter.Serialize(stream, ennemy);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the npcs.req
        /// </summary>
        private static void Npcs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("npcs.req", FileMode.Create, FileAccess.Write);
            foreach (Npc npc in Globals.npcs)
            {
                formatter.Serialize(stream, npc);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the scenes.req
        /// </summary>
        private static void Scenes()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("scenes.req", FileMode.Create, FileAccess.Write);
            foreach (Scene scene in Globals.scenes)
            {
                formatter.Serialize(stream, scene);
            }
            stream.Close();
        }

        /// <summary>
        /// Create the dialogues.req
        /// </summary>
        private static void Dialogues()
        {
            //TODO Object Dialogue
        }
    }
}
