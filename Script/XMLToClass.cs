using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tester.Class;

namespace Tester
{
    /// <summary>
    /// Class which manage the xml to class convertor
    /// </summary>
    public static class XMLToClass
    {
        //Objects
        public static List<Item> useables = new List<Item>();
        public static List<Armor> armors = new List<Armor>();
        public static List<Weapon> weapons = new List<Weapon>();
        public static List<Character> characters = new List<Character>();
        public static List<Ennemy> ennemies = new List<Ennemy>();
        public static List<Npc> npcs = new List<Npc>();
        public static List<Scene> scenes = new List<Scene>();
        public static List<                                                 //dialogues
                        List<                                               //part of the dialogue
                            Dictionary<                                     //key = npc name and speech
                                string, Dictionary<                         //npc line - characters
                                    string, Dictionary<                     //character name - answers
                                        string, string>>>>> dialogues       //answer type - answer text
            = new List<List<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>();

        /// <summary>
        /// Class which manage the xml to class convertor
        /// </summary>
        public static void Load()
        {
            //USEABLES
            Useables();
            Console.WriteLine(useables[0].name);

            //ARMORS
            Armors();
            Console.WriteLine(armors[0].name);

            //WEAPONS
            Weapons();
            Console.WriteLine(weapons[0].name);

            //CHARACTERS
            Characters();
            Console.WriteLine(characters[0].name);

            //ENNEMIES
            Ennemies();
            Console.WriteLine(ennemies[0].name);

            //NPCS
            Npcs();
            Console.WriteLine(npcs[0].name);

            //SCENES

            //DIALOGUES

            Console.ReadLine();
        }

        /// <summary>
        /// Method which load the useables.xml
        /// </summary>
        private static void Useables()
        {
            //Basic attributes
            string name;
            int value;
            string description;
            //List of effects
            Dictionary<string, int> effects = new Dictionary<string, int>();
            string effName;
            int effValue;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("useables.xml");
            
            //Get informations
            XmlNodeList xmlUseables = xmldoc.GetElementsByTagName("useable");
            foreach (XmlNode useable in xmlUseables)
            {
                name = useable.SelectSingleNode("name").InnerText;
                value = Convert.ToInt32(useable.SelectSingleNode("value").InnerText);
                description = useable.SelectSingleNode("description").InnerText;
                XmlNodeList nodeEffects = useable.SelectNodes("effect");
                effects.Clear();
                foreach(XmlNode effect in nodeEffects)
                {
                    effName = effect.SelectSingleNode("name").InnerText;
                    effValue = Convert.ToInt32(effect.SelectSingleNode("value").InnerText);
                    effects.Add(effName, effValue);
                }
                useables.Add(new Item(name, value, description, effects));
            }
        }

        /// <summary>
        /// Method which load the armors.xml
        /// </summary>
        private static void Armors()
        {
            //Basic attributes
            string name;
            int value;
            string description;
            string part;
            string type;
            //List of effects
            Dictionary<string, int> effects = new Dictionary<string, int>();
            string effTarget;
            int effValue;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("armors.xml");

            //Get informations
            XmlNodeList xmlArmors = xmldoc.GetElementsByTagName("armor");
            foreach (XmlNode armor in xmlArmors)
            {
                name = armor.SelectSingleNode("name").InnerText;
                value = Convert.ToInt32(armor.SelectSingleNode("value").InnerText);
                description = armor.SelectSingleNode("description").InnerText;
                part = armor.SelectSingleNode("part").InnerText;
                type = armor.SelectSingleNode("type").InnerText;
                XmlNodeList nodeEffects = armor.SelectNodes("effect");
                effects.Clear();
                foreach (XmlNode effect in nodeEffects)
                {
                    effTarget = effect.SelectSingleNode("target").InnerText;
                    effValue = Convert.ToInt32(effect.SelectSingleNode("value").InnerText);
                    effects.Add(effTarget, effValue);
                }
                armors.Add(new Armor(name, value, description, part, type, effects));
            }
        }

