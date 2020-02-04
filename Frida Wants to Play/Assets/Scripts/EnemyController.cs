using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float hSpeed, vSpeed;
    public 

    GameObject player;
    Collider2D col;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Attack()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 destination = transform.position - playerPos;
        rb.velocity = new Vector2(hSpeed * destination.x, vSpeed * destination.y);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
