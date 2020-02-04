using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float r, speed;

    Rigidbody2D rb;
    int HP;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HP = 2;
        transform.Rotate(0, 0, Mathf.Sign(transform.position.y) * r);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * speed;
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HP--;
        }
        if (collision.CompareTag("Boundary"))
        {
            Debug.Log(transform.eulerAngles.z);
            transform.Rotate(0, 0, -transform.eulerAngles.z * 2);
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
