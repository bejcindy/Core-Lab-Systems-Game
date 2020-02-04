using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    AudioSource aud;
    AudioClip normal, boss;
    bool normaled, bossed;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        normal = Resources.Load("Sounds/NormalBGM") as AudioClip;
        boss = Resources.Load("Sounds/BossBGM") as AudioClip;
        normaled = false;
        bossed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.levels==0 && normaled == false)
        {
            Debug.Log("Here");
            StartCoroutine("NormalBGM");
        }
        if (GameManagement.levels == 5 && bossed == false)
        {
            StartCoroutine("BossBGM");
        }
    }

    IEnumerator NormalBGM()
    {
        normaled = true;
        aud.clip = normal;
        aud.Play();
        yield return null;
    }

    IEnumerator BossBGM()
    {
        bossed = true;
        aud.clip = boss;
        aud.Play();
        yield return null;
    }

}
