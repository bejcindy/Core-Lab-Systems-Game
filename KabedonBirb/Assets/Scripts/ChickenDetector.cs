using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDetector : MonoBehaviour
{
    public static bool ChickenIsDetected = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {

            ChickenIsDetected = true;
            Debug.Log("Chicken Detected!");

        }else {
            ChickenIsDetected = false;
        }
    }
}
