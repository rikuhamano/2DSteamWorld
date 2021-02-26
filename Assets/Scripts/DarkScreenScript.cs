using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkScreenScript : MonoBehaviour {
    public Renderer m_Display;
    public bool grab = false;

    public void setGrab (bool Sgrab) {
        grab = Sgrab;
    }

    void OnPostRender () {
        if (grab) {//Create a new texture with the width and height of the screen
            Texture2D tex = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            tex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);
            tex.Apply ();
            //Check that the display field has been assigned in the Inspector
            if (m_Display != null) {
                Sprite sprite = Sprite.Create (tex, new Rect (0, 0, Screen.width, Screen.height), new Vector2 (0.0f, 0.0f), 42.0f);
                m_Display.GetComponent<SpriteRenderer> ().sprite = sprite;
            }
            //Reset the grab state
            grab = false;
        }
    }

}