using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextController : MonoBehaviour
{
    Text t;
    bool called;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        called = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (called == false)
        {
            StartCoroutine(ChangeText());
        }
    }

    IEnumerator ChangeText()
    {
        called = true;
        t.text = "Detecting Plane";
        yield return new WaitForSeconds(.5f);
        t.text = "Detecting Plane.";
        yield return new WaitForSeconds(.5f);
        t.text = "Detecting Plane..";
        yield return new WaitForSeconds(.5f);
        t.text = "Detecting Plane...";
        yield return new WaitForSeconds(.5f);
        called = false;
    }
}
