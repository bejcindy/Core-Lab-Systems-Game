using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject cam;
    //public GameObject HPLeft, HPMid, HPRight;
    public GameObject[] HP;

    float fireRate = .15f;
    public static int LP;
    SpriteRenderer sr;
    Camera camer;
    Collider2D c;
    Rigidbody2D rb;
    AudioClip hurt, die;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        LP = 3;
        HP[0].SetActive(true);
        HP[1].SetActive(true);
        HP[2].SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        camer = cam.GetComponent<Camera>();
        hurt = Resources.Load("Sounds/HurtMeow") as AudioClip;
        die = Resources.Load("Sounds/LoseMeow") as AudioClip;
        aud = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", 0, fireRate);
        //InvokeRepeating("name", delay of calling the function, rate of calling the function);
    }

    void Shoot()
    {
        //Input.GetButtonDown("Jump")
        //最好能改成一直按着的时候每隔0.几秒发射
        if (Input.GetKey(KeyCode.Space) && WitchController.witchMoving == false && ShowerEnter.showerMoving == false && VaccumEnter.vaccumMoving == false || Input.GetKey(KeyCode.Joystick1Button1) && WitchController.witchMoving == false && ShowerEnter.showerMoving == false && VaccumEnter.vaccumMoving == false)
        {
            Instantiate(Resources.Load("Bullet"), transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);

        Vector3 bottomLeft = camer.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = camer.ScreenToWorldPoint(new Vector3(
            camer.pixelWidth, camer.pixelHeight));

        Rect cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin + c.bounds.size.x / 2, cameraRect.xMax - c.bounds.size.x / 2),
            Mathf.Clamp(transform.position.y, cameraRect.yMin + c.bounds.size.y / 2, cameraRect.yMax - c.bounds.size.y / 2), transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            LP -= 1;
            if (LP > 0)
            {
                aud.PlayOneShot(hurt);
            }
            Destroy(collision.gameObject);

            Hurt();
        }
    }

    void Hurt()
    {
        HP[LP].SetActive(false);

        if (LP == 0)
        {
                Destroy(gameObject);
        }

        StopAllCoroutines();
        sr.color = new Color(255, 255, 255, 1);
        c.enabled = true;

        //HPMid.SetActive(false);
        StartCoroutine("NotCollide");
        //StartCoroutine("Blinkk");
        StartCoroutine("Blink");
        print("LP" + LP);
    }

    IEnumerator NotCollide()
    {
        c.enabled = false;
        yield return new WaitForSeconds(.5f);
        c.enabled = true;
        yield break;
    }

    IEnumerator Blink()
    {
        for(int i =0; i<3; i++)
        {
            sr.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(.1f);
            sr.color = new Color(255, 255, 255, 1);
            yield return new WaitForSeconds(.1f);
        }
    }
}
