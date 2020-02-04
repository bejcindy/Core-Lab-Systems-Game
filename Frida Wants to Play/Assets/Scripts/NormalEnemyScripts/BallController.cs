using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int nBullets;
    public float calmTime;
    public float speed;
    public float forwardDist;
    public float HP;

    GameObject Frida;
    GameObject[] bullets;
    int chase;
    Rigidbody2D rb;
    float timer, startTime;
    Vector2 oldPos;
    float targetY;
    bool fired;

    // Start is called before the first frame update
    void Start()
    {
        Frida = GameObject.FindGameObjectWithTag("Player");
        bullets = new GameObject[nBullets];
        chase = 0;
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
        oldPos = transform.position;
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        targetY = Frida.transform.position.y;
        if (oldPos.x - transform.position.x >= forwardDist && chase == 0)
        {
            fired = false;
            oldPos = transform.position;
            chase = 1;
        }
        if (Mathf.Abs(transform.position.y - targetY) <= .1f && chase == 1)
        {
            startTime = timer;
            chase = 2;
        }
        if((timer - startTime) >= calmTime && chase == 2)
        {
            chase = 0;
        }
        Behavior();
    }

    void Behavior()
    {
        switch (chase)
        {
            case 0:
                rb.velocity = speed * Vector2.left;
                break;

            case 1:
                rb.velocity = speed * Vector2.down * Mathf.Sign(transform.position.y - targetY);
                break;
            case 2:
                rb.velocity = Vector2.zero;
                if (fired == false)
                {
                    StartCoroutine("FireBullets");
                }
                fired = true;
                break;
        }
    }

    IEnumerator FireBullets()
    {
        for (int i = 0; i < nBullets; i++)
        {
            yield return new WaitForSeconds(.2f);
            bullets[i] = Instantiate(Resources.Load("EnemyBullet"), transform.position, Quaternion.identity) as GameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HP--;
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

}
