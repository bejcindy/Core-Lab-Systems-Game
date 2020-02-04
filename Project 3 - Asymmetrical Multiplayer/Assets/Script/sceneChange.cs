using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneChange : MonoBehaviour
{
    public Text cam1, cam2, cam3;
    void Start()
    {
        if (PublicVars.creatureWin)
        {
            cam1.text = "creature wins !";
            cam2.text = "creature wins !";
            cam3.text = "creature wins !";
        }
        if (PublicVars.rangerWin)
        {
            cam1.text = "ranger wins !";
            cam2.text = "ranger wins !";
            cam3.text = "ranger wins !";
        }
        if (PublicVars.youtuberWin)
        {
            cam1.text = "youtuber wins !";
            cam2.text = "youtuber wins !";
            cam3.text = "youtuber wins !";
        }
    }

    public string sceneName;

    void Update()
    {
        if(Input.GetButtonDown("Submit1") || Input.GetButtonDown("Submit2") || Input.GetButtonDown("Submit3"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    
}
