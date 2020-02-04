﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 4f;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);
    }
}
