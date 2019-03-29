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
        private byte face;
        private bool top = false;

        //Objects
        private Scene scene;
        private Camera camera;

        //Constructor
        public CameraManager(Camera _camera)
        {
            //TODO Grid
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

            //Objects
            List<List<LayerImage>> layers = new List<List<LayerImage>>
            {
                scene.adds0,
                scene.adds1,
                scene.adds2
            };

            GameObject back = new GameObject
            {
                name = "background"
            };
            back.transform.eulerAngles = new Vector3(90, 0, 0);
            back.transform.position = new Vector3(0, -0.02f, 0);
            SpriteRenderer backRenderer = back.AddComponent<SpriteRenderer>();
            backRenderer.sortingOrder = 0;
            backRenderer.sprite = Resources.Load<Sprite>("images/background/" + scene.name);
            back.tag = "Background";

            for(int i = 0; i < layers.Count; ++i)
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
                    }
                    image.AddComponent<ImageObject>();
                    image.GetComponent<ImageObject>().layerImage = add;
                    image.tag = "Adds" + i;
                }
            }

            //Walls
            foreach(LayerImage wall in scene.walls)
            {
                Debug.Log(wall.x + ":" + wall.y + ";" + wall.weight + ":" + wall.height);
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
            }

            //Entities
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
            }
            
            //TODO Camera basic position
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
                        camera.transform.position = new Vector3(0, 20, 0);
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
                    face = Convert.ToByte((face + (direction == 2 ? 3 : 1))%4);
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
                    camera.transform.position = new Vector3(0, 5, -scene.height / 2 - 5);
                    break;

                case 1:
                    camera.transform.position = new Vector3(scene.weight / 2 + 5, 5, 0);
                    break;

                case 2:
                    camera.transform.position = new Vector3(0, 5, scene.height / 2 + 5);
                    break;

                case 3:
                    camera.transform.position = new Vector3(-scene.weight / 2 - 5, 5, 0);
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

            //TODO Hide walls


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
