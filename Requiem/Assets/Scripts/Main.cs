﻿using UnityEngine;
using Requiem.Class;
using System.Collections.Generic;

namespace Requiem
{
    /// <summary>
    /// Basic class of the program
    /// </summary>
    public class Main : MonoBehaviour
    {
        private int Boundary  = 50; // distance from edge scrolling starts
        private int speed = 5;
        private int theScreenWidth;
        private int theScreenHeight;

        //Start is called before the first frame update
        void Start()
        {
            ClassToXML.Save();
            XMLToClass.Load();
            theScreenWidth = Screen.width;
            theScreenHeight = Screen.height;
            Globals.currentCharacter = Globals.characters[0];
            Globals.cameraManager = new CameraManager(Camera.main);
            Globals.timeManager = new TimeManager();
            Globals.scriptManager = new ScriptManager();
            Globals.movementManager = new MovementManager();
            Globals.visibilityManager = new VisibilityManager();
            Globals.cameraManager.LoadNewScene(Globals.scenes[0]);
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
                if(Globals.mode == "item")
                {
                    Globals.currentUsables = new List<Item>();
                    foreach(KeyValuePair<Item, int> item in Globals.currentCharacter.bag)
                    {
                        if(item.Key.use == "useable")
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
            if(Globals.mode == "power")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[0].name ? "" : Globals.currentCharacter.powers[0].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.powers[0].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[1].name ? "" : Globals.currentCharacter.powers[1].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.powers[1].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[2].name ? "" : Globals.currentCharacter.powers[2].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.powers[2].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Globals.power = Globals.power == Globals.currentCharacter.powers[3].name ? "" : Globals.currentCharacter.powers[3].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(3, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.powers[3].scope); }
                }
            }
            else if(Globals.mode == "attack")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Globals.power = Globals.power == "0:" + Globals.currentCharacter.weapons[0].powers[0].name ? "" : "0:" + Globals.currentCharacter.weapons[0].powers[0].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[0].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Globals.power = Globals.power == "1:" + Globals.currentCharacter.weapons[1].powers[0].name ? "" : "1:" + Globals.currentCharacter.weapons[1].powers[0].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[1].powers[0].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Globals.power = Globals.power == "0:" + Globals.currentCharacter.weapons[0].powers[1].name ? "" : "0:" + Globals.currentCharacter.weapons[0].powers[1].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[0].powers[1].scope); }
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Globals.power = Globals.power == "1:" + Globals.currentCharacter.weapons[1].powers[1].name ? "" : "1:" + Globals.currentCharacter.weapons[1].powers[1].name;
                    Debug.Log(Globals.power);
                    Globals.cameraManager.CleanCases();
                    if (Globals.power != "") { Globals.visibilityManager.Compute(2, new Location(Globals.currentCharacter.x, Globals.currentCharacter.y), Globals.currentCharacter.weapons[1].powers[1].scope); }
                }
            }
            else if(Globals.mode == "item"){
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
    }
}