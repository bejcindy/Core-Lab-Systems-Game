using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    //public GameObject[] chickenTypes;
    public GameObject chicken1;
    public GameObject chicken2;
    public GameObject chicken3;
    public GameObject chicken4;
    public GameObject chicken5;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;

    //public GameObject[] spawnPoints;
    //private GameObject currentPoint;

    //private int index;
    //private int chickenNumber;

    void Start()
    {
        //index = Random.Range(0, spawnPoints.Length);
        //currentPoint = spawnPoints[index];

        //spawnPoints = new Vector3(spawnPoints.x, spawnPoints.y);
        //chickenTypes = Instantiate(chickenTypes, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;

        Instantiate(chicken1, spawnPoint1.position, spawnPoint1.rotation);
        Instantiate(chicken2, spawnPoint2.position, spawnPoint2.rotation);
        Instantiate(chicken3, spawnPoint3.position, spawnPoint3.rotation);
        Instantiate(chicken4, spawnPoint4.position, spawnPoint4.rotation);
        Instantiate(chicken5, spawnPoint5.position, spawnPoint5.rotation);

    }

    void Update()
    {

    }
}
