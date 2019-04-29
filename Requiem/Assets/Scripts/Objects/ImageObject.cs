using Requiem.Class;
using System;
using UnityEngine;

namespace Requiem.Objects
{
    /// <summary>
    /// Class used to hold the LayerImage of a GameObject
    /// </summary>
    public class ImageObject : MonoBehaviour
    {
        public LayerImage layerImage;

        //Execute the image's scripts
        public void OnMouseUpAsButton()
        {
            bool close = false;

            //Check if the player is close
            for(int i = layerImage.x; i < layerImage.x + layerImage.weight; ++i)
            {
                for(int j = layerImage.y; j < layerImage.y + layerImage.height; ++j)
                {
                    if(Math.Abs(i - Globals.currentCharacter.x) + Math.Abs(j - Globals.currentCharacter.y) == 1) { close = true; }
                }
            }

            //Execute each script if close
            if(close && layerImage.scripts != null)
            {
                foreach(LayerScript script in layerImage.scripts)
                {
                    if (script.state)
                    {
                        Globals.scriptManager.ExecuteScript(script, Globals.currentCharacter);
                    }
                }
            }
        }
    }
}