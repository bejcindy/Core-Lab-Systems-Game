using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    Slider steminaSlider;
    GameObject sun;
    public GameObject timer1, timer2, timer3;
    public static float sunRotation;
    //GameObject[] rangerStat, youtuberStat;
    Text trapWarning, camWarning;

    // Start is called before the first frame update
    void Start()
    {
        trapWarning = GameObject.Find("TrapWarning").GetComponent<Text>();
        camWarning = GameObject.Find("CamWarning").GetComponent<Text>();
        steminaSlider = GameObject.Find("SteminaSlider").GetComponent<Slider>();
        sun = GameObject.Find("Sun");
    }

    // Update is called once per frame
    void Update()
    {
        steminaSlider.value = CreatureController.stemina;

        if (YoutuberController.trapped || CreatureController.trapped)
        {
            trapWarning.text = "Trap is triggered";
        }
        else
        {
            trapWarning.text = "";
        }
        if (YoutuberCamController.close)
        {
            camWarning.text = "Camera sees some thing";
        }
        else
        {
            camWarning.text = "";
        }

        if (sun.transform.localRotation.eulerAngles.x > 180)
        {
            sunRotation = sun.transform.localRotation.eulerAngles.x - 360;
        }
        else
        {
            sunRotation = sun.transform.localRotation.eulerAngles.x;
        }
        //Debug.Log(sunRotation);

        Vector3 temp = new Vector3(0, 0, -(sunRotation + 90));
        timer1.transform.eulerAngles = temp;
        timer2.transform.eulerAngles = temp;
        timer3.transform.eulerAngles = temp;
    }
}
