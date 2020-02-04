using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        //offset = transform.position - player.position;
        offset = new Vector3(0, 6, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        transform.LookAt(player.rotation.eulerAngles);
        transform.rotation = player.rotation;
    }
}