        /// <summary>
        /// Method which load the weapons.xml
        /// </summary>
        private static void Weapons()
        {
            //Basic attributes
            string name;
            int value;
            string description;
            string type;
            //Combat attributes
            int damage;
            int range;
            //List of effects
            Dictionary<string, int> effects = new Dictionary<string, int>();
            string effTarget;
            int effValue;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("weapons.xml");

            //Get informations
            XmlNodeList xmlWeapons = xmldoc.GetElementsByTagName("weapon");
            foreach (XmlNode weapon in xmlWeapons)
            {
                name = weapon.SelectSingleNode("name").InnerText;
                value = Convert.ToInt32(weapon.SelectSingleNode("value").InnerText);
                description = weapon.SelectSingleNode("description").InnerText;
                type = weapon.SelectSingleNode("type").InnerText;
                XmlNode attributes = weapon.SelectSingleNode("attributes");
                damage = Convert.ToInt32(attributes.SelectSingleNode("damage").InnerText);
                range = Convert.ToInt32(attributes.SelectSingleNode("range").InnerText);
                XmlNodeList nodeEffects = attributes.SelectNodes("effect");
                effects.Clear();
                foreach (XmlNode effect in nodeEffects)
                {
                    effTarget = effect.SelectSingleNode("target").InnerText;
                    effValue = Convert.ToInt32(effect.SelectSingleNode("value").InnerText);
                    effects.Add(effTarget, effValue);
                }
                weapons.Add(new Weapon(name, value, description, type, damage, range, effects));
            }
        }

