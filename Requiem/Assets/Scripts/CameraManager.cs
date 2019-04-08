using System;
using UnityEngine;
using Requiem.Class;
using Requiem.Objects;
using System.Collections.Generic;

namespace Requiem
{
    public class CameraManager
    {
        //Variables
        private byte face;  //0=S/1=W/2=N/3=E
        private bool top = false;

        //Objects
        private GameObject gridObjects;
        private GameObject add0Objects;
        private GameObject add1Objects;
        private GameObject add2Objects;
        private GameObject wallObjects;
        private GameObject characterObjects;
        private GameObject ennemyObjects;
        private GameObject npcObjects;
        private GameObject[,] grid;
        private Scene scene;
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

            //TODO Fix XAxis bug
            scene = _scene;
            for(int i = 0; i < lstWalls.Count; ++i)
            {
                lstWalls[i].Clear();
            }

            //Objects
            List<List<LayerImage>> layers = new List<List<LayerImage>>
            {
                scene.adds0,
                scene.adds1,
                scene.adds2
            };

            //Grid
            gridObjects = new GameObject
            {
                name = "Grid"
            };
            grid = new GameObject[scene.weight, scene.height];
            for(int i = 0; i < scene.weight; ++i)
            {
                for(int j = 0; j < scene.height; ++j)
                {
                    GameObject obj = new GameObject
                    {
                        name = "Grid:" + i + ";" + j
                    };
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/grid/base.png");
                    obj.transform.eulerAngles = new Vector3(90, 0, 0);
                    obj.transform.position = new Vector3(i - scene.weight / 2 + 0.5f, 0, (j - scene.height / 2 + 0.5f) * -1);
                    grid[i, j] = obj;
                    obj.transform.parent = gridObjects.transform;
                }
            }

