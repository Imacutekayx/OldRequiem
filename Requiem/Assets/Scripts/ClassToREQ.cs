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
            Item coin = new Item("coin", 1, 1, "A simple coin of gold");
            Dictionary<string, int> hpPotion_effects = new Dictionary<string, int>();
            hpPotion_effects.Add("hp", 10);
            Globals.useables.Add(coin);

            Item hpPotion = new Item("hp potion", 5, 2, "A potion which give 10hp", 1, hpPotion_effects);
            Globals.useables.Add(hpPotion);
            Useables();

            Item wolfSkin = new Item("uto skin", 3, 1, "The skin of a wild uto.");
            Globals.useables.Add(wolfSkin);

            Item gateKey = new Item("gate key", 0, 0, "The key needed for the forest's gate.");
            
            //ARMORS
            Dictionary<string, int> chainArmor_effects = new Dictionary<string, int>();
            chainArmor_effects.Add("armor", 2);
            Armor chainArmor = new Armor("chain armor", 30, 30, "A simple chain armor giving 2 points", "armor", "light", chainArmor_effects);
            Globals.armors.Add(chainArmor);
            Armors();
            
            //WEAPONS
            List<Power> flamberge_powers = new List<Power>();
            Dictionary<string, int> circularStrike_effects = new Dictionary<string, int>();
            circularStrike_effects.Add("areaDamage", 5);
            Power circularStrike = new Power("circular strike", 2, 0, 2, 20, 30, circularStrike_effects);
            flamberge_powers.Add(circularStrike);
            Dictionary<string, int> fireSlash_effects = new Dictionary<string, int>();
            fireSlash_effects.Add("damage;fire", 10);
            Power fireSlash = new Power("fire slash", 5, 1, 1, 50, 10, fireSlash_effects);
            flamberge_powers.Add(fireSlash);
            Dictionary<string, int> flamberge_effects = new Dictionary<string, int>();
            flamberge_effects.Add("fireDamage", 2);
            Weapon flamberge = new Weapon("flamberge", 50, 20, "A ondulated blade gives fireDamage to this two-handed sword", "two-handed", 5, 1, "sharp", flamberge_powers, flamberge_effects);
            Globals.weapons.Add(flamberge);

            List<Power> knife_powers = new List<Power>();
            Dictionary<string, int> fury_effects = new Dictionary<string, int>();
            fury_effects.Add("damage", 9);
            Power fury = new Power("fury", 6, 1, 1, 30, 0, fury_effects);
            knife_powers.Add(fury);
            Weapon knife = new Weapon("knife", 3, 5, "A simple knife.", "dagger", 3, 1, "sharp", knife_powers);
            Globals.weapons.Add(knife);

            Weapon fangs = new Weapon("fangs", 2, 1, "Wild uto's fangs.", "beast", 2, 1, "pierce");
            Globals.weapons.Add(fangs);
            Weapons();
            
            //CHARACTERS
            int[] kanis_dices = { 60, 50, 40, 70, 50, 10, 70 };
            List<string> kanis_languages = new List<string> { "Common", "Mercenary" };
            List<string> kanis_resistances = new List<string> { "fire" };
            Armor[] kanis_armor = new Armor[6];
            kanis_armor[1] = chainArmor;
            bool[] kanis_armorChange = new bool[6];
            kanis_armorChange[0] = true;
            kanis_armorChange[1] = true;
            kanis_armorChange[2] = true;
            kanis_armorChange[3] = true;
            kanis_armorChange[4] = true;
            kanis_armorChange[5] = true;
            Weapon[] kanis_weapon = new Weapon[2];
            kanis_weapon[0] = flamberge;
            List<Power> kanis_powers = new List<Power>();
            Dictionary<string, int> flameTeleport_effects = new Dictionary<string, int>();
            flameTeleport_effects.Add("stateCast;", 0);
            flameTeleport_effects.Add("stateCase;fire", 0);
            Power flameTeleport = new Power("flame teleport", 3, 5, 1, 30, 0, flameTeleport_effects, "fire", true);
            kanis_powers.Add(flameTeleport);
            Character kanis = new Character("Kanis", true, 29, "Kanis' home was burn by a man named Jhin. Killing him is the only goal of the mercenary.", "Mercenary", "human", "serious", "Sioris", 3,
                kanis_dices, 0, kanis_languages, new List<string>(), new List<string>(), kanis_resistances, new List<string>(),
                kanis_armor, kanis_armorChange, "light;heavy", kanis_weapon, "on-handed;dagger;crossbow1;two-handed;crossbow2", kanis_powers, new Dictionary<Item, int>());
            Globals.characters.Add(kanis);
            Characters();
            
            //ENNEMIES
            int[] wildUto_dices = { 50, 10, 30, 60, 70, 15, 55 };
            Dictionary<Item, int> wildUto_bag = new Dictionary<Item, int>();
            wildUto_bag.Add(wolfSkin, 1);
            List<string> wildUto_vulnerabilities = new List<string> { "fire" };
            Weapon[] wildUto_weapon = new Weapon[2];
            wildUto_weapon[0] = fangs;
            Ennemy wildUto = new Ennemy("wild uto", "A wild uto which seek meat.", 5, wildUto_dices, 0, new List<Power>(), "beast", wildUto_bag, new List<string>(), new List<string>(), new List<string>(), new List<string>(), wildUto_vulnerabilities, new Armor[6], new bool[6], "", wildUto_weapon);
            Globals.ennemies.Add(wildUto);
            
            int[] trap_dices = { 80, 50, 70, 50, 30, 65, 40 };
            Weapon[] trap_weapon = new Weapon[2];
            trap_weapon[0] = knife;
            Ennemy trap = new Ennemy("trap", "The trap is a filty creature taking the form of a coon or a uto to attract wanderers at night and stab them multiply in the chest before eating them. Its normal form is a tall and dark tainted humanoid without a face hiding under a long dress.", 10, 
                trap_dices, 0, new List<Power>(), "dagger", new Dictionary<Item, int>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new Armor[6], new bool[6], "", trap_weapon);
            Globals.ennemies.Add(trap);
            Ennemies();

            //NPCS
            List<string> lumberjack_languages = new List<string> { "Common" };
            Dictionary<Item, int> lumberJack_bag = new Dictionary<Item, int>();
            lumberJack_bag.Add(gateKey, 1);
            Npc lumberjack = new Npc("Jack The Lumberjack", true, 46, "lumberjack", "hotblood", true, lumberjack_languages, lumberJack_bag);
            Globals.npcs.Add(lumberjack);
            Npcs();

            //SCENES
            List<LayerImage> forestStart_adds1 = new List<LayerImage>();
            int[] forestStart_tree_x = { 5, 8, 9, 4, 5, 6, 7, 8, 9, 6, 7, 8, 9, 6, 8 };
            int[] forestStart_tree_y = { 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5 };
            for(int i = 0; i < forestStart_tree_x.Length; ++i)
            {
                LayerImage tree = new LayerImage("tree", forestStart_tree_x[i], forestStart_tree_y[i], 0, 120);
                forestStart_adds1.Add(tree);
            }
            int[] forestStart_barrier_x = { 0, 1, 3, 4, 5 };
            for (int i = 0; i < forestStart_barrier_x.Length; ++i)
            {
                LayerImage barrier = new LayerImage("barrier", forestStart_barrier_x[i], 5, 0, 105);
                forestStart_adds1.Add(barrier);
            }
            List<string> smallChest_parameters = new List<string> { "useable;hp potion;1" };
            forestStart_adds1.Add(new LayerImage("smallChest", 9, 1, 3, 30, 1, 1, new List<LayerScript> { new LayerScript("openChest", true, smallChest_parameters) }));
            //TODO new script useObject OR haveObject
            List<LayerImage> forestStart_walls = new List<LayerImage>
            {
                new LayerImage("entranceWestWall", 4, 0, 0, 100),
                new LayerImage("entranceEastWall", 6, 0, 0, 100, 2, 1)
            };
            Globals.characters[0].x = 1;
            Globals.characters[0].y = 9;
            Globals.characters[0].face = 0;
            Globals.ennemies[0].x = 1;
            Globals.ennemies[0].y = 1;
            Globals.ennemies[0].face = 2;
            Globals.npcs[0].x = 8;
            Globals.npcs[0].y = 7;
            Globals.npcs[0].face = 3;
            List<Entity> forestStart_entities = new List<Entity>
            {
                Globals.characters[0], Globals.ennemies[0], Globals.npcs[0]
            };
            Globals.scenes.Add(new Scene("forestStart", 10, 10, "exploration", -1, new List<LayerImage>(), forestStart_adds1,
                new List<LayerImage>(), forestStart_walls, new List<Case>(), new List<LayerScript>(), forestStart_entities));

            //TODO 2nd scene
            Scenes();
            
            //DIALOGUES
            
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
