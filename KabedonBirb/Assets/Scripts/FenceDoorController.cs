using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FenceDoorController : MonoBehaviour
{
    public static bool open;
    //public Text buttonText;

    Text buttonText;
    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        a.SetBool("isOpen", open);
    }
}
