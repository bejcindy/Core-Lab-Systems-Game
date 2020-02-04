using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour
{
    public float moveSpeed;
    public float maxDist, minDist;
    public float rotateSpeed;
    public float laserSpeed;
    public float calmTime;
    public int HP;

    GameObject laser;
    GameObject Frida;
    int behave;
    Rigidbody2D rb;
    float moveDist;
    float timer, startTime;
    bool fired;

    // Start is called before the first frame update
    void Start()
    {
        behave = 0;
        rb = GetComponent<Rigidbody2D>();
        moveDist = Random.Range(minDist, maxDist);
        Frida = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fired);
        timer += Time.deltaTime;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        //Debug.Log("moveDist"+moveDist +"actualDifference"+ (8 - transform.position.x)+"behave"+behave);
        if (Mathf.Abs(8-transform.position.x) >= moveDist && behave == 0)
        {
            behave = 1;
        }
        if (behave == 2 && fired == false && (timer-startTime)>=calmTime)
        {
            behave = 1;
        }
        if (fired == true)
        {
            behave = 2;
        }
        Behavior();
    }

    void Behavior()
    {
        switch (behave)
        {
            case 0:
                //move into screen
                rb.velocity = Vector2.left * moveSpeed;
                break;
            case 1:
                if (Frida)
                {
                    rb.velocity = Vector2.zero;
                    Vector2 direction = transform.position - Frida.transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    laser = Instantiate(Resources.Load("Beam"), transform.position, transform.rotation) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = -laser.transform.right * laserSpeed;
                    fired = true;
                    startTime = timer;
                }
                break;
            case 2:
                fired = false;
                break;
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

}
