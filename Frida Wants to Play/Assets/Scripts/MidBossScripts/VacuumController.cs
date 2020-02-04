using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public float ringSpeed;
    public int HP;

    float timer;
    bool fired, returned, secondReturned, detected, deleted;
    GameObject[] rings;
    GameObject[] secondRings;
    Color ash;

    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        rings = new GameObject[4];
        secondRings = new GameObject[3];
        timer = 0;
        returned = false;
        secondReturned = false;
        detected = false;
        deleted = false;
        ash = new Color(150, 150, 150);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HP--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        if (fired == false)
        {
            StartCoroutine("Rings");
        }
        if (returned == false)
        {
            for (int i = 0; i < rings.Length; i++)
            {
                if (timer >= 2.5)
                {
                    if (rings[i])
                    {
                        rings[i].GetComponent<Rigidbody2D>().velocity = -rings[i].GetComponent<Rigidbody2D>().velocity;
                        returned = true;
                    }
                }
            }
        }

        if (rings[2])
        {
            if (rings[2].transform.position.x > transform.position.x && deleted == false)
            {
                for(int i = 0; i < rings.Length; i++)
                {
                    Destroy(rings[i]);
                }
                for(int i = 0; i < secondRings.Length; i++)
                {
                    Destroy(secondRings[i]);
                }
                deleted = true;
            }
        }

        if (!GameObject.FindGameObjectWithTag("EnemyBullet") && detected == false)
        {
            fired = false;
            detected = true;
        }

        if (secondReturned == false)
        {
            for (int i = 0; i < secondRings.Length; i++)
            {
                if (timer >= 2.8)
                {
                    if (secondRings[i])
                    {
                        secondRings[i].GetComponent<Rigidbody2D>().velocity = -secondRings[i].GetComponent<Rigidbody2D>().velocity;
                        secondReturned = true;
                    }
                }
            }
        }
    }

    IEnumerator Rings()
    {
        fired = true;
        returned = false;
        secondReturned = false;
        for (int i = 0; i < rings.Length; i++)
        {
            rings[i] = Instantiate(Resources.Load("RingBullet"), transform.position, Quaternion.identity) as GameObject;
            rings[i].GetComponent<SpriteRenderer>().color = ash;
            rings[i].transform.eulerAngles = new Vector3(0, 0, (i + 1) * 36);
            rings[i].GetComponent<Rigidbody2D>().velocity = rings[i].transform.up * ringSpeed;
        }
        yield return new WaitForSeconds(.3f);
        for(int i = 0; i < secondRings.Length; i++)
        {
            secondRings[i]= Instantiate(Resources.Load("RingBullet"), transform.position, Quaternion.identity) as GameObject;
            secondRings[i].GetComponent<SpriteRenderer>().color = ash;
            secondRings[i].transform.eulerAngles = new Vector3(0, 0, (i + 1) * 45);
            secondRings[i].GetComponent<Rigidbody2D>().velocity = secondRings[i].transform.up * ringSpeed;
        }
        timer = 0;
        deleted = false;
        detected = false;
    }
}
