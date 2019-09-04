using Requiem.Class;
using Requiem.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Class which manage all related to the visual part of the game
    /// </summary>
    public class CameraManager
    {
        //Variables
        private byte face;  //0=S/1=W/2=N/3=E
        private bool top = false;

        //Const
        private const int CASESIZE = 4;
        private const int ZOOMSPEED = 5;
        private const int MAXFOV = 60;
        private const int MINFOV = 15;

        //Objects
        public GameObject[,] grid;
        private GameObject gridObjects;
        private GameObject add0Objects;
        private GameObject add1Objects;
        private GameObject add2Objects;
        private GameObject wallObjects;
        private GameObject characterObjects;
        private GameObject ennemyObjects;
        private GameObject npcObjects;
        private Camera camera;
        private List<List<GameObject>> lstWalls = new List<List<GameObject>>()
        {
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>()
        };

        //Constructor
        public CameraManager(Camera _camera)
        {
            camera = _camera;
            camera.transform.eulerAngles = new Vector3(30, 0, 0);
        }

        /// <summary>
        /// Change the current scene
        /// </summary>
        /// <param name="_scene">Scene to see</param>
        public void LoadNewScene(Scene _scene)
        {
            //Variables
            string[] faces = { "0", "1", "2", "3", "top" };
            float[] high = { -0.01f, 1f, 3.5f };
            
            Globals.currentScene = _scene;
            for(int i = 0; i < lstWalls.Count; ++i)
            {
                lstWalls[i].Clear();
            }

            //Objects
            List<List<LayerImage>> layers = new List<List<LayerImage>>
            {
                Globals.currentScene.adds0,
                Globals.currentScene.adds1,
                Globals.currentScene.adds2
            };

            //Grid
            gridObjects = new GameObject
            {
                name = "Grid"
            };
            grid = new GameObject[Globals.currentScene.weight, Globals.currentScene.height];
            for(int i = 0; i < Globals.currentScene.weight; ++i)
            {
                for(int j = 0; j < Globals.currentScene.height; ++j)
                {
                    GameObject obj = new GameObject
                    {
                        name = "Grid:" + i + ";" + j
                    };
                    obj.tag = "Cases";
                    obj.AddComponent<CaseObject>();
                    obj.GetComponent<CaseObject>().c = Globals.currentScene.cases[i, j];
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/free0");
                    obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    obj.AddComponent<BoxCollider>();
                    obj.GetComponent<BoxCollider>().size = new Vector3(CASESIZE, CASESIZE, 0.1f);
                    obj.transform.eulerAngles = new Vector3(90, 0, 0);
                    obj.transform.position = CalculatePosition(i, j, 0);
                    grid[i, j] = obj;
                    obj.transform.parent = gridObjects.transform;
                }
            }

            //Background
            GameObject back = new GameObject
            {
                name = "background"
            };
            back.transform.eulerAngles = new Vector3(90, 0, 0);
            back.transform.position = new Vector3(0, -0.02f, 0);
            SpriteRenderer backRenderer = back.AddComponent<SpriteRenderer>();
            backRenderer.sortingOrder = 0;
            backRenderer.sprite = Resources.Load<Sprite>("images/background/" + Globals.currentScene.name);
            back.tag = "Background";

            //Adds
            add0Objects = new GameObject
            {
                name = "Adds0"
            };
            add1Objects = new GameObject
            {
                name = "Adds1"
            };
            add2Objects = new GameObject
            {
                name = "Adds2"
            };
            for (int i = 0; i < layers.Count; ++i)
            {
                foreach(LayerImage add in layers[i])
                {
                    GameObject image = new GameObject
                    {
                        name = "add" + i + ":" + add.name + ":" + add.x + ";" + add.y
                    };
                    image.AddComponent<ImageObject>();
                    image.GetComponent<ImageObject>().layerImage = add;
                    grid[add.x, add.y].GetComponent<CaseObject>().c.layerImage = add;
                    image.transform.eulerAngles = new Vector3(90, 90 * add.face, 0);
                    image.transform.position = CalculatePosition(add.x, add.y, high[i]);
                    SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                    renderer.sortingOrder = i+2;
                    if (i == 0)
                    {
                        Redraw(image, "add0");
                        image.transform.parent = add0Objects.transform;
                    }
                    else if (i == 1)
                    {
                        grid[add.x, add.y].GetComponent<SpriteRenderer>().sprite = add.high < 100 ? Resources.Load<Sprite>("images/grid/add10") : Resources.Load<Sprite>("images/grid/wall");
                        image.transform.parent = add1Objects.transform;
                    }
                    else
                    {
                        image.transform.parent = add2Objects.transform;
                    }
                    image.tag = "Adds" + i;
                }
            }

            //Walls
            wallObjects = new GameObject
            {
                name = "Walls"
            };
            foreach(LayerImage wall in Globals.currentScene.walls)
            {
                GameObject obj = new GameObject
                {
                    name = "wall:" + wall.name + ":" + wall.x + ";" + wall.y
                };
                obj.transform.position = CalculatePosition(wall.x, wall.y, 1.5f, wall.weight, wall.height);
                obj.tag = "Walls";
                obj.AddComponent<WallObject>();
                obj.GetComponent<WallObject>().wall = wall;

                //Grids of the wall
                for (int k = wall.x; k < wall.x + wall.weight; ++k)
                {
                    for (int l = wall.y; l < wall.y + wall.height; ++l)
                    {
                        grid[k, l].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/wall");
                    }
                }

                //Sides of the wall
                for (int i = 0; i < faces.Length; ++i)
                {
                    GameObject img = new GameObject
                    {
                        name = obj.name + "_" + faces[i]
                    };
                    img.transform.parent = obj.transform;
                    Vector3 position = new Vector3();
                    float _weight = wall.weight;
                    float _height = wall.height;
                    switch (faces[i])
                    {
                        case "0":
                            position = new Vector3(0, CASESIZE, -_height / 2 * CASESIZE);
                            break;

                        case "1":
                            position = new Vector3(-_weight / 2 * CASESIZE, CASESIZE, 0);
                            break;

                        case "2":
                            position = new Vector3(0, CASESIZE, _height / 2 * CASESIZE);
                            break;

                        case "3":
                            position = new Vector3(_weight / 2 * CASESIZE, CASESIZE, 0);
                            break;

                        case "top":
                            position = new Vector3(0, 2.5f * CASESIZE, 0);
                            break;
                    }
                    img.transform.localPosition = position;
                    img.transform.eulerAngles = new Vector3(i == 4 ? 90 : 0, i == 4 ? 0 : i * 90, 0);
                    img.AddComponent<SpriteRenderer>();
                    img.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/walls/" + Globals.currentScene.name + "_" + wall.name + "_" + faces[i]);
                    img.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
                lstWalls[wall.face].Add(obj);
                obj.transform.parent = wallObjects.transform;
            }

            //Entities
            characterObjects = new GameObject
            {
                name = "Characters"
            };
            ennemyObjects = new GameObject
            {
                name = "Ennemies"
            };
            npcObjects = new GameObject
            {
                name = "Npcs"
            };
            foreach(Entity entity in Globals.currentScene.entities)
            {
                GameObject image = new GameObject
                {
                    name = entity.type + ":" + entity.name + ":" + (entity.type == "character" ? "" : entity.x + ";" + entity.y)
                };
                image.transform.position = CalculatePosition(entity.x, entity.y, 1);
                SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                renderer.sortingOrder = 3;
                image.tag = "Entities";
                image.AddComponent<EntityObject>();
                image.GetComponent<EntityObject>().entity = entity;
                grid[entity.x, entity.y].GetComponent<CaseObject>().c.entity = entity;
                grid[entity.x, entity.y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/" + entity.type + 0);
                switch (entity.type)
                {
                    case "character":
                        image.transform.parent = characterObjects.transform;
                        break;

                    case "ennemy":
                        image.transform.parent = ennemyObjects.transform;
                        break;

                    case "npc":
                        image.transform.parent = npcObjects.transform;
                        break;
                }
            }

            Globals.visibilityManager.Compute(1, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.fov);
            
            ChangeCameraPosition();
        }

        /// <summary>
        /// Method which change the camera's point of vue
        /// </summary>
        /// <param name="direction">Direction to change</param>
        public void ChangeAngle(byte direction) //0=Up/1=Down/2=Left/3=Right
        {
            //Check the direction wanted and modify the camera settings
            switch (direction)
            {
                case 0:
                    if (!top)
                    {
                        top = true;
                        camera.transform.Rotate(60, 0, 0);
                        camera.transform.position = new Vector3(0, 10 * CASESIZE, 0);
                        ChangeSkins();
                    }
                    break;

                case 1:
                    if (top)
                    {
                        top = false;
                        camera.transform.Rotate(-60, 0, 0);
                        ChangeCameraPosition();
                    }
                    break;

                case 2:
                case 3:
                    camera.transform.Rotate(0, (direction == 2 ? 90 : -90), 0, Space.World);
                    face = Convert.ToByte((face + (direction == 2 ? 1 : 3))%4);
                    if(!top)
                    {
                        ChangeCameraPosition();
                    }
                    break;
            }
        }

        /// <summary>
        /// Method which translate the camera position
        /// </summary>
        /// <param name="direction">Direction of the vector</param>
        /// <param name="speed">Speed of the camera</param>
        public void MoveCamera(byte direction, int speed)
        {
            //Change the setting based on the current face
            direction = Convert.ToByte((face + direction) % 4);
            if(face == 1 || face == 3)
            {
                direction = Convert.ToByte((direction + 2) % 4);
            }
            
            //Translate the camera's position
            switch (direction)
            {
                case 0:
                    if(camera.transform.position.x < Globals.currentScene.weight / 2 * CASESIZE + (face == 3 ? 3 * CASESIZE : (face == 1 ? -3 * CASESIZE : 0)))
                    {
                        camera.transform.Translate(new Vector3(speed * Time.deltaTime, 0), Space.World); // move on +X axis
                    }
                    break;

                case 2:
                    if(camera.transform.position.x > -Globals.currentScene.weight / 2 * CASESIZE - (face == 1 ? 3 * CASESIZE : (face == 3 ? -3 * CASESIZE : 0)))
                    {
                        camera.transform.Translate(new Vector3(-(speed * Time.deltaTime), 0), Space.World); // move on -X axis
                    }
                    break;

                case 1:
                    if(camera.transform.position.z < Globals.currentScene.height / 2 * CASESIZE + (face == 2 ? 3 * CASESIZE : (face == 0 ? -3 * CASESIZE : 0)))
                    {
                        camera.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.World); // move on +Z axis
                    }
                    break;

                case 3:
                    if(camera.transform.position.z > -Globals.currentScene.height / 2 * CASESIZE - (face == 0 ? 3 * CASESIZE : (face == 2 ? -3 * CASESIZE : 0)))
                    {
                        camera.transform.Translate(new Vector3(0, 0, -(speed * Time.deltaTime)), Space.World); // move on -Z axis
                    }
                    break;
            }
        }

        /// <summary>
        /// Method which change the camera's fov
        /// </summary>
        /// <param name="minus">ScrollWheel's value under 0</param>
        public void Zoom(bool minus)
        {
            camera.fieldOfView += (minus ? ZOOMSPEED : -ZOOMSPEED);
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, MINFOV, MAXFOV);
        }

        /// <summary>
        /// Method which clear the cases' possibility value
        /// </summary>
        public void CleanCases(bool effect = false)
        {
            foreach(Case c in Globals.currentScene.cases)
            {
                if(c.possibility != 0)
                {
                    if (!effect || (effect && c.possibility != 2 && c.possibility != 3))
                    {
                        c.possibility = 0;
                        ChangeObject("grid", c.x + ";" + c.y, "redraw");
                    }
                }
            }
        }

        /// <summary>
        /// Method which redraw object by his type
        /// </summary>
        /// <param name="type">Type of the object</param>
        /// <param name="name">Name of the object to change</param>
        public void ChangeObject(string type, string name, string operation, bool recursive = false, GameObject send = null)
        {
            GameObject objectToChange = null;
            string tag = null;
            
            //Get object
            if (recursive)
            {
                objectToChange = send;
            }
            else if (type == "grid")
            {
                string[] tempCoord = name.Split(';');
                objectToChange = grid[Convert.ToInt32(tempCoord[0]), Convert.ToInt32(tempCoord[1])];
            }
            else
            {
                switch (type)
                {
                    case "character":
                    case "ennemy":
                    case "npc":
                        tag = "Entities";
                        break;

                    case "add0":
                        tag = "Adds0";
                        break;

                    case "add1":
                        tag = "Adds1";
                        break;
                }
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.name.Contains(name))
                    {
                        objectToChange = gameObject;
                        break;
                    }
                }
            }

            //Choose what to do with it
            switch (operation)
            {
                //Redraw the gameObject
                case "redraw":
                    Redraw(objectToChange, type);
                    break;

                //Delete the gameObject
                case "delete":
                    UnityEngine.Object.Destroy(objectToChange);
                    break;

                //Change the position and rotation of the gameObject
                case "move":
                    if(tag == "Entities")
                    {
                        Entity entity = objectToChange.GetComponent<EntityObject>().entity;
                        objectToChange.transform.position = CalculatePosition(entity.x, entity.y, 1);
                    }
                    else
                    {
                        LayerImage image = objectToChange.GetComponent<ImageObject>().layerImage;
                        objectToChange.transform.position = CalculatePosition(image.x, image.y, type == "add0" ? -0.01f : type == "add1" ? 1 : 3.5f);
                    }
                    ChangeObject(type, name, "redraw", true, objectToChange);
                    break;

            }
        }
        
        /// <summary>
        /// Method which change the skins in the scene
        /// </summary>
        public void ChangeSkins()
        {
            //Grid
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cases"))
            {
                Redraw(gameObject, "grid");
            }

            //Adds1
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Adds1"))
            {
                Redraw(gameObject, "add1");
            }

            //Adds2
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Adds2"))
            {
                Redraw(gameObject, "add2");
            }

            //Hide walls
            foreach(List<GameObject> walls in lstWalls)
            {
                foreach(GameObject wall in walls)
                {
                    Redraw(wall, "wall");
                }
            }

            //Entities
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Entities"))
            {
                Redraw(gameObject, "entity");
            }
        }

        /// <summary>
        /// Method which calculate the position of a GameObject
        /// </summary>
        /// <param name="x">X coordinate (XAxis)</param>
        /// <param name="y">Y coordinate (ZAxis)</param>
        /// <param name="weight">Weight of the object</param>
        /// <param name="height">Height of the object</param>
        /// <param name="high">High of the object (YAxis)</param>
        /// <returns></returns>
        private Vector3 CalculatePosition(int x, int y, float high, float weight = 1, float height = 1)
        {
            return new Vector3((x - Globals.currentScene.weight / 2 + weight / 2) * CASESIZE, high, ((y - Globals.currentScene.height / 2 + height / 2) * CASESIZE) * -1);
        }

        /// <summary>
        /// Method which change the position of the camera based on the direction facing
        /// </summary>
        private void ChangeCameraPosition()
        {
            //Change the camera's position based on the face
            switch (face)
            {
                case 0:
                    camera.transform.position = new Vector3(0, 5 * CASESIZE, (-Globals.currentScene.height / 2 - 3) * CASESIZE);
                    break;

                case 1:
                    camera.transform.position = new Vector3((-Globals.currentScene.weight / 2 - 3) * CASESIZE, 5 * CASESIZE, 0);
                    break;

                case 2:
                    camera.transform.position = new Vector3(0, 5 * CASESIZE, (Globals.currentScene.height / 2 + 3) * CASESIZE);
                    break;

                case 3:
                    camera.transform.position = new Vector3((Globals.currentScene.weight / 2 + 3) * CASESIZE, 5 * CASESIZE, 0);
                    break;
            }

            ChangeSkins();
        }

        /// <summary>
        /// Method which redraw a GameObject
        /// </summary>
        /// <param name="gameObject">GameObject to redraw</param>
        /// <param name="type">Type of the GameObject</param>
        private void Redraw(GameObject gameObject, string type)
        {
            if (type == "grid")
            {
                Case c = gameObject.GetComponent<CaseObject>().c;
                gameObject.transform.eulerAngles = new Vector3(90, 90 * face, 0);
                if (c.visible)
                {
                    if (c.type == "wall")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/" + c.type);
                    }
                    else if(c.possibility == 5)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/effect");
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/" + c.type + c.state + c.possibility);
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/black");
                }
            }
            else
            {
                switch (type)
                {
                    case "entity":
                    case "character":
                    case "ennemy":
                    case "npc":
                        Entity entity = gameObject.GetComponent<EntityObject>().entity;
                        if (Globals.currentScene.cases[entity.x, entity.y].visible)
                        {
                            if (top)
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("skins/" + entity.type + "/" + entity.name + "_top");
                                gameObject.transform.eulerAngles = new Vector3(90, 90 * entity.face, 0);
                                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
                            }
                            else
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("skins/" + entity.type + "/" + entity.name + "_" + ((face + 4 - entity.face) % 4));
                                gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                            }
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = null;
                        }
                        break;

                    case "add0":
                        LayerImage image0 = gameObject.GetComponent<ImageObject>().layerImage;
                        if (Globals.currentScene.cases[image0.x, image0.y].visible)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds0/" + image0.name);
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = null;
                        }
                        break;

                    case "add1":
                        LayerImage image1 = gameObject.GetComponent<ImageObject>().layerImage;
                        if (Globals.currentScene.cases[image1.x, image1.y].visible)
                        {
                            if (top)
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds1/" + image1.name + "_top");
                                gameObject.transform.eulerAngles = new Vector3(90, 90 * image1.face, 0);
                                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
                            }
                            else
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds1/" + image1.name + "_" + ((face + 4 - image1.face) % 4));
                                gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                            }
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = null;
                        }
                        break;

                    case "add2":
                        LayerImage image2 = gameObject.GetComponent<ImageObject>().layerImage;
                        if (Globals.currentScene.cases[image2.x, image2.y].visible)
                        {
                            if (top)
                            {
                                gameObject.SetActive(false);
                            }
                            else
                            {
                                gameObject.SetActive(true);
                                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds2/" + image2.name + "_" + ((face + 4 - image2.face) % 4));
                                gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                            }
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = null;
                        }
                        break;

                    case "wall":
                        LayerImage wall = gameObject.GetComponent<WallObject>().wall;
                        bool visible = false;
                        for (int i = wall.x; i < wall.x + wall.weight; ++i)
                        {
                            for (int j = wall.y; j < wall.y + wall.height; ++j)
                            {
                                if (Globals.currentScene.cases[i, j].visible)
                                {
                                    visible = true;
                                    break;
                                }
                            }
                            if (visible) { break; }
                        }
                        if (visible)
                        {
                            gameObject.SetActive(!(!top && lstWalls[face].Contains(gameObject)));
                        }
                        else
                        {
                            gameObject.SetActive(false);
                        }
                        break;
                }
            }
        }
    }
}
