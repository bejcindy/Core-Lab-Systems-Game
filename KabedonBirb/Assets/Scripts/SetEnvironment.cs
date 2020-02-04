using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnvironment : MonoBehaviour
{
    
    public static float scaleChange;
    public static bool moveForward, moveBackward, moveLeft, moveRight, rotateLeft, rotateRight, sizeUp, sizeDown;
    public static Vector3 fencepos;

    public GameObject Player;
    public GameObject AreaSettingSphere;

    GameObject[] planes;
    GameObject chickenHouse;
    GameObject ground;
    float minY = 0;
    float xPos, zPos;
    float sizeGroth = .01f, posGroth = .025f;
    float waitSeconds = .1f;
    bool called;
    Vector3 pos;
    //float timer = 0;
    

    //public float detectTime;
    // Start is called before the first frame update
    void Start()
    {
        AreaSettingSphere.SetActive(false);
        scaleChange = 0;
    }

    // Update is called once per frame
    void Update()
    {
        planes = GameObject.FindGameObjectsWithTag("Plane");

        if (planes.Length > 0)
        {
            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].transform.position.y < minY)
                {
                    minY = planes[i].transform.position.y;
                    xPos = planes[i].transform.position.x;
                    zPos = planes[i].transform.position.z;
                    pos = new Vector3(xPos, minY, zPos);
                }
            }

            if (!ground)
            {
                ground = Instantiate(Resources.Load("GroundPlane"), pos, Quaternion.identity) as GameObject;
            }
            else
            {
                ground.transform.position = pos;
                //AreaSettingSphere.transform.position = new Vector3(AreaSettingSphere.transform.position.x, ground.transform.position.y, AreaSettingSphere.transform.position.z);
                //groundpos = ground.transform.position;
                AreaSettingSphere.SetActive(true);
                if (!chickenHouse)
                {
                    Vector3 pos = new Vector3(AreaSettingSphere.transform.position.x, ground.transform.position.y, AreaSettingSphere.transform.position.z);
                    chickenHouse = Instantiate(Resources.Load("chickenfence"), pos, Quaternion.identity) as GameObject;
                }
                chickenHouse.transform.position = new Vector3(AreaSettingSphere.transform.position.x, ground.transform.position.y, AreaSettingSphere.transform.position.z);
                //fencepos = AreaSettingSphere.transform.position;
            }
        }
        if (!called)
        {
            if (moveForward)
            {
                StartCoroutine(MoveFB(1));
            }
            if (moveBackward)
            {
                StartCoroutine(MoveFB(-1));
            }
            if (moveLeft)
            {
                StartCoroutine(MoveLR(-1));
            }
            if (moveRight)
            {
                StartCoroutine(MoveLR(1));
            }
            if (rotateLeft)
            {
                StartCoroutine(Rotate(-1));
            }
            if (rotateRight)
            {
                StartCoroutine(Rotate(1));
            }
            if (sizeUp)
            {
                StartCoroutine(Scale(sizeGroth));
            }
            if (sizeDown)
            {
                StartCoroutine(Scale(-sizeGroth));
            }
        }
    }

    //IEnumerator Move(float x, float z)
    //{
    //    called = true;
    //    AreaSettingSphere.transform.position += new Vector3(x, 0, z);
    //    yield return new WaitForSeconds(waitSeconds);
    //    called = false;
    //}

    IEnumerator MoveFB(float d)
    {
        called = true;
        AreaSettingSphere.transform.position += Player.transform.forward * posGroth * d;
        yield return new WaitForSeconds(waitSeconds);
        called = false;
    }

    IEnumerator MoveLR(float d)
    {
        called = true;
        AreaSettingSphere.transform.position += Player.transform.right * posGroth * d;
        yield return new WaitForSeconds(waitSeconds);
        called = false;
    }

    IEnumerator Rotate(float speed)
    {
        called = true;
        AreaSettingSphere.transform.Rotate(0, speed, 0);
        yield return new WaitForSeconds(waitSeconds);
        called = false;
    }

    IEnumerator Scale(float s)
    {
        called = true;
        AreaSettingSphere.transform.localScale += new Vector3(s, s, s);
        //remember to change the multiplier if the ratio between the two scales changes;
        chickenHouse.transform.localScale += new Vector3(s * 5 / 6, s * 5 / 6, s * 5 / 6);
        scaleChange += s;
        yield return new WaitForSeconds(waitSeconds);
        called = false;
    }
}
