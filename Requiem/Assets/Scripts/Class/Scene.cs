using System.Collections.Generic;

namespace Requiem.Class
{
    /// <summary>
    /// Class which represents a scene
    /// </summary>
    public class Scene
    {
        //Variables
        public string name;
        public int weight;
        public int height;
        public string gamemode;

        //Objects
        public List<LayerImage> adds0;
        public List<LayerImage> adds1;
        public List<LayerImage> adds2;
        public List<LayerImage> walls;
        public Case[,] cases;
        public List<LayerScript> scripts;
        public List<Entity> entities;

        //Constructor
        public Scene(string _name, int _weight, int _height, string _gamemode, List<LayerImage> _adds0,
            List<LayerImage> _adds1, List<LayerImage> _adds2, List<LayerImage> _walls, List<Case> _cases, List<LayerScript> _scripts, List<Entity> _entities)
        {
            name = _name;
            weight = _weight;
            height = _height;
            gamemode = _gamemode;
            adds0 = _adds0;
            adds1 = _adds1;
            adds2 = _adds2;
            walls = _walls;
            cases = new Case[weight, height];
            for(int i = 0; i < weight; ++i)
            {
                for(int j = 0; j < height; ++j)
                {
                    cases[i, j] = new Case(i, j);
                }
            }
            foreach(Case c in _cases)
            {
                cases[c.x, c.y] = c;
            }
            foreach(LayerImage add1 in adds1)
            {
                for(int i = add1.x; i < add1.x + add1.weight; ++i)
                {
                    for (int j = add1.y; j < add1.y + add1.height; ++j)
                    {
                        cases[i, j].type = "add1";
                        cases[i, j].high = add1.high;
                    }
                }
            }
            foreach(LayerImage wall in walls)
            {
                for (int i = wall.x; i < wall.x + wall.weight; ++i)
                {
                    for (int j = wall.y; j < wall.y + wall.height; ++j)
                    {
                        cases[i, j].type = "wall";
                    }
                }
            }
            scripts = _scripts;
            entities = _entities;
            foreach(Entity entity in entities)
            {
                for(int i = entity.x; i < entity.x + entity.weight; ++i)
                {
                    for(int j = entity.y; j < entity.y + entity.height; ++j)
                    {
                        cases[i, j].type = entity.type;
                    }
                }
            }
        }
    }
}