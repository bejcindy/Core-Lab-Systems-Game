using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenBehavior : MonoBehaviour
{
    //public Transform player;
    //private static bool panic = false;
    public static Vector3 forceDirection;
    //Slider detectDist, escapeSpeed;
    public static float dist;
    public static float escapeSpeed;
    public static float detectDist;

    public AudioClip chick1, chick2, chick3, wing;

    AudioSource aud;
    Rigidbody rb;
    Vector3 playerNoY, posNoY;
    Vector3 randomNoY;
    Vector3 red;
    Quaternion redirection;
    Quaternion rRotation;
    GameObject player;
    Transform playerTransform;
    Animator anim;
    bool falsing, stopping;
    bool moving;
    bool sounding, winged, fwinged;
    bool isPanic, isWondering, isRandom, redirecting;
    int state;
    float escapeWallSpeed;

    //public float thrust;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        escapeSpeed = 1.75f + SetEnvironment.scaleChange * 175 / 6;
        detectDist = .75f + SetEnvironment.scaleChange * 25 / 2;
        state = 2;
        isPanic = false;
        isWondering = false;
        isRandom = false;
        redirecting = false;
        falsing = false;
        sounding = false;
        stopping = false;
        winged = false;
        fwinged = false;
        escapeWallSpeed = 0;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("state: " + state + " isWondering: " + isWondering + " isRandom: " + isRandom);
        playerNoY = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        posNoY = new Vector3(transform.position.x, 0, transform.position.z);
        dist = Vector3.Distance(playerNoY, posNoY);

        if (rb.velocity == Vector3.zero)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        anim.SetBool("moving", moving);
        anim.SetBool("escaping", isPanic);

        if (dist <= detectDist)
        {
            state = 1;
        }
        else
        {
            if (redirecting)
            {
                state = 0;
            }
            else
            {
                state = 2;
            }
        }
        if (state == 0 || state == 2)
        {
            if (!sounding)
            {
                StartCoroutine(WonderSound());
            }
        }
        if (state == 1 && !winged)
        {
            aud.PlayOneShot(wing);
            winged = true;
        }
        Behavior();
    }
   

    private void OnCollisionEnter(Collision collision)
    {
        //if (!falsing)
        //{
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Chicken"))
            {
                redirecting = true;
                redirection = Quaternion.Euler(0, -transform.rotation.y, 0);
                //StartCoroutine(SetFalse());
            }
        //}
    }

    void Behavior()
    {
        switch (state)
        {
            case (0):
                //exit other states
                StopCoroutine(ChickenWondering());
                StopCoroutine(StopWalking());
                isPanic = false;
                isWondering = false;
                isRandom = false;
                falsing = false;
                stopping = false;
                if (!fwinged)
                {
                    StartCoroutine(FalseWinged());
                }

                //avoid running into walls
                if (Quaternion.Angle(transform.rotation, redirection) > 0.01f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, redirection, Time.deltaTime * 5);
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    //change speed later to the previous speed of the chick
                    rb.velocity = transform.forward * .5f * escapeSpeed;
                    if (!falsing)
                    {
                        StartCoroutine(SetFalse());
                    }
                }
                
                break;

            case (1):
                //exit other state
                StopCoroutine(ChickenWondering());
                StopCoroutine(StopWalking());
                isRandom = false;
                isWondering = false;
                isPanic = true;
                falsing = false;
                stopping = false;
                redirecting = false;
                
                //if chased, run in opposite direction
                forceDirection = (posNoY - playerNoY).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(forceDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
                //maybe need to change to forceDirection
                rb.velocity = transform.forward * escapeSpeed;
                escapeWallSpeed = escapeSpeed;
                break;

            case (2):
                //wonder around if nothing's happening
                isPanic = false;
                if (!fwinged)
                {
                    StartCoroutine(FalseWinged());
                }
                if (!isWondering)
                {
                    StartCoroutine(ChickenWondering());
                }
                else
                {
                    if (isRandom)
                    {
                        //if (transform.rotation != rRotation)
                        if(Quaternion.Angle(transform.rotation, rRotation) > 0.01f)
                        {
                            transform.rotation = Quaternion.Lerp(transform.rotation, rRotation, Time.deltaTime * 5);
                            rb.velocity = Vector3.zero;
                        }
                        else
                        {
                            //if reach rotation, then start walking
                            rb.velocity = randomNoY.normalized * .5f * escapeSpeed;
                            escapeWallSpeed = .5f * escapeSpeed;
                            if (!stopping)
                            {
                                StartCoroutine(StopWalking());
                            }
                        }
                    }
                }
                break;
        }
    }

    IEnumerator WonderSound()
    {
        sounding = true;
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            aud.clip = chick1;
        }else if (r == 2)
        {
            aud.clip = chick2;
        }
        else
        {
            aud.clip = chick3;
        }
        aud.Play();
        yield return new WaitForSeconds(Random.Range(2, 4));
        sounding = false;
    }

    IEnumerator FalseWinged()
    {
        fwinged = true;
        yield return new WaitForSeconds(2);
        winged = false;
        fwinged = false;
    }

    IEnumerator SetFalse()
    {
        falsing = true;
        yield return new WaitForSeconds(1.5f);
        redirecting = false;
        falsing = false;
    }

    IEnumerator ChickenWondering()
    {
        isWondering = true;
        //stay for a while
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(1, 2));
        //turn around
        Vector2 randomR = Random.insideUnitCircle;
        randomNoY = new Vector3(randomR.x, 0, randomR.y);
        rRotation = Quaternion.LookRotation(randomNoY, Vector3.up);
        isRandom = true;
        //walk is included in SetFalse
    }
    IEnumerator StopWalking()
    {
        stopping = true;
        yield return new WaitForSeconds(Random.Range(1, 3));
        //time for walking
        isRandom = false;
        isWondering = false;
        stopping = false;
    }
}