        /// <summary>
        /// Method which load the characters.xml
        /// </summary>
        private static void Characters()
        {
            //Basic attributes
            string name;
            bool sex;
            int age;
            //Description of the character
            string story;
            string cl;
            string race;
            string personality;
            string origin;
            //Combat attributes
            Weapon[] charWeapons = new Weapon[2];
            string weapontype;
            int[] dices = new int[6];
            string typearmor;
            Armor[] charArmors = new Armor[6];
            bool[] armorChanges = new bool[6];
            //Powers
            List<Power> powers = new List<Power>();
            string powName;
            int powMp;
            int powScope;
            int powArea;
            int powCast;
            int powSpeed;
            Dictionary<string, int> powEffects = new Dictionary<string, int>();
            string effTarget;
            int effValue;
            List<string> powOptions = new List<string>();
            //Items
            Dictionary<Item, int> bag = new Dictionary<Item, int>();
            string iteUse;
            string iteName;
            int iteNbr;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("characters.xml");

            //Get informations
            XmlNodeList xmlCharacters = xmldoc.GetElementsByTagName("character");
            foreach (XmlNode character in xmlCharacters)
            {
                //Basic attributes
                name = character.SelectSingleNode("name").InnerText;
                sex = character.SelectSingleNode("sex").InnerText == "male" ? true : false;
                age = Convert.ToInt32(character.SelectSingleNode("age").InnerText);
                //Description of the character
                XmlNode nodeDescription = character.SelectSingleNode("description");
                story = nodeDescription.SelectSingleNode("story").InnerText;
                cl = nodeDescription.SelectSingleNode("class").InnerText;
                race = nodeDescription.SelectSingleNode("race").InnerText;
                personality = nodeDescription.SelectSingleNode("personality").InnerText;
                origin = nodeDescription.SelectSingleNode("origin").InnerText;
                //Combat attributes
                XmlNode nodeAttributes = character.SelectSingleNode("attributes");
                XmlNodeList weaponNames = nodeAttributes.SelectNodes("weapon");
                if (weaponNames[0].InnerText != "" && weaponNames[1].InnerText != "")
                {
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.name == weaponNames[0].InnerText)
                        {
                            charWeapons[0] = weapon;
                            if (weaponNames[1].InnerText == "")
                            {
                                break;
                            }
                        }
                        if (weapon.name == weaponNames[1].InnerText)
                        {
                            charWeapons[1] = weapon;
                            if (weaponNames[0].InnerText == "")
                            {
                                break;
                            }
                        }
                    }
                }
                weapontype = nodeAttributes.SelectSingleNode("weapontype").InnerText;
                //List of dices
                XmlNode nodeDices = nodeAttributes.SelectSingleNode("dices");
                dices[0] = Convert.ToInt32(nodeDices.SelectSingleNode("constitution").InnerText);
                dices[1] = Convert.ToInt32(nodeDices.SelectSingleNode("knowledge").InnerText);
                dices[2] = Convert.ToInt32(nodeDices.SelectSingleNode("focus").InnerText);
                dices[3] = Convert.ToInt32(nodeDices.SelectSingleNode("charisma").InnerText);
                dices[4] = Convert.ToInt32(nodeDices.SelectSingleNode("arcane").InnerText);
                dices[5] = Convert.ToInt32(nodeDices.SelectSingleNode("temper").InnerText);
                //List of armors
                typearmor = nodeAttributes.SelectSingleNode("typearmor").InnerText;
                XmlNode nodeArmors = nodeAttributes.SelectSingleNode("armors");
                XmlNodeList xmlArmors = nodeArmors.SelectNodes("armor");
                for (int i = 0; i < 6; ++i)
                {
                    if (xmlArmors[i].SelectSingleNode("name").InnerText != "")
                    {
                        foreach (Armor armor in armors)
                        {
                            if (armor.name == xmlArmors[i].SelectSingleNode("name").InnerText)
                            {
                                charArmors[i] = armor;
                                break;
                            }
                        }
                    }
                    armorChanges[i] = xmlArmors[i].SelectSingleNode("change").InnerText == "true";
                }
                //List of powers
                XmlNode nodePowers = nodeAttributes.SelectSingleNode("powers");
                XmlNodeList xmlPowers = nodePowers.SelectNodes("power");
                powers.Clear();
                foreach (XmlNode power in xmlPowers)
                {
                    //Basic attributes of the power
                    powName = power.SelectSingleNode("name").InnerText;
                    powMp = Convert.ToInt32(power.SelectSingleNode("mp").InnerText);
                    powScope = Convert.ToInt32(power.SelectSingleNode("scope").InnerText);
                    powArea = Convert.ToInt32(power.SelectSingleNode("area").InnerText);
                    powCast = Convert.ToInt32(power.SelectSingleNode("cast").InnerText);
                    powSpeed = Convert.ToInt32(power.SelectSingleNode("speed").InnerText);
                    //List of effects of the power
                    XmlNode nodeEffects = power.SelectSingleNode("effects");
                    XmlNodeList xmlEffects = nodeEffects.SelectNodes("effect");
                    powEffects.Clear();
                    foreach (XmlNode effect in xmlEffects)
                    {
                        effTarget = effect.SelectSingleNode("target").InnerText;
                        effValue = Convert.ToInt32(effect.SelectSingleNode("value").InnerText);
                        powEffects.Add(effTarget, effValue);
                    }
                    //List of options of the power
                    XmlNode nodeOptions = power.SelectSingleNode("options");
                    XmlNodeList xmlOptions = nodeOptions.SelectNodes("option");
                    powOptions.Clear();
                    foreach (XmlNode option in xmlOptions)
                    {
                        powOptions.Add(option.InnerText);
                    }
                    powers.Add(new Power(powName, powMp, powScope, powArea, powCast, powSpeed,
                        powEffects, powOptions.Count() != 0 ? powOptions : null));
                }
                //List of items
                XmlNode nodeBag = character.SelectSingleNode("bag");
                XmlNodeList xmlItems = nodeBag.SelectNodes("item");
                bag.Clear();
                foreach (XmlNode item in xmlItems)
                {
                    iteUse = item.SelectSingleNode("use").InnerText;
                    iteName = item.SelectSingleNode("name").InnerText;
                    iteNbr = Convert.ToInt32(item.SelectSingleNode("nbr").InnerText);

                    switch (iteUse)
                    {
                        case "useable":
                            foreach (Item useable in useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in armors)
                            {
                                if (iteName == armor.name)
                                {
                                    bag.Add(armor, 1);
                                    break;
                                }
                            }
                            break;
                    }
                }
                characters.Add(new Character(name, sex, age, story, cl, race, personality, origin,
                    dices, charArmors, armorChanges, typearmor, charWeapons, weapontype, powers, bag));
            }
        }

        /// <summary>
        /// Method which load the ennemies.xml
        /// </summary>
        private static void Ennemies()
        {
            //Basic attributes
            string name;
            string description;
            //Combat attributes
            Weapon[] ennWeapons = new Weapon[2];
            string weapontype;
            int[] dices = new int[6];
            //TODO SCRIPTS
            string typearmor;
            Armor[] ennArmors = new Armor[6];
            bool[] armorChanges = new bool[6];
            //Powers
            List<Power> powers = new List<Power>();
            string powName;
            int powMp;
            int powScope;
            int powArea;
            int powCast;
            int powSpeed;
            Dictionary<string, int> powEffects = new Dictionary<string, int>();
            string effTarget;
            int effValue;
            List<string> powOptions = new List<string>();
            //Items
            Dictionary<Item, int> bag = new Dictionary<Item, int>();
            string iteUse;
            string iteName;
            int iteNbr;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("ennemies.xml");

            //Get informations
            XmlNodeList xmlEnnemies = xmldoc.GetElementsByTagName("ennemy");
            foreach (XmlNode ennemy in xmlEnnemies)
            {
                //Basic attributes
                name = ennemy.SelectSingleNode("name").InnerText;
                description = ennemy.SelectSingleNode("description").InnerText;
                //Combat attributes
                XmlNode nodeAttributes = ennemy.SelectSingleNode("attributes");
                XmlNodeList weaponNames = nodeAttributes.SelectNodes("weapon");
                if (weaponNames[0].InnerText != "" && weaponNames[1].InnerText != "")
                {
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.name == weaponNames[0].InnerText)
                        {
                            ennWeapons[0] = weapon;
                            if (weaponNames[1].InnerText == "")
                            {
                                break;
                            }
                        }
                        if (weapon.name == weaponNames[1].InnerText)
                        {
                            ennWeapons[1] = weapon;
                            if (weaponNames[0].InnerText == "")
                            {
                                break;
                            }
                        }
                    }
                }
                weapontype = nodeAttributes.SelectSingleNode("weapontype").InnerText;
                //List of dices
                XmlNode nodeDices = nodeAttributes.SelectSingleNode("dices");
                dices[0] = Convert.ToInt32(nodeDices.SelectSingleNode("constitution").InnerText);
                dices[1] = Convert.ToInt32(nodeDices.SelectSingleNode("knowledge").InnerText);
                dices[2] = Convert.ToInt32(nodeDices.SelectSingleNode("focus").InnerText);
                dices[3] = Convert.ToInt32(nodeDices.SelectSingleNode("charisma").InnerText);
                dices[4] = Convert.ToInt32(nodeDices.SelectSingleNode("arcane").InnerText);
                dices[5] = Convert.ToInt32(nodeDices.SelectSingleNode("temper").InnerText);
                //List of armors
                typearmor = nodeAttributes.SelectSingleNode("typearmor").InnerText;
                XmlNode nodeArmors = nodeAttributes.SelectSingleNode("armors");
                XmlNodeList xmlArmors = nodeArmors.SelectNodes("armor");
                for (int i = 0; i < 6; ++i)
                {
                    if (xmlArmors[i].SelectSingleNode("name").InnerText != "")
                    {
                        foreach (Armor armor in armors)
                        {
                            if (armor.name == xmlArmors[i].SelectSingleNode("name").InnerText)
                            {
                                ennArmors[i] = armor;
                                break;
                            }
                        }
                    }
                    armorChanges[i] = xmlArmors[i].SelectSingleNode("change").InnerText == "true";
                }
                //List of powers
                XmlNode nodePowers = nodeAttributes.SelectSingleNode("powers");
                XmlNodeList xmlPowers = nodePowers.SelectNodes("power");
                powers.Clear();
                foreach (XmlNode power in xmlPowers)
                {
                    //Basic attributes of the power
                    powName = power.SelectSingleNode("name").InnerText;
                    powMp = Convert.ToInt32(power.SelectSingleNode("mp").InnerText);
                    powScope = Convert.ToInt32(power.SelectSingleNode("scope").InnerText);
                    powArea = Convert.ToInt32(power.SelectSingleNode("area").InnerText);
                    powCast = Convert.ToInt32(power.SelectSingleNode("cast").InnerText);
                    powSpeed = Convert.ToInt32(power.SelectSingleNode("speed").InnerText);
                    //List of effects of the power
                    XmlNode nodeEffects = power.SelectSingleNode("effects");
                    XmlNodeList xmlEffects = nodeEffects.SelectNodes("effect");
                    powEffects.Clear();
                    foreach (XmlNode effect in xmlEffects)
                    {
                        effTarget = effect.SelectSingleNode("target").InnerText;
                        effValue = Convert.ToInt32(effect.SelectSingleNode("value").InnerText);
                        powEffects.Add(effTarget, effValue);
                    }
                }
                //List of items
                XmlNode nodeBag = ennemy.SelectSingleNode("bag");
                XmlNodeList xmlItems = nodeBag.SelectNodes("item");
                bag.Clear();
                foreach (XmlNode item in xmlItems)
                {
                    iteUse = item.SelectSingleNode("use").InnerText;
                    iteName = item.SelectSingleNode("name").InnerText;
                    iteNbr = Convert.ToInt32(item.SelectSingleNode("nbr").InnerText);

                    switch (iteUse)
                    {
                        case "useable":
                            foreach (Item useable in useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in armors)
                            {
                                if (iteName == armor.name)
                                {
                                    bag.Add(armor, 1);
                                    break;
                                }
                            }
                            break;
                    }
                }
                //Add Scripts
                ennemies.Add(new Ennemy(name, description, dices, powers, weapontype, bag, ennArmors, armorChanges, typearmor, ennWeapons));
            }
        }

        /// <summary>
        /// Method which load the npcs.xml
        /// </summary>
        private static void Npcs()
        {
            //Basic attributes
            string name;
            bool sex;
            int age;
            string job;
            string temper;
            //Items
            Dictionary<Item, int> bag = new Dictionary<Item, int>();
            string iteUse;
            string iteName;
            int iteNbr;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("npcs.xml");

            //Get informations
            XmlNodeList xmlNpcs = xmldoc.GetElementsByTagName("npc");
            foreach (XmlNode npc in xmlNpcs)
            {
                //Basic attributes
                name = npc.SelectSingleNode("name").InnerText;
                sex = npc.SelectSingleNode("sex").InnerText == "male" ? true : false;
                age = Convert.ToInt32(npc.SelectSingleNode("age").InnerText);
                job = npc.SelectSingleNode("job").InnerText;
                temper = npc.SelectSingleNode("temper").InnerText;
                //List of items
                XmlNode nodeBag = npc.SelectSingleNode("bag");
                XmlNodeList xmlItems = nodeBag.SelectNodes("item");
                bag.Clear();
                foreach (XmlNode item in xmlItems)
                {
                    iteUse = item.SelectSingleNode("use").InnerText;
                    iteName = item.SelectSingleNode("name").InnerText;
                    iteNbr = Convert.ToInt32(item.SelectSingleNode("nbr").InnerText);

                    switch (iteUse)
                    {
                        case "useable":
                            foreach (Item useable in useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in armors)
                            {
                                if (iteName == armor.name)
                                {
                                    bag.Add(armor, 1);
                                    break;
                                }
                            }
                            break;
                    }
                }
                npcs.Add(new Npc(name, sex, age, job, temper, bag));
            }
        }
    }
}
