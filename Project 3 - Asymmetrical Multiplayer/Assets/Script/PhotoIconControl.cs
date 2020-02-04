using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoIconControl : MonoBehaviour
{
    public Transform rangerStat, youtuberStat;

    GameObject[] rangerPhoto, youtuberPhoto;
    Color semiTrans, normal;

    // Start is called before the first frame update
    void Start()
    {
        semiTrans = new Color(255, 255, 255, .25f);
        normal = new Color(255, 255, 255, 255);
        rangerPhoto = new GameObject[5];
        youtuberPhoto = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            rangerPhoto[i] = rangerStat.GetChild(i).gameObject;
            youtuberPhoto[i] = youtuberStat.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i + 1 > RangerController.photo)
            {
                rangerPhoto[i].GetComponent<Image>().color = semiTrans;
            }
            else
            {
                rangerPhoto[i].GetComponent<Image>().color = normal;
            }
            if (i + 1 > YoutuberController.photo)
            {
                youtuberPhoto[i].GetComponent<Image>().color = semiTrans;
            }
            else
            {
                youtuberPhoto[i].GetComponent<Image>().color = normal;
            }
        }
    }
}
