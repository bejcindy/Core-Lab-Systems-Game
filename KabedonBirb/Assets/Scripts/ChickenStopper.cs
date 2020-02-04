using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStopper : MonoBehaviour
{
    public static int score;
    //public static bool entered;

    bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            //entered = true;
            if (!stopped && other.GetComponent<ChickenBehavior>().enabled == true)
            {
                StartCoroutine("StopChicken", other);
            }
        }
    }
    IEnumerator StopChicken(GameObject chicken)
    {
        stopped = true;
        score++;
        yield return new WaitForSeconds(1.5f);
        chicken.GetComponent<ChickenBehavior>().enabled = false;
        chicken.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stopped = false;
    }
}
