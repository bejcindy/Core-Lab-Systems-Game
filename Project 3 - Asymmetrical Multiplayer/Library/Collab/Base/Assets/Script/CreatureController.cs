using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public static float timer1, timer2;
    //public int playerNum;
    int playerNum;
    Rigidbody rb;
    public float destroyCoolDown;
    public GameObject minimap;
    public float speed;
    public float rotateSpeed;
    public float rushDuration, rushSpeed;

    Renderer rend;
    public Material normal_Material;
    public Material hide_Material;
    public float hideTime;
    public float waitTime;

    bool isHiding = false;
    public static bool trapped;
    public static bool isRushing;
    
    public GameObject particle_Trail;


    public int mushroomStemina, hideStemina, rushStemina;
    public static int stemina;
    RaycastHit hit;
    //RectTransform rt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.material = normal_Material;
        minimap.SetActive(false);
        trapped = false;
        particle_Trail.SetActive(true);
        isRushing = false;
        timer1 = waitTime;
        timer2 = destroyCoolDown;
        stemina = 0;
        playerNum = PublicVars.characters[2];
        //rt = (RectTransform)gameObject.transform;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal" + playerNum);
        float z = Input.GetAxis("Vertical" + playerNum);
        float rx = Input.GetAxis("RotateX" + playerNum);
        //float rz = Input.GetAxis("RotateY" + playerNum);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.gameObject.name == "Terrain")
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + GetComponent<Renderer>().bounds.size.y * .5f, transform.position.z);
            }
        }
        //Vector3 look = new Vector3(rx, 0, rz);
        if (playerNum != 3)
        {
            if (rx != 0)
            {
                transform.Rotate(rotateSpeed * rx * transform.up);
            }
            else
            {
                transform.rotation = transform.rotation;
            }
        }
        else
        {
            float rz = Input.GetAxis("RotateY" + playerNum);
            Vector3 look = new Vector3(rx, 0, rz);
            if (look.x != 0 && look.z != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(look, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
        }

        if (timer1 < waitTime)
        {
            timer1 += Time.deltaTime;
        }
        else
        {
            timer1 = waitTime;
        }

        if (timer2 < destroyCoolDown)
        {
            //Debug.Log("smaller");
            timer2 += Time.deltaTime;
        }
        else
        {
            timer2 = destroyCoolDown;
        }

        //rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
        if (trapped == false && !isRushing)
        {
            rb.velocity = transform.TransformDirection(new Vector3(x * speed, rb.velocity.y, z * speed));
        }

        if (Input.GetButton("Minimap" + playerNum))
        {
            minimap.SetActive(true);
        }
        else
        {
            minimap.SetActive(false);
        }

        //HIDING
        //set "Hide" button in project setting
        if (Input.GetButtonDown("Special"+playerNum) && !isHiding && stemina >= hideStemina)//(Input.GetButtonDown("Hide") || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Hide());
        }
        //Debug.Log(timer2);
        if (Input.GetButtonDown("Photo" + playerNum) && timer2 == destroyCoolDown && !isRushing && stemina >= rushStemina)
        {
            StartCoroutine("Rush");
        }
        if (isRushing)
        {
            rb.AddForce(transform.forward * rushSpeed);
        }
    
    }


    IEnumerator Rush()
    {
        //Debug.Log("yes");
        stemina -= rushStemina;
        isRushing = true;
        //rb.AddForce(transform.forward * rushSpeed);
        yield return new WaitForSeconds(rushDuration);
        timer2 = 0;
        isRushing = false;
    }

    IEnumerator Hide()
    {
        isHiding = true;
        stemina -= hideStemina;
        particle_Trail.SetActive(false);
        rend.material = hide_Material;
        yield return new WaitForSeconds(hideTime);
        timer1 = 0;
        rend.material = normal_Material;
        particle_Trail.SetActive(true);

        // wait a bit until can hide again
        yield return new WaitForSeconds(waitTime);
        isHiding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0);
            StartCoroutine("IsTrapped", other);
        }
        if (other.CompareTag("Mushroom") && stemina < 30)
        {
            //stemina grow
            stemina += mushroomStemina;
            Destroy(other.gameObject);
        }
    }
    IEnumerator IsTrapped(Collider c)
    {
        yield return new WaitForSeconds(.1f);
        trapped = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(3);
        trapped = false;
        Destroy(c.gameObject);
    }
}
