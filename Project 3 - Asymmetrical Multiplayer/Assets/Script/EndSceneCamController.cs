using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneCamController : MonoBehaviour
{
    public Transform creature, ranger, youtuber;

    // Start is called before the first frame update
    void Start()
    {
        if (PublicVars.creatureWin)
        {
            transform.rotation = creature.rotation;
        }
        if (PublicVars.rangerWin)
        {
            transform.rotation = ranger.rotation;
        }
        if (PublicVars.youtuberWin)
        {
            transform.rotation = youtuber.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
