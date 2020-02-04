using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x <= 89f || transform.rotation.eulerAngles.x >= 91)
        {
            transform.RotateAround(new Vector3(480, 0, 440), Vector3.right, rotateSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(480, 0, 440));
        }
        //transform.RotateAround(new Vector3(480, 0, 440), Vector3.right, rotateSpeed * Time.deltaTime);
        //transform.LookAt(new Vector3(480, 0, 440));
    }
}
