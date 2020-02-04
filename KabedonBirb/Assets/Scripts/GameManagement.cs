using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public static int state;
    public static int chickenCounter;
    public static float t;
    //public static bool helpMenu, pauseMenu;
    //public static int nChicken = 6;
    //public static GameObject[] chicken;

    public GameObject openIcon, closeIcon;
    public GameObject loadingCanvas, setUpCanvas, gamePlayCanvas, gameOverCanvas;
    public GameObject fence;
    public GameObject chick;
    public GameObject occlusionPlane;
    public Text GameOverTime;
    public Text chickenText;
    public Text timerText;

    GameObject[] planes;
    GameObject[] chicken;
    GameObject ground;
    GameObject chickenHouse;
    float chickenHeight;
    float chickenScale;
    //float t;
    int nChicken = 6;
    int seconds, minutes;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        //helpMenu = false;
        //pauseMenu = false;
        chicken = new GameObject[nChicken];
        chickenHeight = chick.GetComponent<Collider>().bounds.size.y;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (chickenCounter == nChicken && state == 2)
        {
            state = 3;
        }
        if (state == 0)
        {
            occlusionPlane.GetComponent<MeshRenderer>().enabled = true;
            occlusionPlane.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            occlusionPlane.GetComponent<MeshRenderer>().enabled = false;
            occlusionPlane.GetComponent<LineRenderer>().enabled = false;
        }
        State();
    }

    void State()
    {
        switch (state)
        {
            //maybe case 0 should be start scene

            //loading scene
            case (0):
                //loading icon and instruction image should be on by default
                loadingCanvas.SetActive(true);
                setUpCanvas.SetActive(false);
                gamePlayCanvas.SetActive(false);
                gameOverCanvas.SetActive(false);
                planes = GameObject.FindGameObjectsWithTag("Plane");
                //disable setEnvironment script
                gameObject.GetComponent<SetEnvironment>().enabled = false;
                //if plane.length>0, turn off loading icon and activate continue button
                if (planes.Length > 0)
                {
                    loadingCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    loadingCanvas.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    loadingCanvas.transform.GetChild(1).gameObject.SetActive(true);
                    loadingCanvas.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;

            //set play area
            case (1):
                //turn off every thing in case 0
                //activate size↑↓, pos↑↓←→ buttons
                //activate "start" button
                loadingCanvas.SetActive(false);
                setUpCanvas.SetActive(true);
                gamePlayCanvas.SetActive(false);
                gameOverCanvas.SetActive(false);
                //activate setEnvironment script
                gameObject.GetComponent<SetEnvironment>().enabled = true;
                break;

            //play game
            case (2):
                //instantiate chicken within the play area
                ground = GameObject.FindGameObjectWithTag("Ground");
                chickenHeight = chick.GetComponent<Collider>().bounds.size.y * SetEnvironment.scaleChange * .25f;
                chickenHouse = GameObject.FindGameObjectWithTag("ChickenDetector");

                for (int i = 0; i < nChicken; i++)
                {
                    if (!chicken[i])
                    {
                        //-2~2
                        //was 33
                        Vector2 rCircle = Random.insideUnitCircle * 30 * fence.transform.localScale.x;
                        Vector3 pos = new Vector3(rCircle.x + fence.transform.position.x, chickenHeight + ground.transform.position.y, rCircle.y + fence.transform.position.z);
                        Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                        chicken[i] = Instantiate(chick, pos, rot) as GameObject;
                    }
                    else
                    {
                        //remember to change the multiplier and the number if the ratio between the two scales changes
                        //0.015f
                        //if(chicken[i].transform.localScale.x != 1 + SetEnvironment.scaleChange * .25f)
                        //{
                        //    chicken[i].transform.localScale += new Vector3(SetEnvironment.scaleChange * .25f, SetEnvironment.scaleChange * .25f, SetEnvironment.scaleChange * .25f);
                        //}
                        chicken[i].transform.localScale = new Vector3(1 + SetEnvironment.scaleChange / 12 * 200, 1 + SetEnvironment.scaleChange / 12 * 200, 1 + SetEnvironment.scaleChange / 12 * 200);

                    }

                }

                if (!chicken[nChicken - 1])
                {
                    chickenHouse.GetComponent<Collider>().isTrigger = false;
                }
                else
                {
                    chickenHouse.GetComponent<Collider>().isTrigger = true;
                }

                //disable setEnvironment script
                gameObject.GetComponent<SetEnvironment>().enabled = false;
                //activate "open/close door", "help", "pause" (which includes "exit" and volume slider)
                //activate chicken counter and timer
                loadingCanvas.SetActive(false);
                setUpCanvas.SetActive(false);
                gamePlayCanvas.SetActive(true);
                gameOverCanvas.SetActive(false);
                //change button sprite
                if (FenceDoorController.open)
                {
                    openIcon.SetActive(false);
                    closeIcon.SetActive(true);
                }
                else
                {
                    openIcon.SetActive(true);
                    closeIcon.SetActive(false);
                }

                //start timer
                t += Time.deltaTime;
                seconds = (int)(t % 60);
                minutes = (int)(t / 60);
                if (seconds < 10)
                {
                    timerText.text = minutes + " : 0" + seconds;
                }
                else
                {
                    timerText.text = minutes + " : " + seconds;
                }
                //chicken counter text
                chickenText.text = chickenCounter + " / " + nChicken;
                break;

            //game over
            case (3):
                //activate "restart", "back to title"
                //activate rank panel
                loadingCanvas.SetActive(false);
                setUpCanvas.SetActive(false);
                gamePlayCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                //display time
                if (seconds < 10)
                {
                    GameOverTime.text = minutes + " : 0" + seconds;
                }
                else
                {
                    GameOverTime.text = minutes + " : " + seconds;
                }
                //activate celebrating particle system

                break;
        }
    }
}
