using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugValues : MonoBehaviour
{
    public Text t, value1, value2, t2, t3;
    //public Slider slider1, slider2;
    //public Transform cam;

    Transform chicken;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Player").transform;
        //chicken = GameObject.FindGameObjectWithTag("Chicken").transform;
    }

    // Update is called once per frame
    void Update()
    {
        chicken = GameObject.FindGameObjectWithTag("Chicken").transform;
        //t.text = "x " + cam.position.x + ", y " + cam.position.y + ", z " + cam.position.z;
        //t2.text = "x " + chicken.position.x + ", y " + chicken.position.y + ", z " + chicken.position.z;
        //t.text = "x " + SetEnvironment.groundpos.x + ", y " + SetEnvironment.groundpos.y + ", z " + SetEnvironment.groundpos.z;
        //t.text = "x " + chicken.position.x + ", y " + chicken.position.y + ", z " + chicken.position.z;
        //t.text = "distance: " + ChickenBehavior.dist;
        //t.text= "x " + ChickenBehavior.forceDirection.x + ", y " + ChickenBehavior.forceDirection.y + ", z " + ChickenBehavior.forceDirection.z;
        //t.text = "Detect Distance = " + ChickenBehavior.detectDist;
        t.text = "moveforward = " + SetEnvironment.moveForward;
        //t2.text = "Escape Speed = " + ChickenBehavior.escapeSpeed;
        t2.text = "x " + SetEnvironment.fencepos.x + " y " + SetEnvironment.fencepos.y + " z " + SetEnvironment.fencepos.z;
        t3.text = "Score = " + ChickenStopper.score;
        //value1.text = "detect range: " + slider1.value;
        //value2.text = "escape speed: " + slider2.value;
    }
}
