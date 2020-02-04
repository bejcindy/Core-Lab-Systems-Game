using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAndBullet : MonoBehaviour
{
    public int nBeams;
    public int bulletWaves, nBullets, bulletSpeed;
    public float beamLength, beamSpeed;
    public float waveDelay, bulletDelay;
    public Transform firePoint;

    GameObject[] beams;
    GameObject[] bullets;
    int up;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Beam", 0, waveDelay);
        InvokeRepeating("Bullets", 0, waveDelay);
        up = 1;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Bullets()
    {
        bullets = new GameObject[bulletWaves * nBullets];
        for(int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(Resources.Load("Circle"), firePoint.position, Quaternion.identity) as GameObject;
            bullets[i].transform.Rotate(0, 0, -90 + (i % nBullets + 1) * 180 / (nBullets + 1));
            StartCoroutine(RotateBullets(i, i / nBullets));
        }
        up = -up;
    }

    IEnumerator RotateBullets(int a, int b)
    {
        yield return new WaitForSeconds(b * bulletDelay);
        bullets[a].transform.Rotate(0, 0, (-90 + (nBullets + 1) * 180 / (nBullets + 1)) / (bulletWaves-1) * (a / nBullets) * up);
        //Debug.Log((-90 + (nBullets + 1) * 180 / (nBullets + 1)) / (bulletWaves - 1) * (a / nBullets) * up);
        //Debug.Log(a);
        bullets[a].GetComponent<Rigidbody2D>().velocity = bullets[a].transform.rotation * -transform.right * bulletSpeed;
    }

    void Beam()
    {
        beams = new GameObject[nBeams];
        for(int i = 0; i < beams.Length; i++)
        {
            beams[i] = Instantiate(Resources.Load("Beam"), firePoint.position, Quaternion.identity) as GameObject;
            beams[i].transform.Rotate(0, 0, -90 + (i + 1) * 180 / (nBeams + 1));
            StartCoroutine(ShootBeam(i));
        }
    }

    IEnumerator ShootBeam(int a)
    {
        yield return new WaitForSeconds(beamLength);
        beams[a].GetComponent<Rigidbody2D>().velocity = beams[a].transform.rotation * -transform.right * beamSpeed;
    }

}
