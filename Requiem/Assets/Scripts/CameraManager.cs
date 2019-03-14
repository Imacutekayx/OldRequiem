using System;
using UnityEngine;
using Requiem.Class;

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
            scene = _scene;
            //TODO Camera basic position
            ChangeCameraPosition();

            //TODO Create scene
            //TEST
            GameObject gameObject = new GameObject
            {
                name = "TestBack"
            };
            gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
            gameObject.transform.position = new Vector3(0, -0.3f, 0);
            SpriteRenderer testRender = gameObject.AddComponent<SpriteRenderer>();

            //Background
            foreach (LayerImage background in scene.background)
            {
                GameObject image = new GameObject
                {
                    name = "background:" + background.x + ";" + background.y
                };
                image.transform.eulerAngles = new Vector3(90, 0, 0);
                image.transform.position = new Vector3(background.x - scene.weight / 2 + background.weight / 2, -0.2f, background.y + scene.height / 2 - background.height / 2);
                SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
            }

            //Change skin
            ChangeSkins();
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
                        camera.transform.position = new Vector3(0, 40, 0);
                        
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
                    Debug.Log(face);
                    if(!top)
                    {
                        ChangeCameraPosition();
                    }
                    break;
            }
            Debug.Log(face);
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
            //TODO Change skin of entities and hide walls
        }
    }
}
