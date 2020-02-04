using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkWhenAttacked : MonoBehaviour
{
    SpriteRenderer sr;
    Color opaque, transparent;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        opaque = new Color(255, 255, 255, 1);
        transparent = new Color(255, 255, 255, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            StartCoroutine("Blink");
        }
    }
    IEnumerator Blink()
    {
        sr.color = transparent;
        yield return new WaitForSeconds(.1f);
        sr.color = opaque;
    }
}
