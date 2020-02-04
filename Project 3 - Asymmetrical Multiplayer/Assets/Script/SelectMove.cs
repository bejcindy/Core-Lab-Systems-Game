using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMove : MonoBehaviour
{
    GameObject mainCam;
    [HideInInspector]
    public Transform[] icons; //set by playerJoin
    [HideInInspector]
    public Transform[] otherIcons;
    [HideInInspector]
    public int playerNum; //set by playerJoin
    int iLength;
    private int index = 0;
    private bool canMove = true;
    private bool selected = false;
    private string horizontal;
    public Text playerName;
    public Image cursor1, cursor2, cursor3;
    AudioSource aud1, aud2;
    AudioClip switching, selecting;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        AudioSource[] audioSources = mainCam.GetComponents<AudioSource>();
        aud1 = audioSources[1];
        aud2 = audioSources[0];
        switching = audioSources[1].clip;
        selecting = audioSources[0].clip;
        horizontal = "Horizontal" + playerNum; // Saves a reference to the input string (for optimization)
        iLength = icons.Length; // saves a reference to the array length for readability 
        index = playerNum - 1; // convert player num to array position (arrays start at 0 but our joysticks start at 1)
        transform.position = icons[index].position; // set the cursor to a default starting position based index
        playerName.text = "P" + playerNum; // Give the player cursor a label
    }
    void Update()
    {
        MoveCursor();
        SelectCharacter();
        StartCheck();
        //Debug.Log(index);
    }

    void MoveCursor()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw(horizontal) > 0)
            {
                index = (index + 1) % iLength; // the mod function lets us loop without an if statement
                aud1.PlayOneShot(switching);
            }
            else if (Input.GetAxisRaw(horizontal) < 0)
            {
                index = (index - 1 + iLength) % iLength;  //((x-1) + k) % k  reverse overflow
                aud1.PlayOneShot(switching);
            }
            //Debug.Log(icons[index].position);
            cursor1.transform.position = new Vector3(icons[index].position.x, icons[index].position.y, 0); // place the cursor at its new position based on the icons position
            cursor2.transform.position = new Vector3(icons[index].position.x, icons[index].position.y, 0);
            cursor3.transform.position = new Vector3(icons[index].position.x, icons[index].position.y, 0);
            canMove = false; //briefly stop the cursor from moving while joystic is pressed
        }
        if (Input.GetAxisRaw(horizontal) == 0 && !selected)
        {
            canMove = true; // let the cursor move again when no imput is detected.
            // This makes selection easier for the user.
        }
    }

    void SelectCharacter()
    {
        if (Input.GetButtonDown("Special" + playerNum))
        {
            if (selected) //if the player is already selected then deselect it
            {
                PublicVars.characters[index] = -1; //remove the player number and reset the array to -1
                icons[index].gameObject.GetComponent<Image>().color = Color.white; //change color back to normal
                otherIcons[index].gameObject.GetComponent<Image>().color = Color.white;
                otherIcons[index + 3].gameObject.GetComponent<Image>().color = Color.white;
                selected = false; // set selected to false
            }
            else if (PublicVars.characters[index] != -1)
            {
                //this player has already been selected
                //TODO: have an error sound play
                print("already taken");
            }
            else // select this character
            {
                PublicVars.characters[index] = playerNum; // update the array with your controller number
                icons[index].gameObject.GetComponent<Image>().color = Color.gray; // change the icon color
                otherIcons[index].gameObject.GetComponent<Image>().color = Color.gray;
                otherIcons[index + 3].gameObject.GetComponent<Image>().color = Color.gray;
                selected = true; // set selected bool to true
                canMove = false; // prevent movement 
                //TODO: have an select sound play
                print("player" + playerNum + " is " + index);
                aud2.PlayOneShot(selecting);
            }
        }
    }

    void StartCheck() //deturmines if the game should start
    {
        int playerCount = 0;
        for (int i = 0; i < PublicVars.characters.Length; i++)
        {
            if (PublicVars.characters[i] != -1)
            {
                playerCount++; // this counts how many players have selected characters
            }
        }
        //if at least 2 players are selected and the start button is hit then start the game
        //otherwise do nothing - Not enough players
        //if all 3 players are selected, start the game automatically
        if (playerCount > 2) //|| (Input.GetButtonDown("Submit" + playerNum) && playerCount > 1))
        {
            SceneManager.LoadScene("terrain");
        }
    }
}
