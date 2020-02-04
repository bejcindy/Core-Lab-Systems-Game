using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerEnter : MonoBehaviour
{
    public static bool showerMoving;

    // Start is called before the first frame update
    void Start()
    {
        showerMoving = true;
        GetComponent<ShowerController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = 3 * Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= 6.5)
        {
            GetComponent<ShowerController>().enabled = true;
            showerMoving = false;
        }
    }
}
