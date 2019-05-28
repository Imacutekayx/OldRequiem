using Requiem.Class;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Requiem
{
    /// <summary>
    /// Class which manage the class to xml convertor
    /// </summary>
    public static class ClassToXML
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
                new Power("Summon Weapon", 3, 0, 1, 90, 1, charEffects, false, charOptions)
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
            charPowers.Add(new Power("Pyrokinesy", 5, 5, 1, 15, 20, charEffects, false, charOptions));
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
                "calm;sadistic;logical", "Soft-Cliff", 1, 1, 10, charDices, 0, new List<string>() { "common" }, new List<string>(), new List<string>(), new List<string>(), new List<string>(), charArmor, charArmorChange, "mage;magic", charWeapon, "magic1;magic2;dagger", charPowers, charBag));
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
                1, 1, ennDices, 0, ennPowers, "", ennBag, new List<string>() { "common" }, new List<string>(), new List<string>(), 
                new List<string>(), new List<string>() { "fire" }, ennArmor, ennArmorChange, "", ennWeapon, ennScripts));
            Ennemies();

            //NPCS
            Dictionary<Item, int> npcBag = new Dictionary<Item, int>
            {
                {Globals.useables[0], 30}
            };
            Npc npc = new Npc("Butcher", true, 41, "butcher", "hotblood", 1, 1, true, new List<string>() { "common" }, npcBag);
            Globals.npcs.Add(npc);
            Npcs();

            //SCENES
            LayerImage sceBackground = new LayerImage("grass", 20, 20, 0, 0);
            List<string> sceParameters = new List<string>
            {
                "weapon;sword",
                "useable;gold;5"
            };
            List<LayerImage> sceAdds1 = new List<LayerImage>
            {
                new LayerImage("bigChest", 1, 2, 1, 7, 1, 50, new List<LayerScript>{ new LayerScript("openChest", true, sceParameters)}),
                new LayerImage("bigRock", 1, 2, 3, 7, 1, 45)
            };
            List<LayerImage> sceWalls = new List<LayerImage>
            {
                new LayerImage("ennemyWestWall", 1, 2, 1, 2, 1),
                new LayerImage("ennemyEastWall", 1, 3, 3, 1, 3),
                new LayerImage("ennemySouthWall", 3, 1, 1, 4, 0),
                new LayerImage("npcNorthWall", 3, 1, 7, 0, 2),
                new LayerImage("npcEastWall", 1, 2, 9, 1, 3)
            };
            List<Case> sceCases = new List<Case>();
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
            Globals.scenes.Add(new Scene("forestStart", 10, 10, "exploration", -1, new List<LayerImage>(),
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
        /// Create the useables.xml
        /// </summary>
        private static void Useables()
        {
            //Create the XML
            using(XmlWriter writer = XmlWriter.Create("useables.xml"))
            {
                writer.WriteStartElement("useables");
                //Loop for each item
                foreach(Item useable in Globals.useables)
                {
                    writer.WriteStartElement("useable");
                    //Basic attributes
                    writer.WriteElementString("name", useable.name);
                    writer.WriteElementString("value", Convert.ToString(useable.value));
                    writer.WriteElementString("weight", Convert.ToString(useable.weight));
                    writer.WriteElementString("description", useable.description);
                    writer.WriteElementString("scope", Convert.ToString(useable.scope));
                    //List of effects
                    writer.WriteStartElement("effects");
                    if(useable.effects != null)
                    {
                        foreach (KeyValuePair<string, int> effect in useable.effects)
                        {
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("name", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the armors.xml
        /// </summary>
        private static void Armors()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("armors.xml"))
            {
                writer.WriteStartElement("armors");
                //Loop for each armor
                foreach(Armor armor in Globals.armors)
                {
                    writer.WriteStartElement("armor");
                    writer.WriteElementString("name", armor.name);
                    writer.WriteElementString("description", armor.description);
                    writer.WriteElementString("value", Convert.ToString(armor.value));
                    writer.WriteElementString("weight", Convert.ToString(armor.weight));
                    writer.WriteElementString("part", armor.part);
                    writer.WriteElementString("type", armor.type);
                    //List of effects
                    writer.WriteStartElement("effects");
                    if(armor.effects != null)
                    {
                        foreach (KeyValuePair<string, int> effect in armor.effects)
                        {
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("target", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the weapons.xml
        /// </summary>
        private static void Weapons()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("weapons.xml"))
            {
                writer.WriteStartElement("weapons");
                //Loop for each armor
                foreach (Weapon weapon in Globals.weapons)
                {
                    writer.WriteStartElement("weapon");
                    writer.WriteElementString("name", weapon.name);
                    writer.WriteElementString("description", weapon.description);
                    writer.WriteElementString("value", Convert.ToString(weapon.value));
                    writer.WriteElementString("weight", Convert.ToString(weapon.weight));
                    writer.WriteElementString("type", weapon.type);
                    //Combat attributes
                    writer.WriteStartElement("attributes");
                    writer.WriteElementString("damage", Convert.ToString(weapon.damage));
                    writer.WriteElementString("range", Convert.ToString(weapon.range));
                    //List of effects
                    writer.WriteStartElement("effects");
                    if(weapon.effects != null)
                    {
                        foreach (KeyValuePair<string, int> effect in weapon.effects)
                        {
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("target", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("typeAttack", weapon.typeAttack);
                    //List of powers
                    writer.WriteStartElement("powers");
                    for(int i = 1; i < weapon.powers.Count; ++i)
                    {
                        //Individual power
                        writer.WriteStartElement("power");
                        writer.WriteElementString("name", weapon.powers[i].name);
                        writer.WriteElementString("mp", Convert.ToString(weapon.powers[i].mana));
                        writer.WriteElementString("scope", Convert.ToString(weapon.powers[i].scope));
                        writer.WriteElementString("area", Convert.ToString(weapon.powers[i].area));
                        writer.WriteElementString("cast", Convert.ToString(weapon.powers[i].cast));
                        writer.WriteElementString("speed", Convert.ToString(weapon.powers[i].speed));
                        //List of effects
                        writer.WriteStartElement("effects");
                        foreach (KeyValuePair<string, int> effect in weapon.powers[i].effects)
                        {
                            //Individual effect
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("target", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteElementString("needBasic", Convert.ToString(weapon.powers[i].needBasic));
                        //List of options for this power
                        writer.WriteStartElement("options");
                        if (weapon.powers[i].options != null)
                        {
                            foreach (string option in weapon.powers[i].options)
                            {
                                //Individual option
                                writer.WriteElementString("option", option);
                            }
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the characters.xml
        /// </summary>
        private static void Characters()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("characters.xml"))
            {
                writer.WriteStartElement("characters");
                //Loop for each character
                foreach (Character character in Globals.characters)
                {
                    //Root of character
                    writer.WriteStartElement("character");
                    //Basic attributes
                    writer.WriteElementString("name", character.name);
                    writer.WriteElementString("sex", character.sex ? "male" : "female");
                    writer.WriteElementString("age", Convert.ToString(character.age));
                    //Description of the character
                    writer.WriteStartElement("description");
                    writer.WriteElementString("story", character.story);
                    writer.WriteElementString("class", character.cl);
                    writer.WriteElementString("race", character.race);
                    writer.WriteElementString("personality", character.personality);
                    writer.WriteElementString("origin", character.origin);
                    writer.WriteElementString("weight", Convert.ToString(character.weight));
                    writer.WriteElementString("height", Convert.ToString(character.height));
                    writer.WriteElementString("fov", Convert.ToString(character.fov));
                    writer.WriteEndElement();
                    //List of particularities
                    writer.WriteStartElement("particularities");
                    writer.WriteStartElement("languages");
                    foreach (string language in character.languages)
                    {
                        writer.WriteElementString("language", language);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("competences");
                    foreach (string competence in character.competences)
                    {
                        writer.WriteElementString("competence", competence);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("immunities");
                    foreach (string immunity in character.immunities)
                    {
                        writer.WriteElementString("language", immunity);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("resistances");
                    foreach (string resistance in character.resistances)
                    {
                        writer.WriteElementString("resistance", resistance);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("vulnerabilities");
                    foreach (string vulnerability in character.vulnerabilities)
                    {
                        writer.WriteElementString("vulnerabilty", vulnerability);
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    //Combat attribute
                    writer.WriteStartElement("attributes");
                    writer.WriteElementString("weapon", character.weapons[0] != null ? character.weapons[0].name : "");
                    writer.WriteElementString("weapon", character.weapons[1] != null ? character.weapons[1].name : "");
                    writer.WriteElementString("weapontype", character.weapontype);
                    //List of dices
                    writer.WriteStartElement("dices");
                    writer.WriteElementString("constitution", Convert.ToString(character.dices[0]));
                    writer.WriteElementString("knowledge", Convert.ToString(character.dices[1]));
                    writer.WriteElementString("focus", Convert.ToString(character.dices[2]));
                    writer.WriteElementString("charisma", Convert.ToString(character.dices[3]));
                    writer.WriteElementString("agility", Convert.ToString(character.dices[4]));
                    writer.WriteElementString("arcane", Convert.ToString(character.dices[5]));
                    writer.WriteElementString("temper", Convert.ToString(character.dices[6]));
                    writer.WriteEndElement();
                    writer.WriteElementString("armor", Convert.ToString(character.armor));
                    //List of armor of the character
                    writer.WriteElementString("typearmor", character.armortype);
                    writer.WriteStartElement("armors");
                    for(int i = 0; i < 6; ++i)
                    {
                        writer.WriteStartElement("armor");
                        writer.WriteElementString("name", character.armors[i] != null ? character.armors[i].name : "");
                        writer.WriteElementString("change", Convert.ToString(character.armorChange[i]));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of powers of the character
                    writer.WriteStartElement("powers");
                    foreach (Power power in character.powers)
                    {
                        //Individual power
                        writer.WriteStartElement("power");
                        writer.WriteElementString("name", power.name);
                        writer.WriteElementString("mp", Convert.ToString(power.mana));
                        writer.WriteElementString("scope", Convert.ToString(power.scope));
                        writer.WriteElementString("area", Convert.ToString(power.area));
                        writer.WriteElementString("cast", Convert.ToString(power.cast));
                        writer.WriteElementString("speed", Convert.ToString(power.speed));
                        //List of effects
                        writer.WriteStartElement("effects");
                        foreach (KeyValuePair<string, int> effect in power.effects)
                        {
                            //Individual effect
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("target", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteElementString("needBasic", Convert.ToString(power.needBasic));
                        //List of options for this power
                        writer.WriteStartElement("options");
                        if (power.options != null)
                        {
                            foreach (string option in power.options)
                            {
                                //Individual option
                                writer.WriteElementString("option", option);
                            }
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("bag");
                    //List of items in bag
                    foreach(KeyValuePair<Item, int> item in character.bag)
                    {
                        //Individual item
                        writer.WriteStartElement("item");
                        writer.WriteElementString("use", item.Key.use);
                        writer.WriteElementString("name", item.Key.name);
                        writer.WriteElementString("nbr", Convert.ToString(item.Value));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the ennemies.xml
        /// </summary>
        private static void Ennemies()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("ennemies.xml"))
            {
                writer.WriteStartElement("ennemies");
                //Loop for each character
                foreach (Ennemy ennemy in Globals.ennemies)
                {
                    //Root of character
                    writer.WriteStartElement("ennemy");
                    //Basic attributes
                    writer.WriteElementString("name", ennemy.name);
                    writer.WriteElementString("description", ennemy.description);
                    writer.WriteElementString("weight", Convert.ToString(ennemy.weight));
                    writer.WriteElementString("height", Convert.ToString(ennemy.height));
                    //Combat attribute
                    writer.WriteStartElement("attributes");
                    writer.WriteElementString("weapon", ennemy.weapons[0] != null ? ennemy.weapons[0].name : "");
                    writer.WriteElementString("weapon", ennemy.weapons[1] != null ? ennemy.weapons[1].name : "");
                    writer.WriteElementString("weapontype", ennemy.weapontype);
                    //List of dices
                    writer.WriteStartElement("dices");
                    writer.WriteElementString("constitution", Convert.ToString(ennemy.dices[0]));
                    writer.WriteElementString("knowledge", Convert.ToString(ennemy.dices[1]));
                    writer.WriteElementString("focus", Convert.ToString(ennemy.dices[2]));
                    writer.WriteElementString("charisma", Convert.ToString(ennemy.dices[3]));
                    writer.WriteElementString("agility", Convert.ToString(ennemy.dices[4]));
                    writer.WriteElementString("arcane", Convert.ToString(ennemy.dices[5]));
                    writer.WriteElementString("temper", Convert.ToString(ennemy.dices[6]));
                    writer.WriteEndElement();
                    //List of particularities
                    writer.WriteElementString("armor", Convert.ToString(ennemy.armor));
                    writer.WriteStartElement("particularities");
                    writer.WriteStartElement("languages");
                    foreach (string language in ennemy.languages)
                    {
                        writer.WriteElementString("language", language);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("competences");
                    foreach (string competence in ennemy.competences)
                    {
                        writer.WriteElementString("competence", competence);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("immunities");
                    foreach (string immunity in ennemy.immunities)
                    {
                        writer.WriteElementString("language", immunity);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("resistances");
                    foreach (string resistance in ennemy.resistances)
                    {
                        writer.WriteElementString("resistance", resistance);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("vulnerabilities");
                    foreach (string vulnerability in ennemy.vulnerabilities)
                    {
                        writer.WriteElementString("vulnerability", vulnerability);
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    //List of special scripts
                    writer.WriteStartElement("scripts");
                    foreach(KeyValuePair<string, int> script in ennemy.scripts)
                    {
                        writer.WriteStartElement("script");
                        writer.WriteElementString("name", script.Key);
                        writer.WriteElementString("time", Convert.ToString(script.Value));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of armor of the character
                    writer.WriteElementString("typearmor", ennemy.armortype);
                    writer.WriteStartElement("armors");
                    for(int i = 0; i < 6; ++i)
                    {
                        writer.WriteStartElement("armor");
                        writer.WriteElementString("name", ennemy.armors[i] != null ? ennemy.armors[i].name : "");
                        writer.WriteElementString("change", Convert.ToString(ennemy.armorChange[i]));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of powers of the character
                    writer.WriteStartElement("powers");
                    foreach (Power power in ennemy.powers)
                    {
                        //Individual power
                        writer.WriteStartElement("power");
                        writer.WriteElementString("name", power.name);
                        writer.WriteElementString("mp", Convert.ToString(power.mana));
                        writer.WriteElementString("scope", Convert.ToString(power.scope));
                        writer.WriteElementString("area", Convert.ToString(power.area));
                        writer.WriteElementString("cast", Convert.ToString(power.cast));
                        writer.WriteElementString("speed", Convert.ToString(power.speed));
                        //List of effects
                        writer.WriteStartElement("effects");
                        foreach (KeyValuePair<string, int> effect in power.effects)
                        {
                            //Individual effect
                            writer.WriteStartElement("effect");
                            writer.WriteElementString("target", effect.Key);
                            writer.WriteElementString("value", Convert.ToString(effect.Value));
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteElementString("needBasic", Convert.ToString(power.needBasic));
                        //List of options for this power
                        writer.WriteStartElement("options");
                        if (power.options != null)
                        {
                            foreach (string option in power.options)
                            {
                                //Individual option
                                writer.WriteElementString("option", option);
                            }
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("bag");
                    //List of items in bag
                    foreach (KeyValuePair<Item, int> item in ennemy.bag)
                    {
                        //Individual item
                        writer.WriteStartElement("item");
                        writer.WriteElementString("use", item.Key.use);
                        writer.WriteElementString("name", item.Key.name);
                        writer.WriteElementString("nbr", Convert.ToString(item.Value));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the npcs.xml
        /// </summary>
        private static void Npcs()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("npcs.xml"))
            {
                writer.WriteStartElement("npcs");
                //Loop for each npc
                foreach (Npc npc in Globals.npcs)
                {
                    writer.WriteStartElement("npc");
                    //Basic attributes
                    writer.WriteElementString("name", npc.name);
                    writer.WriteElementString("sex", Convert.ToString(npc.sex));
                    writer.WriteElementString("age", Convert.ToString(npc.age));
                    writer.WriteElementString("job", npc.job);
                    writer.WriteElementString("temper", npc.temper);
                    writer.WriteElementString("weight", Convert.ToString(npc.weight));
                    writer.WriteElementString("height", Convert.ToString(npc.height));
                    writer.WriteElementString("unique", Convert.ToString(npc.unique));
                    writer.WriteStartElement("languages");
                    foreach(string language in npc.languages)
                    {
                        writer.WriteElementString("language", language);
                    }
                    writer.WriteEndElement();
                    //List of items in the bag
                    writer.WriteStartElement("bag");
                    foreach (KeyValuePair<Item, int> item in npc.bag)
                    {
                        writer.WriteStartElement("item");
                        writer.WriteElementString("use", item.Key.use);
                        writer.WriteElementString("name", item.Key.name);
                        writer.WriteElementString("nbr", Convert.ToString(item.Value));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the scenes.xml
        /// </summary>
        private static void Scenes()
        {
            //Variables
            string[] adds = { "adds0", "adds1", "adds2", "walls" };

            //Objects
            List<List<LayerImage>> lstAdds;

            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("scenes.xml"))
            {
                writer.WriteStartElement("scenes");
                //Loop for each scene
                foreach (Scene scene in Globals.scenes)
                {
                    lstAdds = new List<List<LayerImage>>
                    {
                        scene.adds0, scene.adds1, scene.adds2, scene.walls
                    };
                    //Root of the scene
                    writer.WriteStartElement("scene");
                    writer.WriteElementString("name", scene.name);
                    //Size of the scene
                    writer.WriteElementString("size", scene.weight + ";" + scene.height);
                    //Basic gamemode
                    writer.WriteElementString("gamemode", scene.gamemode);
                    writer.WriteElementString("darkness", Convert.ToString(scene.darkness));
                    //List of the layers
                    writer.WriteStartElement("layers");
                    for(int i = 0; i < adds.Length; ++i)
                    {
                        writer.WriteStartElement(adds[i]);
                        foreach(LayerImage image in lstAdds[i])
                        {
                            writer.WriteStartElement("image");
                            writer.WriteElementString("name", image.name);
                            writer.WriteElementString("size", image.weight + ";" + image.height);
                            writer.WriteElementString("high", Convert.ToString(image.high));
                            writer.WriteElementString("coordinate", image.x + ";" + image.y);
                            writer.WriteElementString("face", Convert.ToString(image.face));
                            //List of all scripts
                            writer.WriteStartElement("scripts");
                            foreach (LayerScript script in scene.scripts)
                            {
                                //Individual script
                                writer.WriteStartElement("script");
                                writer.WriteElementString("name", script.name);
                                writer.WriteElementString("state", Convert.ToString(script.state));
                                if (script.typeTrigger != null)
                                {
                                    writer.WriteElementString("typeTrigger", script.typeTrigger);
                                    writer.WriteElementString("size", script.weight + ";" + script.height);
                                    writer.WriteElementString("coordinate", script.x + ";" + script.y);
                                    writer.WriteElementString("range", Convert.ToString(script.range));
                                }
                                //List of parameters of the script
                                writer.WriteStartElement("parameters");
                                foreach (string parameter in script.parameters)
                                {
                                    //Individual parameter
                                    writer.WriteElementString("parameter", parameter);
                                }
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of all cases
                    writer.WriteStartElement("cases");
                    foreach(Case c in scene.cases)
                    {
                        //Individual case
                        writer.WriteStartElement("case");
                        //Coordinates of the case
                        writer.WriteElementString("coordinate", c.x + ";" + c.y);
                        writer.WriteElementString("type", c.type);
                        writer.WriteElementString("state", c.state);
                        if(c.items != null)
                        {
                            writer.WriteStartElement("items");
                            foreach (KeyValuePair<Item, int> item in c.items)
                            {
                                writer.WriteStartElement("item");
                                writer.WriteElementString("use", item.Key.use);
                                writer.WriteElementString("name", item.Key.name);
                                writer.WriteElementString("nbr", Convert.ToString(item.Value));
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of all scripts
                    writer.WriteStartElement("scripts");
                    foreach(LayerScript script in scene.scripts)
                    {
                        //Individual script
                        writer.WriteStartElement("script");
                        writer.WriteElementString("name", script.name);
                        writer.WriteElementString("state", Convert.ToString(script.state));
                        if(script.typeTrigger != null)
                        {
                            writer.WriteElementString("typeTrigger", script.typeTrigger);
                            writer.WriteElementString("size", script.weight + ";" + script.height);
                            writer.WriteElementString("coordinate", script.x + ";" + script.y);
                            writer.WriteElementString("range", Convert.ToString(script.range));
                        }
                        //List of parameters of the script
                        writer.WriteStartElement("parameters");
                        foreach(string parameter in script.parameters)
                        {
                            //Individual parameter
                            writer.WriteElementString("parameter", parameter);
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    //List of entities
                    writer.WriteStartElement("entities");
                    foreach(Entity entity in scene.entities)
                    {
                        writer.WriteStartElement("entity");
                        writer.WriteElementString("type", entity.type);
                        writer.WriteElementString("name", entity.name);
                        writer.WriteElementString("coordinates", entity.x + ";" + entity.y);
                        writer.WriteElementString("face", Convert.ToString(entity.face));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        /// <summary>
        /// Create the dialogues.xml
        /// </summary>
        private static void Dialogues()
        {
            //Create the XML
            using (XmlWriter writer = XmlWriter.Create("dialogues.xml"))
            {
                writer.WriteStartElement("dialogues");
                //Loop for dialogues
                foreach(Dictionary<string, Dictionary<string, Dictionary<string, string>>> dialogue in Globals.dialogues)
                {
                    writer.WriteStartElement("dialogue");
                    writer.WriteStartElement("parts");
                    //Loop of parts
                    foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> part in dialogue)
                    {
                        writer.WriteStartElement("part");
                        writer.WriteElementString("npc", part.Key);
                        writer.WriteStartElement("characters");
                        //Loop of characters
                        foreach(KeyValuePair<string, Dictionary<string, string>> character in part.Value)
                        {
                            writer.WriteStartElement("character");
                            writer.WriteElementString("name", character.Key);
                            writer.WriteStartElement("answers");
                            //Loop of answers
                            foreach(KeyValuePair<string, string> answer in character.Value)
                            {
                                writer.WriteStartElement("answer");
                                writer.WriteElementString("name", answer.Key);
                                writer.WriteElementString("text", answer.Value);
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }
    }
}
