using UnityEngine;
using Requiem.Class;
using System.Collections.Generic;
using System;

namespace Requiem
{
    /// <summary>
    /// Basic class of the program
    /// </summary>
    public class Main : MonoBehaviour
    {
        private const int BOUNDARY = 50; // distance from edge scrolling starts
        private const int SPEED = 20;
        private const bool CONTROLLER = true;
        private int theScreenWidth;
        private int theScreenHeight;
        private Case c;
        private Case lastC;

        //Controller
        private byte view = 0;
        private byte mode = 0;
        private byte lastMode;
        private byte power = 0;
        private byte lastPower;

        //Start is called before the first frame update
        void Start()
        {
            ClassToREQ.Save();
            REQToClass.Load();
            theScreenWidth = Screen.width;
            theScreenHeight = Screen.height;
            Globals.cameraManager = new CameraManager(Camera.main);
            Globals.timeManager = new TimeManager();
            Globals.scriptManager = new ScriptManager();
            Globals.movementManager = new MovementManager();
            Globals.visibilityManager = new VisibilityManager();
            Globals.currentCharacter = (Character)Globals.scenes[0].entities.Find(i => i.name == "Kanis");
            Globals.cameraManager.LoadNewScene(Globals.scenes[0]);
            c = Globals.scenes[0].cases[0, 0];
        }

        //Update is called each frame
        void Update()
        {
            if (!CONTROLLER)
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
                if (Input.mousePosition.x > theScreenWidth - BOUNDARY)
                {
                    Globals.cameraManager.MoveCamera(0, SPEED);
                }
                if (Input.mousePosition.x < 0 + BOUNDARY)
                {
                    Globals.cameraManager.MoveCamera(2, SPEED);
                }
                if (Input.mousePosition.y > theScreenHeight - BOUNDARY)
                {
                    Globals.cameraManager.MoveCamera(1, SPEED);
                }
                if (Input.mousePosition.y < 0 + BOUNDARY)
                {
                    Globals.cameraManager.MoveCamera(3, SPEED);
                }

                //Zoom
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    Globals.cameraManager.Zoom(Input.GetAxis("Mouse ScrollWheel") < 0);
                }

                //Action to do
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Globals.mode = Globals.mode == "movement" ? "" : "movement";
                    Globals.cameraManager.CleanCases();
                    Debug.Log(Globals.mode);
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    Globals.mode = Globals.mode == "power" ? "" : "power";
                    Globals.cameraManager.CleanCases();
                    Globals.power = "";
                    Debug.Log(Globals.mode);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Globals.mode = Globals.mode == "attack" ? "" : "attack";
                    Globals.cameraManager.CleanCases();
                    Globals.power = "";
                    Debug.Log(Globals.mode);
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    Globals.mode = Globals.mode == "item" ? "" : "item";
                    if (Globals.mode == "item")
                    {
                        Globals.currentUsables = new List<Item>();
                        foreach (KeyValuePair<Item, int> item in Globals.currentCharacter.bag)
                        {
                            if (item.Key.use == "useable")
                            {
                                Globals.currentUsables.Add(item.Key);
                            }
                        }
                    }
                    Globals.cameraManager.CleanCases();
                    Globals.power = "";
                    Debug.Log(Globals.mode);
                }

