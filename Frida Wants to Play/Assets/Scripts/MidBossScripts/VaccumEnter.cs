using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccumEnter : MonoBehaviour
{
    public static bool vaccumMoving;

    // Start is called before the first frame update
    void Start()
    {
        vaccumMoving = true;
        GetComponent<VacuumController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = 3 * Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<VacuumController>().enabled = true;
            vaccumMoving = false;
        }
    }
}
