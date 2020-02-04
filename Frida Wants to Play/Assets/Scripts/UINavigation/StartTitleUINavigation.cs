using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTitleUINavigation : MonoBehaviour
{
    public GameObject up, down;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical")>0)
        {
            up.SetActive(true);
            down.SetActive(false);
            
        }
        if (up.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space))
            {
                GameManagement.Opening.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            up.SetActive(false);
            down.SetActive(true);
            
        }
        if (down.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space))
            {
                Application.Quit();
            }
        }
    }
}
