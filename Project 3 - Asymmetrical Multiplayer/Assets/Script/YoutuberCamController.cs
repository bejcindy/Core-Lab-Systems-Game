using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutuberCamController : MonoBehaviour
{
    GameObject creature, ranger;
    public float detectDist;
    public static bool close;

    // Start is called before the first frame update
    void Start()
    {
        creature = GameObject.Find("Creature");
        ranger = GameObject.Find("Ranger");
        close = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (creature && ranger)
        {
            float creatureDist = Vector3.Distance(transform.position, creature.transform.position);
            float rangerDist = Vector3.Distance(transform.position, ranger.transform.position);
            if (creatureDist <= detectDist || rangerDist <= detectDist)
            {
                //Debug.Log("something's close");
                close = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0);
            }
            else
            {
                close = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0);
            }
        }
    }
}
