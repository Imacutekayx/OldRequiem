using Requiem.Class;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class which manage the xml to class convertor
    /// </summary>
    public static class REQToClass
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
        /// Method which load the useables.req
        /// </summary>
        private static void Useables()
        {
            Globals.useables.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("useables.req", FileMode.Open, FileAccess.Read);
            while(stream.Position != stream.Length)
            {
                Globals.useables.Add((Item)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the armors.req
        /// </summary>
        private static void Armors()
        {
            Globals.armors.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("armors.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.armors.Add((Armor)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the weapons.req
        /// </summary>
        private static void Weapons()
        {
            Globals.weapons.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("weapons.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.weapons.Add((Weapon)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the characters.req
        /// </summary>
        private static void Characters()
        {
            Globals.characters.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("characters.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.characters.Add((Character)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the ennemies.req
        /// </summary>
        private static void Ennemies()
        {
            Globals.ennemies.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("ennemies.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.ennemies.Add((Ennemy)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the npcs.req
        /// </summary>
        private static void Npcs()
        {
            Globals.npcs.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("npcs.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.npcs.Add((Npc)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Method which load the scenes.req
        /// </summary>
        private static void Scenes()
        {
            Globals.scenes.Clear();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("scenes.req", FileMode.Open, FileAccess.Read);
            while (stream.Position != stream.Length)
            {
                Globals.scenes.Add((Scene)formatter.Deserialize(stream));
            }
            stream.Close();
        }

        /// <summary>
        /// Methid which load the dialogues.xml
        /// </summary>
        private static void Dialogues()
        {
            //TODO Dialogues.req
        }
    }
}
