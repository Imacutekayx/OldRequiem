using UnityEngine;

namespace Requiem
{
    public class Main : MonoBehaviour
    {
        //Start is called before the first frame update
        void Start()
        {
            ClassToXML.Save();
            //XMLToClass.Load();
            Globals.cameraManager = new CameraManager(Camera.main);
            Globals.cameraManager.LoadNewScene(Globals.scenes[0]);
        }

        //Update is called each frame
        void Update()
        {
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
        }
    }
}