                //Power to use
                if (Globals.mode == "power")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.powers[0].name ? "" : Globals.currentCharacter.powers[0].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "")
                        {
                            Power p = Globals.currentCharacter.powers[0];
                            if (p.elementalControl != "")
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope, true, p.elementalControl);
                            }
                            else
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope);
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.powers[1].name ? "" : Globals.currentCharacter.powers[1].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "")
                        {
                            Power p = Globals.currentCharacter.powers[1];
                            if (p.elementalControl != "")
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope, true, p.elementalControl);
                            }
                            else
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope);
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.powers[2].name ? "" : Globals.currentCharacter.powers[2].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "")
                        {
                            Power p = Globals.currentCharacter.powers[2];
                            if (p.elementalControl != "")
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope, true, p.elementalControl);
                            }
                            else
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope);
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.powers[3].name ? "" : Globals.currentCharacter.powers[3].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "")
                        {
                            Power p = Globals.currentCharacter.powers[3];
                            if (p.elementalControl != "")
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope, true, p.elementalControl);
                            }
                            else
                            {
                                Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope);
                            }
                        }
                    }
                }
                else if (Globals.mode == "attack")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.weapons[0].powers[0].name ? "" : Globals.currentCharacter.weapons[0].powers[0].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[0].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.weapons[0].powers[1].name ? "" : Globals.currentCharacter.weapons[0].powers[1].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[1].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.weapons[0].powers[2].name ? "" : Globals.currentCharacter.weapons[0].powers[2].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[2].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        Globals.power = Globals.power == Globals.currentCharacter.weapons[0].powers[3].name ? "" : Globals.currentCharacter.weapons[0].powers[3].name;
                        Globals.basic = null;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[3].scope); }
                    }
                }
                else if (Globals.mode == "item")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Globals.power = Globals.power == Globals.currentUsables[0].name ? "" : Globals.currentUsables[0].name;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(4, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentUsables[0].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        Globals.power = Globals.power == Globals.currentUsables[1].name ? "" : Globals.currentUsables[1].name;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(4, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentUsables[1].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Globals.power = Globals.power == Globals.currentUsables[2].name ? "" : Globals.currentUsables[2].name;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(4, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentUsables[2].scope); }
                    }
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        Globals.power = Globals.power == Globals.currentUsables[3].name ? "" : Globals.currentUsables[3].name;
                        Debug.Log(Globals.power);
                        Globals.cameraManager.CleanCases();
                        if (Globals.power != "") { Globals.visibilityManager.Compute(4, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentUsables[3].scope); }
                    }
                }

            }
            else
            {
                //Change angle
                if(Input.GetKeyDown(KeyCode.JoystickButton11)){
                    if(view != 4)
                    {
                        if(view == 0)
                        {
                            Globals.cameraManager.ChangeAngle(1);
                        }
                        Globals.cameraManager.ChangeAngle(3);
                        ++view;
                    }
                    else
                    {
                        Globals.cameraManager.ChangeAngle(0);
                        view = 0;
                    }
                }

                //Change mode
                if (Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    if(mode == 0)
                    {
                        mode = 4;
                    }
                    else
                    {
                        --mode;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    if (mode == 4)
                    {
                        mode = 0;
                    }
                    else
                    {
                        ++mode;
                    }
                }
                if(mode != lastMode)
                {
                    lastMode = mode;
                    switch (mode)
                    {
                        case 0:
                            Globals.mode = "";
                            break;
                        case 1:
                            Globals.mode = "movement";
                            break;
                        case 2:
                            Globals.mode = "attack";
                            power = 0;
                            Globals.power = "";
                            break;
                        case 3:
                            Globals.mode = "power";
                            power = 0;
                            Globals.power = "";
                            break;
                        case 4:
                            Globals.mode = "item";
                            power = 0;
                            Globals.currentUsables = new List<Item>();
                            foreach (KeyValuePair<Item, int> item in Globals.currentCharacter.bag)
                            {
                                if (item.Key.use == "useable")
                                {
                                    Globals.currentUsables.Add(item.Key);
                                }
                            }
                            Globals.power = "";
                            break;
                    }
                    Globals.cameraManager.CleanCases();
                    Debug.Log(Globals.mode);
                }

                //Change power
                if(mode == 2 || mode == 3 || mode == 4)
                {
                    if (Input.GetKeyDown(KeyCode.JoystickButton6))
                    {
                        if (power == 0)
                        {
                            power = 4;
                        }
                        else
                        {
                            --power;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.JoystickButton7))
                    {
                        if (power == 4)
                        {
                            power = 0;
                        }
                        else
                        {
                            ++power;
                        }
                    }
                    if (power != lastPower)
                    {
                        lastPower = power;
                        switch (mode)
                        {
                            case 2:
                                if (power == 0)
                                {
                                    Globals.power = "";
                                }
                                else
                                {
                                    Globals.power = Globals.currentCharacter.weapons[0].powers[power - 1].name;
                                }
                                Globals.basic = null;
                                Debug.Log(Globals.power);
                                Globals.cameraManager.CleanCases();
                                if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[power - 1].scope); }
                                break;

                            case 3:
                                if(power == 0)
                                {
                                    Globals.power = "";
                                }
                                else
                                {
                                    Globals.power = Globals.currentCharacter.powers[power - 1].name;
                                }
                                Globals.basic = null;
                                Debug.Log(Globals.power);
                                Globals.cameraManager.CleanCases();
                                if (Globals.power != "")
                                {
                                    Power p = Globals.currentCharacter.powers[power - 1];
                                    if (p.elementalControl != "")
                                    {
                                        Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope, true, p.elementalControl);
                                    }
                                    else
                                    {
                                        Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), p.scope);
                                    }
                                }
                                break;

                            case 4:
                                if(power == 0)
                                {
                                    Globals.power = "";
                                }
                                else
                                {
                                    Globals.power = Globals.currentUsables[power - 1].name;
                                }
                                Debug.Log(Globals.power);
                                Globals.cameraManager.CleanCases();
                                if (Globals.power != "") { Globals.visibilityManager.Compute(4, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentUsables[power - 1].scope); }
                                break;
                        }
                    }
                }

                //Movement
                if (Input.GetKeyDown(KeyCode.JoystickButton0))  //Left
                {
                    if(c.x > 0)
                    {
                        Clean();
                        c = Globals.currentScene.cases[--c.x, c.y];
                        Debug.Log(c.x + ";" + c.y);
                    }
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton1))  //Down
                {
                    if (c.y < Globals.currentScene.height - 1)
                    {
                        Clean();
                        c = Globals.currentScene.cases[c.x, ++c.y];
                        Debug.Log(c.x + ";" + c.y);
                    }
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton2))  //Right
                {
                    if (c.x < Globals.currentScene.weight - 1)
                    {
                        Clean();
                        c = Globals.currentScene.cases[++c.x, c.y];
                        Debug.Log(c.x + ";" + c.y);
                    }
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton3))  //Up
                {
                    if (c.x > 0)
                    {
                        Clean();
                        c = Globals.currentScene.cases[c.x, --c.y];
                        Debug.Log(c.x + ";" + c.y);
                    }
                }
                if (c != lastC)
                {
                    lastC = c;
                    switch (Globals.mode)
                    {
                        case "movement":
                            if (c.type == "free")
                            {
                                bool first = true;
                                foreach (Location location in Globals.movementManager.path)
                                {
                                    if (!first)
                                    {
                                        Globals.currentScene.cases[location.x, location.y].possibility = 0;
                                        Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                                    }
                                    else { first = false; }
                                }
                                Globals.movementManager.path = Globals.movementManager.CalculateMove(new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), new Location(c.x, c.y));
                                first = true;
                                foreach (Location location in Globals.movementManager.path)
                                {
                                    if (!first)
                                    {
                                        Globals.currentScene.cases[location.x, location.y].possibility = 1;
                                        Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                                    }
                                    else { first = false; }
                                }
                            }
                            break;

                        case "power":
                            if (Globals.power != "" && c.possibility == 3)
                            {
                                foreach (Power pow in Globals.currentCharacter.powers)
                                {
                                    if (pow.name == Globals.power)
                                    {
                                        Globals.scriptManager.ShowPowerArea(pow, new Location(c.x, c.y));
                                        break;
                                    }
                                }
                            }
                            break;

                        case "attack":
                            if (Globals.power != "" && c.possibility == 2)
                            {
                                string[] name = Globals.power.Split(':');
                                foreach (Power pow in Globals.currentCharacter.weapons[Convert.ToInt32(name[0])].powers)
                                {
                                    if (pow.name == name[1])
                                    {
                                        Globals.scriptManager.ShowPowerArea(pow, new Location(c.x, c.y));
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton9))
                {
                    if (!Globals.currentCharacter.busy)
                    {
                        switch (Globals.mode)
                        {
                            case "":
                                bool close = false;

                                //Check if the player is close
                                for (int i = c.layerImage.x; i < c.layerImage.x; ++i)
                                {
                                    for (int j = c.layerImage.y; j < c.layerImage.y; ++j)
                                    {
                                        if (Math.Abs(i - Globals.currentCharacter.x) + Math.Abs(j - Globals.currentCharacter.y) <= 1) { close = true; }
                                    }
                                }
                                if (c.layerImage != null)
                                {
                                    //Execute each script if close
                                    if (close && c.layerImage.scripts != null)
                                    {
                                        foreach (LayerScript script in c.layerImage.scripts)
                                        {
                                            if (script.state)
                                            {
                                                Globals.scriptManager.ExecuteScript(script, Globals.currentCharacter);
                                            }
                                        }
                                    }
                                }
                                else if (close && c.items.Count != 0)
                                {
                                    if (c.entity == null)
                                    {
                                        foreach (KeyValuePair<Item, int> item in c.items)
                                        {
                                            Globals.currentCharacter.TakeItem(item.Key, item.Value);
                                        }
                                    }
                                    else
                                    {
                                        c.entity.CheckBag();
                                    }
                                }
                                break;

                            case "movement":
                                if (c.type == "free")
                                {
                                    foreach (Location location in Globals.movementManager.path)
                                    {
                                        Globals.currentScene.cases[location.x, location.y].possibility = 0;
                                        Globals.cameraManager.ChangeObject("grid", location.x + ";" + location.y, "redraw");
                                    }
                                    Globals.movementManager.nextCase = Globals.movementManager.path.Count - 1;
                                    Globals.movementManager.StartMove();
                                }
                                break;

                            case "attack":
                                if (Globals.power != "" && (c.possibility == 5 || c.possibility == 2))
                                {
                                    string[] name = Globals.power.Split(':');
                                    foreach (Power pow in Globals.currentCharacter.weapons[Convert.ToInt32(name[0])].powers)
                                    {
                                        if (pow.name == name[1])
                                        {
                                            if (pow.mana <= Globals.currentCharacter.mp)
                                            {
                                                Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(pow.cast), "castAttack", Globals.currentCharacter, Globals.power + ";" + c.x + ":" + c.y));
                                                Globals.power = "";
                                                Globals.cameraManager.CleanCases();
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;

                            case "power":
                                if (Globals.power != "" && (c.possibility == 5 || c.possibility == 3))
                                {
                                    foreach (Power pow in Globals.currentCharacter.powers)
                                    {
                                        if (pow.name == Globals.power)
                                        {
                                            if (pow.mana <= Globals.currentCharacter.mp)
                                            {
                                                if (pow.needBasic)
                                                {
                                                    if (Globals.basic != null)
                                                    {
                                                        if (Globals.basic.x == c.x && Globals.basic.y == c.y)
                                                        {
                                                            Globals.basic = null;
                                                            Globals.cameraManager.CleanCases();
                                                            Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), pow.scope, true, pow.elementalControl);
                                                        }
                                                        else
                                                        {
                                                            double multiple = UnityEngine.Random.Range(1, 100) <= Globals.currentCharacter.dices[2] ? 1 : 1.5f;
                                                            Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(pow.cast * multiple), "castPower", Globals.currentCharacter, pow.name + ";" + c.x + ":" + c.y + ";" + Globals.basic.x + ":" + Globals.basic.y));
                                                            Globals.power = "";
                                                            Globals.basic = null;
                                                            Globals.cameraManager.CleanCases();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Globals.basic = new Location(c.x, c.y);
                                                        Globals.cameraManager.CleanCases();
                                                        Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), pow.scope);
                                                    }
                                                }
                                                else
                                                {
                                                    double multiple = UnityEngine.Random.Range(1, 100) <= Globals.currentCharacter.dices[2] ? 1 : 1.5f;
                                                    Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(pow.cast * multiple), "castPower", Globals.currentCharacter, pow.name + ";" + c.x + ":" + c.y));
                                                    Globals.power = "";
                                                    Globals.cameraManager.CleanCases();
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;

                            case "item":
                                if (Globals.power != "" && c.possibility == 4)
                                {
                                    foreach (Item item in Globals.currentUsables)
                                    {
                                        if (item.name == Globals.power)
                                        {
                                            Globals.timeManager.AddAction(new Act("script", Convert.ToInt32(Globals.currentCharacter.dices[0] / 5), "useItem", Globals.currentCharacter, Globals.power + ";" + c.x + ":" + c.y));
                                            if (!Globals.currentCharacter.UseItem(item))
                                            {
                                                Globals.currentUsables.Remove(item);
                                            }
                                            Globals.power = "";
                                            Globals.cameraManager.CleanCases();
                                            break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void Clean()
        {
            if (c.possibility == 5 || c.possibility == 2 || c.possibility == 3)
            {
                Globals.cameraManager.CleanCases(true);
                switch (Globals.mode)
                {
                    case "attack":
                        string[] name = Globals.power.Split(':');
                        foreach (Power pow in Globals.currentCharacter.weapons[Convert.ToInt32(name[0])].powers)
                        {
                            if (pow.name == name[1])
                            {
                                Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), pow.scope, true);
                                break;
                            }
                        }
                        break;

                    case "power":
                        foreach (Power pow in Globals.currentCharacter.powers)
                        {
                            if (pow.name == Globals.power)
                            {
                                if (!pow.needBasic || (pow.needBasic && Globals.basic != null))
                                    Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), pow.scope, true);
                                break;
                            }
                        }
                        break;
                }
            }
        }
    }
}