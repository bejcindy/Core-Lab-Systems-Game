using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimation : MonoBehaviour
{
    public Animator ChickenAnim;
    // Start is called before the first frame update
    void Start()
    {
        ChickenAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Chicken not moving){
                //ChickenAnim.Play("idle1",-1,0f);
            //}
    }
}
