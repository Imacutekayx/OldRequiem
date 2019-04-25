using UnityEngine;

namespace Requiem
{
    /// <summary>
    /// Basic class of the program
    /// </summary>
    public class Main : MonoBehaviour
    {
        public int Boundary  = 50; // distance from edge scrolling starts
        public int speed = 5;
        private int theScreenWidth;
        private int theScreenHeight;

        //Start is called before the first frame update
        void Start()
        {
            ClassToXML.Save();
            XMLToClass.Load();
            theScreenWidth = Screen.width;
            theScreenHeight = Screen.height;
            Globals.cameraManager = new CameraManager(Camera.main);
            Globals.cameraManager.LoadNewScene(Globals.scenes[0]);
            Globals.currentCharacter = Globals.characters[0];
            Globals.timeManager = new TimeManager();
            Globals.movementManager = new MovementManager();
            Globals.movementManager.TempPatern();
        }

        //Update is called each frame
        void Update()
        {
            //Change angle
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Globals.cameraManager.ChangeAngle(0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Globals.cameraManager.ChangeAngle(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Globals.cameraManager.ChangeAngle(2);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Globals.cameraManager.ChangeAngle(3);
            }

            //Move camera
            if (Input.mousePosition.x > theScreenWidth - Boundary)
            {
                Globals.cameraManager.MoveCamera(0, speed);
            }
            if (Input.mousePosition.x < 0 + Boundary)
            {
                Globals.cameraManager.MoveCamera(2, speed);
            }
            if (Input.mousePosition.y > theScreenHeight - Boundary)
            {
                Globals.cameraManager.MoveCamera(1, speed);
            }
            if (Input.mousePosition.y < 0 + Boundary)
            {
                Globals.cameraManager.MoveCamera(3, speed);
            }
        }
    }
}