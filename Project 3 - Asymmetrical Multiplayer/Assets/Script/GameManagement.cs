using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public int nMushrooms;
    public LayerMask mask = -1;
    GameObject[] mushrooms;
    // Start is called before the first frame update
    void Start()
    {
        PublicVars.rangerWin = false;
        PublicVars.creatureWin = false;
        PublicVars.youtuberWin = false;
        //for (int i = 0; i < Display.displays.Length; i++)
        //{
        //    Display.displays[i].Activate();
        //}
        mushrooms = new GameObject[nMushrooms];
        for (int i = 0; i < mushrooms.Length; i++)
        {
            //z:90~880
            Vector3 pos = new Vector3(Random.Range(90, 880), 200, Random.Range(90, 880));
            mushrooms[i] = Instantiate(Resources.Load("mushroom"), pos, Quaternion.identity) as GameObject;
            RaycastHit hit;
            Ray ray = new Ray(mushrooms[i].transform.position, Vector3.down);
            Debug.DrawRay(mushrooms[i].transform.position, Vector3.down, Color.red);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.Log("did");
                if (hit.collider.gameObject.name == "Terrain")
                {
                    mushrooms[i].transform.position = new Vector3(mushrooms[i].transform.position.x, hit.point.y, mushrooms[i].transform.position.z);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (RangerController.photo >= 5)
        {
            PublicVars.rangerWin = true;
            SceneManager.LoadScene("EndScreen-deer");
        }
        if (YoutuberController.photo >= 5)
        {
            PublicVars.youtuberWin = true;
            SceneManager.LoadScene("EndScreen-deer");
        }
        if (YoutuberController.photo < 5 && RangerController.photo < 5 && CanvasController.sunRotation >= 89)
        {
            PublicVars.creatureWin = true;
            SceneManager.LoadScene("EndScreen-deer");
        }
    }
}