            //TODO Fix sprite background & grid
            //Background
            GameObject back = new GameObject
            {
                name = "background"
            };
            back.transform.eulerAngles = new Vector3(90, 0, 0);
            back.transform.position = new Vector3(0, -0.02f, 0);
            SpriteRenderer backRenderer = back.AddComponent<SpriteRenderer>();
            backRenderer.sortingOrder = 0;
            Debug.Log("images/background/" + scene.name + ".png");
            backRenderer.sprite = Resources.Load<Sprite>("images/background/" + scene.name + ".png");
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
                    image.transform.eulerAngles = new Vector3(90, 90 * add.face, 0);
                    image.transform.position = new Vector3(add.x - scene.weight / 2 + add.weight / 2, high[i], (add.y - scene.height / 2 + add.height / 2) * -1);
                    SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                    renderer.sortingOrder = i+1;
                    if (i == 0)
                    {
                        renderer.sprite = Resources.Load<Sprite>("images/adds0/" + add.name);
                        image.transform.parent = add0Objects.transform;
                    }
                    else if (i == 1)
                    {
                        for(int k = add.x; k < add.x + add.weight; ++k)
                        {
                            for(int l = add.y; l < add.y + add.height; ++l)
                            {
                                grid[k, l].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/background/add1.png");
                                image.transform.parent = add1Objects.transform;
                            }
                        }
                    }
                    else
                    {
                        image.transform.parent = add2Objects.transform;
                    }
                    image.AddComponent<ImageObject>();
                    image.GetComponent<ImageObject>().layerImage = add;
                    image.tag = "Adds" + i;
                }
            }

            //Walls
            wallObjects = new GameObject
            {
                name = "Walls"
            };
            foreach(LayerImage wall in scene.walls)
            {
                GameObject obj = new GameObject
                {
                    name = "wall:" + wall.name + ":" + wall.x + ";" + wall.y
                };
                obj.transform.position = new Vector3(wall.x - scene.weight / 2 + wall.weight / 2, 1.5f, (wall.y - scene.height / 2 + wall.height / 2) * -1);
                obj.AddComponent<ImageObject>();
                obj.GetComponent<ImageObject>().layerImage = wall;
                obj.tag = "Walls";
                
                for(int i = 0; i < faces.Length; ++i)
                {
                    GameObject img = new GameObject
                    {
                        name = obj.name + "_" + faces[i]
                    };
                    img.transform.parent = obj.transform;
                    Vector3 position = new Vector3();
                    switch (faces[i])
                    {
                        case "0":
                            position = new Vector3(0, 1.5f, -wall.height / 2);
                            break;

                        case "1":
                            position = new Vector3(-wall.weight / 2, 1.5f, 0);
                            break;

                        case "2":
                            position = new Vector3(0, 1.5f, wall.height / 2);
                            break;

                        case "3":
                            position = new Vector3(wall.weight / 2, 1.5f, 0);
                            break;

                        case "top":
                            position = new Vector3(0, 3, 0);
                            break;
                    }
                    img.transform.localPosition = position;
                    img.transform.eulerAngles = new Vector3(i == 4 ? 90 : 0, i == 4 ? 0 : i * 90, 0);
                    img.AddComponent<SpriteRenderer>();
                    img.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/walls/" + scene.name + "_" + wall.name + "_" + faces[i]);
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
            foreach(Entity entity in scene.entities)
            {
                GameObject image = new GameObject
                {
                    name = entity.type + ":" + entity.name + ":" + (entity.type == "character" ? "" : entity.x + ";" + entity.y)
                };
                image.transform.position = new Vector3(entity.x - scene.weight / 2 + 0.5f / 2, 1, (entity.y - scene.height / 2 + 0.5f) * -1);
                SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                renderer.sortingOrder = 2;
                image.AddComponent<EntityObject>();
                image.GetComponent<EntityObject>().entity = entity;
                image.tag = "Entities";
                for (int k = entity.x; k < entity.x + entity.weight; ++k)
                {
                    for (int l = entity.y; l < entity.y + entity.height; ++l)
                    {
                        grid[k, l].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/background/" + entity.type + ".png");
                    }
                }
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
            
            ChangeCameraPosition();
        }

        /// <summary>
        /// Method which change the camera's point of vue
        /// </summary>
        /// <param name="direction">Direction to change</param>
        public void ChangeAngle(byte direction) //0=Up/1=Down/2=Left/3=Right
        {
            switch (direction)
            {
                case 0:
                    if (!top)
                    {
                        top = true;
                        camera.transform.Rotate(60, 0, 0);
                        camera.transform.position = new Vector3(0, 15, 0);
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
                    foreach (GameObject obj in lstWalls[face])
                    {
                        obj.SetActive(true);
                    }
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
            direction = Convert.ToByte((face + direction) % 4);
            if(face == 1 || face == 3)
            {
                direction = Convert.ToByte((direction + 2) % 4);
            }

            switch (direction)
            {
                case 0:
                    camera.transform.Translate(new Vector3(speed * Time.deltaTime, 0), Space.World); // move on +X axis
                    break;

                case 2:
                    camera.transform.Translate(new Vector3(-(speed * Time.deltaTime), 0), Space.World); // move on -X axis
                    break;

                case 1:
                    camera.transform.Translate(new Vector3(0,0,speed * Time.deltaTime), Space.World); // move on +Z axis
                    break;

                case 3:
                    camera.transform.Translate(new Vector3(0,0,-(speed * Time.deltaTime)), Space.World); // move on -Z axis
                    break;
            }
        }

        /// <summary>
        /// Method which change the position of the camera based on the direction facing
        /// </summary>
        private void ChangeCameraPosition()
        {
            switch (face)
            {
                case 0:
                    camera.transform.position = new Vector3(0, 5, -scene.height / 2 - 3);
                    break;

                case 1:
                    camera.transform.position = new Vector3(-scene.weight / 2 - 3, 5, 0);
                    break;

                case 2:
                    camera.transform.position = new Vector3(0, 5, scene.height / 2 + 3);
                    break;

                case 3:
                    camera.transform.position = new Vector3(scene.weight / 2 + 3, 5, 0);
                    break;
            }

            ChangeSkins();
        }

        /// <summary>
        /// Method which change the skins in the scene
        /// </summary>
        private void ChangeSkins()
        {
            //Adds1
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Adds1"))
            {
                LayerImage image = gameObject.GetComponent<ImageObject>().layerImage;
                if (top)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds1/" + image.name + "_top");
                    gameObject.transform.eulerAngles = new Vector3(90, 90 * image.face, 0);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds1/" + image.name + "_" + ((face + image.face) % 4));
                    gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                }
            }

            //Adds2
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Adds2"))
            {
                LayerImage image = gameObject.GetComponent<ImageObject>().layerImage;
                if (top)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/adds2/" + image.name + "_" + ((face + image.face) % 4));
                    gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                }
            }

            //Hide walls
            foreach(GameObject obj in lstWalls[face])
            {
                obj.SetActive(false);
            }

            //Entities
            foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Entities"))
            {
                Entity entity = gameObject.GetComponent<EntityObject>().entity;
                if (top)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("skins/" + entity.type + "/" + entity.name + "_top");
                    gameObject.transform.eulerAngles = new Vector3(90, 90 * entity.face, 0);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("skins/" + entity.type + "/" + entity.name + "_" + ((face + entity.face) % 4));
                    gameObject.transform.eulerAngles = new Vector3(0, 90 * face, 0);
                }
            }
        }

        /// <summary>
        /// Method which change a certain object by a certain state
        /// </summary>
        /// <param name="gameObject">Object to change</param>
        /// <param name="type">Type of the object</param>
        /// <param name="state">State of the change (mov;x;y/dead)</param>
        private void ChangeObject(GameObject gameObject, string type, string state)
        {

        }
    }
}
