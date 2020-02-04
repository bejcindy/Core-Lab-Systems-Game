using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMover : MonoBehaviour
{
    public float s;
    float x, y;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector2.left * s;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //reduce LP
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
