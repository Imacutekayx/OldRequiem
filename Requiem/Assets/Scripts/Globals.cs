using Requiem.Class;
using System.Collections.Generic;

namespace Requiem
{
    public static class Globals
    {
        //Objects
        public static CameraManager cameraManager;
        public static List<Item> useables = new List<Item>();
        public static List<Armor> armors = new List<Armor>();
        public static List<Weapon> weapons = new List<Weapon>();
        public static List<Character> characters = new List<Character>();
        public static List<Ennemy> ennemies = new List<Ennemy>();
        public static List<Npc> npcs = new List<Npc>();
        public static List<Scene> scenes = new List<Scene>();
        public static List<                                             //dialogues
                        Dictionary<                                     //key = npc name and speech
                            string, Dictionary<                         //npc line - characters
                                string, Dictionary<                     //character name - answers
                                    string, string>>>> dialogues        //answer type - answer text
            = new List<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
    }
}
