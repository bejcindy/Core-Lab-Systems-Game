using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerController : MonoBehaviour
{
    public float moveSpeed, chaseSpeed;
    public float dropGap;
    public float dropSpeed;
    public float waitTime;
    public int HP;

    GameObject Frida;
    Rigidbody2D rb;
    bool dropped, beamed;
    int hitEdge;

    // Start is called before the first frame update
    void Start()
    {
        Frida = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        dropped = false;
        beamed = false;
        InvokeRepeating("LeftDrop", 0, dropGap);
        rb.velocity = Vector2.up * chaseSpeed;
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
        if (transform.position.y >= 4)
        {
            rb.velocity = Vector2.down * chaseSpeed;
        }
        if (transform.position.y <= -4)
        {
            rb.velocity = Vector2.up * chaseSpeed;
        }
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void LeftDrop()
    {
        float yOffset = Random.Range(-.5f, 0f);
        float y = Random.Range(0f, .5f);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + yOffset);
        Vector2 secondPos = new Vector2(transform.position.x, transform.position.y + y);
        GameObject drop = Instantiate(Resources.Load("Circle"), pos, Quaternion.identity) as GameObject;
        GameObject secondDrop = Instantiate(Resources.Load("Circle"), secondPos, Quaternion.identity) as GameObject;
        drop.transform.localScale = new Vector2(.2f, .2f);
        drop.GetComponent<Rigidbody2D>().velocity = dropSpeed * Vector2.left;
        secondDrop.transform.localScale = new Vector2(.2f, .2f);
        secondDrop.GetComponent<Rigidbody2D>().velocity = dropSpeed * Vector2.left;
    }
}
    
