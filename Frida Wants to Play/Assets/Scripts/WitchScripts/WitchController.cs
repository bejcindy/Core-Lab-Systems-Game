using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{
    public int HP;
    public static bool witchMoving;

    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        witchMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 4.5)
        {
            GetComponent<NormalAndAim>().enabled = false;
            witchMoving = true;
            col.enabled = false;
            rb.velocity = 2 * Vector2.left;
        }
        else
        {
            GetComponent<NormalAndAim>().enabled = true;
            witchMoving = false;
            col.enabled = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet") && HP > 0)
        {
            HP--;
            Destroy(collision.gameObject);
            Debug.Log(HP);
            //sr.color = new Color(255, 255, 255, .6f);
        }
    }
}
