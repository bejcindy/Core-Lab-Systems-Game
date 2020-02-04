using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterControl : MonoBehaviour
{
    public int playerNum;
    public float speed;
    public float rotateSpeed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal" + playerNum);
        float z = Input.GetAxis("Vertical" + playerNum);
        float rx = Input.GetAxis("RotateX" + playerNum);
        float rz = Input.GetAxis("RotateY" + playerNum);
        Vector3 look = new Vector3(rx, 0, rz);
        if (look.x != 0 && look.z != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(look, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        rb.velocity = new Vector3(x * speed, 0, z * speed);
    }
}
