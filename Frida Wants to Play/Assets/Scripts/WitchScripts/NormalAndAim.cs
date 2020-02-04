using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAndAim : MonoBehaviour
{
    public int ringWaves;
    public int nTriangles;
    public int rotation;
    public float ringSpeed, triangleSpeed;
    public float ringDelay, triangleDelay, waveDelay;
    public float predict;
    public Transform firePoint;

    GameObject[] rings;
    GameObject[] triangles;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Ring", 0, waveDelay);
        InvokeRepeating("Aim", 0, waveDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Aim()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        triangles = new GameObject[nTriangles];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = Instantiate(Resources.Load("TriangleBullet"), firePoint.position, Quaternion.identity) as GameObject;
            triangles[i].GetComponent<SpriteRenderer>().color = new Color(.46f, .95f, 1f);
            StartCoroutine(AimShot(i));
        }
    }

    IEnumerator AimShot(int a)
    {
        yield return new WaitForSeconds(a * triangleDelay);

        if (player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            triangles[a].transform.up = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - firePoint.position;
        }
        else
        {
            triangles[a].transform.up = new Vector3(player.transform.position.x, player.transform.position.y + predict * Mathf.Sign(player.GetComponent<Rigidbody2D>().velocity.y), player.transform.position.z) - firePoint.position;
        }
        triangles[a].GetComponent<Rigidbody2D>().velocity = triangles[a].transform.rotation * transform.up * triangleSpeed;
    }

    void Ring()
    {
        rings = new GameObject[ringWaves * 3];
        for (int i = 0; i < rings.Length; i++)
        {
            rings[i] = Instantiate(Resources.Load("RingBullet"), firePoint.position, Quaternion.identity) as GameObject;
            rings[i].GetComponent<SpriteRenderer>().color = new Color(.46f, .95f, 1f);
            Debug.Log(rings[i].GetComponent<SpriteRenderer>().color);
            if (i % 3 == 0)
            {
                rings[i].transform.Rotate(0, 0, 90 - rotation);
            }
            if (i % 3 == 1)
            {
                rings[i].transform.Rotate(0, 0, 90);
            }
            if (i % 3 == 2)
            {
                rings[i].transform.Rotate(0, 0, 90 + rotation);
            }

            StartCoroutine(ShootRing(i / 3, i));
        }
    }

    IEnumerator ShootRing(int a, int b)
    {
        yield return new WaitForSeconds(a * ringDelay);
        rings[b].GetComponent<Rigidbody2D>().velocity = rings[b].transform.rotation * transform.up * ringSpeed;
    }
}
