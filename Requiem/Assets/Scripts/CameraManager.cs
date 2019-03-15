using System;
using UnityEngine;
using Requiem.Class;
using Requiem.Objects;

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
            camera = _camera;
            camera.transform.eulerAngles = new Vector3(30, 0, 0);
        }

        /// <summary>
        /// Change the current scene
        /// </summary>
        /// <param name="_scene">Scene to see</param>
        public void LoadNewScene(Scene _scene)
        {
            //TODO THINK ABOUT MULTIPLE CASES OBJECT
            scene = _scene;

            //Background
            foreach (LayerImage background in scene.background)
            {
                GameObject image = new GameObject
                {
                    name = "background:" + background.x + ";" + background.y
                };
                image.transform.eulerAngles = new Vector3(90, 0, 0);
                image.transform.position = new Vector3(background.x - scene.weight / 2 + background.weight / 2, -0.2f, (background.y - scene.height / 2 + background.height / 2) * -1);
                SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("images/background/" + scene.name);
                image.tag = "Background";
            }

            //Adds0
            foreach(LayerImage add0 in scene.adds0)
            {
                GameObject image = new GameObject
                {
                    name = "add0:" + add0.name + ":" + add0.x + ";" + add0.y
                };
                image.transform.eulerAngles = new Vector3(90, 90 * add0.face, 0);
                image.transform.position = new Vector3(add0.x - scene.weight / 2 + add0.weight / 2, -0.1f, (add0.y - scene.height / 2 + add0.height / 2) * -1);
                SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("images/adds0/" + add0.name);
                image.tag = "Adds0";
            }

            //Adds1
            foreach (LayerImage add1 in scene.adds1)
            {
                GameObject image = new GameObject
                {
                    name = "add1:" + add1.name + ":" + add1.x + ";" + add1.y
                };
                image.transform.position = new Vector3(add1.x - scene.weight / 2 + add1.weight / 2, 1, (add1.y - scene.height / 2 + add1.height / 2) * -1);
                image.AddComponent<SpriteRenderer>();
                image.AddComponent<ImageObject>();
                image.GetComponent<ImageObject>().layerImage = add1;
                image.tag = "Adds1";
            }

            //Adds2
            foreach (LayerImage add2 in scene.adds2)
            {
                GameObject image = new GameObject
                {
                    name = "add2:" + add2.name + ":" + add2.x + ";" + add2.y
                };
                image.transform.position = new Vector3(add2.x - scene.weight / 2 + add2.weight / 2, 3.5f, (add2.y - scene.height / 2 + add2.height / 2) * -1);
                image.AddComponent<SpriteRenderer>();
                image.AddComponent<ImageObject>();
                image.GetComponent<ImageObject>().layerImage = add2;
                image.tag = "Adds2";
            }

            //TODO Walls(NewList?)
            //Entities
            foreach(Entity entity in scene.entities)
            {
                GameObject image = new GameObject
                {
                    name = entity.type + ":" + entity.name + ":" + (entity.type == "character" ? "" : entity.x + ";" + entity.y)
                };
                image.transform.position = new Vector3(entity.x - scene.weight / 2 + 0.5f / 2, 1, (entity.y - scene.height / 2 + 0.5f) * -1);
                image.AddComponent<SpriteRenderer>();
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
        /// Method which change the position of the camera based on the direction facing
        /// </summary>
        private void ChangeCameraPosition()
        {
            switch (face)
            {
                case 0:
                    camera.transform.position = new Vector3(0, 10, -scene.height / 2 - 5);
                    break;

                case 1:
                    camera.transform.position = new Vector3(scene.weight / 2 + 5, 10, 0);
                    break;

                case 2:
                    camera.transform.position = new Vector3(0, 10, scene.height / 2 + 5);
                    break;

                case 3:
                    camera.transform.position = new Vector3(-scene.weight / 2 - 5, 10, 0);
                    break;
            }

            ChangeSkins();
        }

        /// <summary>
        /// Method which change the skins in the scene
        /// </summary>
        private void ChangeSkins()
        {
            //TODO Change skins Walls
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
