using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoin : MonoBehaviour
{
    //public Vector3 offset;
    //public GameObject[] canvases;
    public GameObject CursorPerfab;
    public Color32[] cursorColors; // The colors for the player cursors
    public Transform[] icons; // The icons representing the characters
    public Transform[] otherIcons;
    private bool[] created;

    void Start()
    {
        // This checks if a player has spawned, and prevents multiple cursors being created by the same player
        
        created = new bool[] { false, false, false, false };
        PublicVars.characters = new int[] { -1, -1, -1 }; // -1 means no player selected otherwise it is the joystick number
    }

    void Update()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (Input.GetButtonDown("Special" + (i + 1)) && !created[i])
            {
                //if the player hits the jump button let them join the game by spwning a cursor
                GameObject newCursor = Instantiate(CursorPerfab, icons[0].position, Quaternion.identity) as GameObject;
                SelectMove sMove = newCursor.GetComponent<SelectMove>(); //get a reference to the SelectMove script
                sMove.playerNum = i + 1; //assign a player number
                sMove.icons = icons; // give the new cursor a referance to the icon locations
                sMove.otherIcons = otherIcons;
                GameObject canvas = newCursor.transform.Find("Canvas").gameObject;
                GameObject canvas2 = newCursor.transform.Find("Canvas2").gameObject;
                GameObject canvas3 = newCursor.transform.Find("Canvas3").gameObject;
                GameObject img = canvas.transform.Find("Image").gameObject;
                GameObject img2 = canvas2.transform.Find("Image").gameObject;
                GameObject img3 = canvas3.transform.Find("Image").gameObject;
                img.GetComponent<Image>().color = cursorColors[i]; // set the cursor color
                img2.GetComponent<Image>().color = cursorColors[i];
                img3.GetComponent<Image>().color = cursorColors[i];
                created[i] = true; // mark that the cursor was created
            }

        }
    }
}
