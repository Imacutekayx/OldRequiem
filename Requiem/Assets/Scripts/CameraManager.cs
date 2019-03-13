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

        //TODO CHANGE
        private const int CASE = 10, UP = 40;

        //Constructor
        public CameraManager(Camera _camera)
        {
            camera = _camera;
        }

        /// <summary>
        /// Change the current scene
        /// </summary>
        /// <param name="_scene">Scene to see</param>
        public void LoadNewScene(Scene _scene)
        {
            scene = _scene;
            ChangeSkins();
        }

        /// <summary>
        /// Method which change the camera's point of vue
        /// </summary>
        /// <param name="direction">Direction to change</param>
        public void ChangeAngle(byte direction) //0=Up/1=Down/2=Left/3=Right
        {
            //TODO Fix rotation
            switch (direction)
            {
                case 0:
                    if (!top)
                    {
                        top = true;
                        camera.transform.Rotate(0, 0, 60);
                        camera.transform.position = new Vector3(0, UP, 0);
                        
                        ChangeSkins();
                    }
                    break;

                case 1:
                    if (top)
                    {
                        top = false;
                        camera.transform.Rotate(0, 0, -60);
                        ChangeCameraPosition();
                    }
                    break;

                case 2:
                case 3:
                    camera.transform.Rotate(0, direction == 2 ? -90 : 90, 0);
                    face = Convert.ToByte((face + direction == 2 ? 3 : 1)%4);
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
                    camera.transform.position = new Vector3(0, CASE * (scene.height + 5), UP / 2);
                    break;

                case 1:
                    camera.transform.position = new Vector3(CASE * (scene.weight + 5), 0, UP / 2);
                    break;

                case 2:
                    camera.transform.position = new Vector3(0, CASE * (scene.height + 5) * -1, UP / 2);
                    break;

                case 3:
                    camera.transform.position = new Vector3(CASE * (scene.weight + 5) * -1, 0, UP / 2);
                    break;
            }

            ChangeSkins();
        }

        /// <summary>
        /// Method which change the skins in the scene
        /// </summary>
        private void ChangeSkins()
        {
            foreach (Entity entity in scene.entities)
            {
                byte FinalFace = Convert.ToByte((face + entity.face) % 4);
                try
                {
                    entity.skin = Resources.Load<Sprite>("entity/" + entity.type + "/" + entity.name + "_" + (top ? "top" : "face" + FinalFace) + (entity.dead ? "_dead" : "") + ".png");
                }
                catch(Exception e)
                {
                    entity.skin = Resources.Load<Sprite>("default.png");
                }
            }
            
            //TODO Change walls' skin

            //TODO APPLY SKIN TO OBJECT
        }
    }
}
