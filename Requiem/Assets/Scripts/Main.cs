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
            Globals.scriptManager = new ScriptManager();
            Globals.movementManager = new MovementManager();
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

            //Action to do
            if (Input.GetKeyDown(KeyCode.M))
            {
                Globals.mode = Globals.mode == "movement" ? "" : "movement";
                Debug.Log(Globals.mode);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Globals.mode = Globals.mode == "power" ? "" : "power";
                Globals.power = "";
                Debug.Log(Globals.mode);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Globals.mode = Globals.mode == "attack" ? "" : "attack";
                Globals.power = "";
                Debug.Log(Globals.mode);
            }

            //Power to use
            if(Globals.mode == "power")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[0].name ? "" : Globals.currentCharacter.powers[0].name;
                    Debug.Log(Globals.power);
                    //TODO Show reachable cases
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[1].name ? "" : Globals.currentCharacter.powers[1].name;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[2].name ? "" : Globals.currentCharacter.powers[2].name;
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[3].name ? "" : Globals.currentCharacter.powers[3].name;
                }
            }
            else if(Globals.mode == "attack")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //TODO Weapon's power (moves)
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    
                }
            }
        }
    }
}