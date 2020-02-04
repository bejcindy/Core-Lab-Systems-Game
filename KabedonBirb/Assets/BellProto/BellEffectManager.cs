using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellEffectManager : MonoBehaviour
{
    public GameObject BellPS;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BellPresed() {

        GameObject bellEffect = Instantiate(BellPS, transform.position, Quaternion.identity);
        bellEffect.GetComponent<ParticleSystem>().Play();
    }
}
