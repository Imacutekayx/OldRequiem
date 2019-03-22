using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Requiem.Class;

namespace Requiem
{
    /// <summary>
    /// Class which manage the xml to class convertor
    /// </summary>
    public static class XMLToClass
    {
        /// <summary>
        /// Class which manage the xml to class convertor
        /// </summary>
        public static void Load()
        {
            //USEABLES
            Useables();

            //ARMORS
            Armors();

            //WEAPONS
            Weapons();

            //CHARACTERS
            Characters();

            //ENNEMIES
            Ennemies();

            //NPCS
            Npcs();

            //SCENES
            Scenes();

            //DIALOGUES
            Dialogues();
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
                Globals.useables.Add(new Item(name, value, description, effects));
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
                Globals.armors.Add(new Armor(name, value, description, part, type, effects));
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
                Globals.weapons.Add(new Weapon(name, value, description, type, damage, range, effects));
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
            int[] dices = new int[7];
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
                    foreach (Weapon weapon in Globals.weapons)
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
                dices[4] = Convert.ToInt32(nodeDices.SelectSingleNode("agility").InnerText);
                dices[5] = Convert.ToInt32(nodeDices.SelectSingleNode("arcane").InnerText);
                dices[6] = Convert.ToInt32(nodeDices.SelectSingleNode("temper").InnerText);
                //List of armors
                typearmor = nodeAttributes.SelectSingleNode("typearmor").InnerText;
                XmlNode nodeArmors = nodeAttributes.SelectSingleNode("armors");
                XmlNodeList xmlArmors = nodeArmors.SelectNodes("armor");
                for (int i = 0; i < 6; ++i)
                {
                    if (xmlArmors[i].SelectSingleNode("name").InnerText != "")
                    {
                        foreach (Armor armor in Globals.armors)
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
                            foreach (Item useable in Globals.useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in Globals.weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in Globals.armors)
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
                Globals.characters.Add(new Character(name, sex, age, story, cl, race, personality, origin,
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
            int[] dices = new int[7];
            //TODO Add scripts
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
                    foreach (Weapon weapon in Globals.weapons)
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
                dices[4] = Convert.ToInt32(nodeDices.SelectSingleNode("agility").InnerText);
                dices[5] = Convert.ToInt32(nodeDices.SelectSingleNode("arcane").InnerText);
                dices[6] = Convert.ToInt32(nodeDices.SelectSingleNode("temper").InnerText);
                //List of armors
                typearmor = nodeAttributes.SelectSingleNode("typearmor").InnerText;
                XmlNode nodeArmors = nodeAttributes.SelectSingleNode("armors");
                XmlNodeList xmlArmors = nodeArmors.SelectNodes("armor");
                for (int i = 0; i < 6; ++i)
                {
                    if (xmlArmors[i].SelectSingleNode("name").InnerText != "")
                    {
                        foreach (Armor armor in Globals.armors)
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
                            foreach (Item useable in Globals.useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in Globals.weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in Globals.armors)
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
                Globals.ennemies.Add(new Ennemy(name, description, dices, powers, weapontype, bag, ennArmors, armorChanges, typearmor, ennWeapons));
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
                            foreach (Item useable in Globals.useables)
                            {
                                if (iteName == useable.name)
                                {
                                    bag.Add(useable, iteNbr);
                                    break;
                                }
                            }
                            break;

                        case "weapon":
                            foreach (Weapon weapon in Globals.weapons)
                            {
                                if (iteName == weapon.name)
                                {
                                    bag.Add(weapon, 1);
                                    break;
                                }
                            }
                            break;

                        case "armor":
                            foreach (Armor armor in Globals.armors)
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
                Globals.npcs.Add(new Npc(name, sex, age, job, temper, bag));
            }
        }

        /// <summary>
        /// Method which load the scenes.xml
        /// </summary>
        private static void Scenes()
        {
            //Basic attributes
            string name;
            int weight;
            int height;
            string gamemode;
            //Images
            string[] layers = { "adds0", "adds1", "adds2" };
            List<List<LayerImage>> lstLayers = new List<List<LayerImage>>();
            for(int i = 0; i < 3; ++i)
            {
                lstLayers.Add(new List<LayerImage>());
            }
            string layImName;
            int layImHeight;
            int layImWeight;
            int layImX;
            int layImY;
            byte layImFace;
            //Cases
            List<Case> cases = new List<Case>();
            int casX;
            int casY;
            string casType;
            string casState;
            string casScript;
            //Scripts
            List<LayerScript> scripts = new List<LayerScript>();
            string scrName;
            bool scrState;
            int scrWeight;
            int scrHeight;
            int scrX;
            int scrY;
            int scrRange;
            List<string> scrParameters = new List<string>();
            //Entities
            List<Entity> entities = new List<Entity>();
            string entType;
            string entName;
            int entX;
            int entY;
            byte entFace;

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("scenes.xml");

            //Get informations
            XmlNodeList xmlScenes = xmldoc.GetElementsByTagName("scene");
            foreach(XmlNode scene in xmlScenes)
            {
                //Size of the scene
                name = scene.SelectSingleNode("name").InnerText;
                string[] size = scene.SelectSingleNode("size").InnerText.Split(';');
                weight = Convert.ToInt32(size[0]);
                height = Convert.ToInt32(size[1]);
                //Basic gamemode
                gamemode = scene.SelectSingleNode("gamemode").InnerText;
                //List of layers
                XmlNode xmlLayers = scene.SelectSingleNode("layers");
                for (int i = 0; i < layers.Length; ++i)
                {
                    XmlNode xmlLayer = xmlLayers.SelectSingleNode(layers[i]);
                    lstLayers[i].Clear();
                    XmlNodeList images = xmlLayer.SelectNodes("image");
                    //List of images
                    foreach (XmlNode image in images)
                    {
                        layImName = image.SelectSingleNode("name").InnerText;
                        string[] imTemp = image.SelectSingleNode("size").InnerText.Split(';');
                        layImWeight = Convert.ToInt32(imTemp[0]);
                        layImHeight = Convert.ToInt32(imTemp[1]);
                        imTemp = image.SelectSingleNode("coordinate").InnerText.Split(';');
                        layImX = Convert.ToInt32(imTemp[0]);
                        layImY = Convert.ToInt32(imTemp[1]);
                        layImFace = Convert.ToByte(image.SelectSingleNode("face").InnerText);
                        lstLayers[i].Add(new LayerImage(layImName, layImWeight, layImHeight, layImX, layImY, layImFace));
                    }
                }
                //List of cases
                XmlNode xmlCases = scene.SelectSingleNode("cases");
                XmlNodeList xmlCase = xmlCases.SelectNodes("case");
                cases.Clear();
                foreach(XmlNode c in xmlCase)
                {
                    string[] cCoor = c.SelectSingleNode("coordinate").InnerText.Split(';');
                    casX = Convert.ToInt32(cCoor[0]);
                    casY = Convert.ToInt32(cCoor[1]);
                    casType = c.SelectSingleNode("obstacle").InnerText;
                    casState = c.SelectSingleNode("state").InnerText;
                    casScript = c.SelectSingleNode("script").InnerText;
                    cases.Add(new Case(casX, casY, casType, casState, casScript));
                }
                //List of scripts
                XmlNode xmlScripts = scene.SelectSingleNode("scripts");
                XmlNodeList xmlScript = xmlScripts.SelectNodes("script");
                scripts.Clear();
                foreach(XmlNode script in xmlScript)
                {
                    //Attributes
                    scrName = script.SelectSingleNode("name").InnerText;
                    scrState = script.SelectSingleNode("state").InnerText == "true" ? true : false;
                    string[] scrTemp = script.SelectSingleNode("size").InnerText.Split(';');
                    scrWeight = Convert.ToInt32(scrTemp[0]);
                    scrHeight = Convert.ToInt32(scrTemp[1]);
                    scrTemp = script.SelectSingleNode("coordinate").InnerText.Split(';');
                    scrX = Convert.ToInt32(scrTemp[0]);
                    scrY = Convert.ToInt32(scrTemp[1]);
                    scrRange = Convert.ToInt32(script.SelectSingleNode("range").InnerText);
                    //List of parameters
                    XmlNode xmlParameters = script.SelectSingleNode("parameters");
                    XmlNodeList xmlParameter = xmlParameters.SelectNodes("parameter");
                    scrParameters.Clear();
                    foreach(XmlNode parameter in xmlParameter)
                    {
                        scrParameters.Add(parameter.InnerText);
                    }
                    scripts.Add(new LayerScript(scrName, scrState, scrWeight, scrHeight, scrX, scrY, scrRange, scrParameters));
                }
                //List of entities
                XmlNode xmlEntities = scene.SelectSingleNode("entities");
                XmlNodeList xmlEntity = xmlEntities.SelectNodes("entity");
                entities.Clear();
                foreach(XmlNode entity in xmlEntity)
                {
                    //Get informations
                    entType = entity.SelectSingleNode("type").InnerText;
                    entName = entity.SelectSingleNode("name").InnerText;
                    string[] entCoor = entity.SelectSingleNode("coordinates").InnerText.Split(';');
                    entX = Convert.ToInt32(entCoor[0]);
                    entY = Convert.ToInt32(entCoor[1]);
                    entFace = Convert.ToByte(entity.SelectSingleNode("face").InnerText);
                    //Get entity
                    switch (entType)
                    {
                        case "character":
                            foreach(Character character in Globals.characters)
                            {
                                if(character.name == entName)
                                {
                                    entities.Add(character);
                                }
                            }
                            break;

                        case "ennemy":
                            foreach(Ennemy ennemy in Globals.ennemies)
                            {
                                if(ennemy.name == entName)
                                {
                                    Ennemy ennCopy = new Ennemy(ennemy.name, ennemy.description, ennemy.dices, ennemy.powers, ennemy.weapontype,
                                        ennemy.bag, ennemy.armor, ennemy.armorChange, ennemy.armortype, ennemy.weapon, ennemy.scripts)
                                    {
                                        x = entX,
                                        y = entY,
                                        face = entFace
                                    };
                                    entities.Add(ennCopy);
                                    break;
                                }
                            }
                            break;

                        case "npc":
                            foreach(Npc npc in Globals.npcs)
                            {
                                if(npc.name == entName)
                                {
                                    Npc npcCopy = new Npc(npc.name, npc.sex, npc.age, npc.job, npc.temper, npc.bag)
                                    {
                                        x = entX,
                                        y = entY,
                                        face = entFace
                                    };
                                    entities.Add(npcCopy);
                                    break;
                                }
                            }
                            break;
                    }
                }
                Globals.scenes.Add(new Scene(name, weight, height, gamemode, lstLayers[0], lstLayers[1], lstLayers[2], cases, scripts, entities));
            }
        }

        /// <summary>
        /// Methid which load the dialogues.xml
        /// </summary>
        private static void Dialogues()
        {
            //Objects
            Dictionary<string, string> answers = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> DiaCharacters = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> parts = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //Open XML
            var xmldoc = new XmlDocument();
            xmldoc.Load("dialogues.xml");

            //Dialogues
            XmlNodeList xmlDialogues = xmldoc.GetElementsByTagName("dialogue");
            foreach(XmlNode _dialogue in xmlDialogues)
            {
                //Parts
                XmlNode xmlParts = _dialogue.SelectSingleNode("parts");
                XmlNodeList xmlPart = xmlParts.SelectNodes("part");
                parts.Clear();
                foreach(XmlNode part in xmlPart)
                {
                    //Characters
                    XmlNode xmlCharacters = part.SelectSingleNode("characters");
                    XmlNodeList xmlCharacter = xmlCharacters.SelectNodes("character");
                    DiaCharacters.Clear();
                    foreach(XmlNode character in xmlCharacter)
                    {
                        //Answers
                        XmlNode xmlAnswers = character.SelectSingleNode("answers");
                        XmlNodeList xmlAnswer = xmlAnswers.SelectNodes("answer");
                        answers.Clear();
                        foreach(XmlNode answer in xmlAnswer)
                        {
                            answers.Add(answer.SelectSingleNode("name").InnerText, answer.SelectSingleNode("text").InnerText);
                        }
                        DiaCharacters.Add(character.SelectSingleNode("name").InnerText, answers);
                    }
                    parts.Add(part.SelectSingleNode("npc").InnerText, DiaCharacters);
                }
                Globals.dialogues.Add(parts);
            }
        }
    }
}
