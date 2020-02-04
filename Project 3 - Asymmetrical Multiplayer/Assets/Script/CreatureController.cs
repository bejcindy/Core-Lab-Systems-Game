using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour
{
    public static float timer1, timer2;

    public Image hideIcon, rushIcon;
    //public int playerNum;
    int playerNum;
    Rigidbody rb;
    public float destroyCoolDown;
    public GameObject minimap;
    public float speed;
    public float rotateSpeed;
    public float rushDuration, rushSpeed;

    public GameObject meshWithAnim;
    Renderer rend;
    public Material normal_Material;
    public Material hide_Material;
    public float hideTime;
    public float waitTime;

    bool isHiding = false;
    public static bool trapped;
    public static bool isRushing;
    
    public GameObject particle_Trail;

    public Image rangerBlind, youtuberBlind;
    public int mushroomStemina, hideStemina, rushStemina;
    public static int stemina;
    RaycastHit hit;
    float o;
    public float fadeSpeed;

    public LayerMask youtuberNormal, youtuberHiding, rangerNormal, rangerHiding;
    public Camera rangerCam, youtuberCam;

    public Animator deerAnim;
    public Animator instruction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = meshWithAnim.GetComponent<Renderer>();
        rend.material = normal_Material;
        minimap.SetActive(false);
        trapped = false;
        particle_Trail.SetActive(true);
        isRushing = false;
        timer1 = waitTime;
        timer2 = destroyCoolDown;
        stemina = 20;
        rangerCam.cullingMask = rangerNormal;
        youtuberCam.cullingMask = youtuberNormal;
        //playerNum = PublicVars.characters[2];
        playerNum = 2;
        //rangerBlind = GameObject.Find("RangerBlind").GetComponent<Image>();
        //youtuberBlind = GameObject.Find("YoutuberBlind").GetComponent<Image>();
        //rt = (RectTransform)gameObject.transform;
        o = 0;
    }

    void Update()
    {
        hideIcon.fillAmount = timer1 / waitTime;
        rushIcon.fillAmount = timer2 / destroyCoolDown;
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
        if (rx != 0)
        {
            transform.Rotate(rotateSpeed * rx * transform.up);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
        //if (playerNum != 3)
        //{
        //    if (rx != 0)
        //    {
        //        transform.Rotate(rotateSpeed * rx * transform.up);
        //    }
        //    else
        //    {
        //        transform.rotation = transform.rotation;
        //    }
        //}
        //else
        //{
        //    float rz = Input.GetAxis("RotateY" + playerNum);
        //    Vector3 look = new Vector3(rx, 0, rz);
        //    if (look.x != 0 && look.z != 0)
        //    {
        //        Quaternion targetRotation = Quaternion.LookRotation(look, Vector3.up);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        //    }
        //}

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

        if (o > 0)
        {
            //Debug.Log("true");
            o -= Time.deltaTime * fadeSpeed;
        }
        else
        {
            o = 0;
        }
        rangerBlind.color = new Color(rangerBlind.color.r, rangerBlind.color.g, rangerBlind.color.b, o);
        youtuberBlind.color = new Color(youtuberBlind.color.r, youtuberBlind.color.g, youtuberBlind.color.b, o);
        //rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
        if (trapped == false && !isRushing)
        {
            if (x == 0 && z == 0)
            {
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = false;
                rb.velocity = transform.TransformDirection(new Vector3(x * speed, rb.velocity.y, z * speed));
            }
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

        //Animation

        if (rb.velocity.magnitude > 0.01 && !deerAnim.GetBool("deerRun"))
        {
            deerAnim.SetBool("deerRun", true);
        }

        if (rb.velocity.magnitude < 0.01 && deerAnim.GetBool("deerRun"))
        {
            deerAnim.SetBool("deerRun", false);
        }

        if (Input.anyKeyDown||Input.GetAxisRaw("Horizontal"+playerNum)!=0||Input.GetAxisRaw("Vertical"+playerNum)!=0)
        {
            instruction.SetTrigger("fadeOut");
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
        o = 1;
        rangerCam.cullingMask = rangerHiding;
        youtuberCam.cullingMask = youtuberHiding;
        stemina -= hideStemina;
        particle_Trail.SetActive(false);
        rend.material = hide_Material;
        yield return new WaitForSeconds(hideTime);
        timer1 = 0;
        rend.material = normal_Material;
        particle_Trail.SetActive(true);
        rangerCam.cullingMask = rangerNormal;
        youtuberCam.cullingMask = youtuberNormal;
        // wait a bit until can hide again
        yield return new WaitForSeconds(waitTime);
        isHiding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0);
            other.gameObject.GetComponent<Animator>().SetBool("trapTriggered", true);
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
        c.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0);
        Destroy(c.gameObject);
    }
}
