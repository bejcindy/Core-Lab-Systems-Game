using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoutuberController : MonoBehaviour
{
    public static int photo;
    public static float timer, stealTimer;
    AudioSource aud;

    public GameObject ranger;
    public Image stealIcon, cameraIcon;
    public Text leftCams;
    public GameObject flashLight;
    public float stealCoolDown;
    public GameObject canvas;
    public GameObject minimap;
    public int nCam;
    public float viewAngle;
    public float detectDistance;
    public float coolDown;
    public Transform creature;
    //public int playerNum;
    int playerNum;
    public float speed;
    public float rotateSpeed;

    bool trigger,waited;
    Rigidbody rb;
    GameObject placeholder;
    GameObject[] cams;
    public static bool trapped;
    RaycastHit hit;

    public Animator ytbAnim;
    public Animator instruction;

    // Start is called before the first frame update
    void Start()
    {
        flashLight.SetActive(false);
        photo = 0;
        trapped = false;
        rb = GetComponent<Rigidbody>();
        minimap.SetActive(false);
        timer = coolDown;
        stealTimer = stealCoolDown;
        aud = GetComponent<AudioSource>();
        //playerNum = PublicVars.characters[1];
        playerNum = 1;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        cams = GameObject.FindGameObjectsWithTag("YoutuberCam");

        stealIcon.fillAmount = stealTimer / stealCoolDown;
        cameraIcon.fillAmount = timer / coolDown;
        leftCams.text = "x " + (nCam - cams.Length);

        float x = Input.GetAxis("Horizontal" + playerNum);
        float z = Input.GetAxis("Vertical" + playerNum);
        float rx = Input.GetAxis("RotateX" + playerNum);

        if (stealTimer < stealCoolDown)
        {
            stealTimer += Time.deltaTime;
        }
        else
        {
            stealTimer = stealCoolDown;
        }

        if (stealTimer == stealCoolDown && Input.GetButtonDown("Submit" + playerNum) && RangerController.photo>0 && Vector3.Distance(transform.position,ranger.transform.position)<=50)
        {
            RangerController.photo--;
            photo++;
            stealTimer = 0;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.gameObject.name == "Terrain")
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + GetComponent<Renderer>().bounds.size.y * .5f, transform.position.z);
            }
        }
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

        if (trapped == false)
        {
            //rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
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
            canvas.SetActive(false);
            minimap.SetActive(true);
        }
        else
        {
            canvas.SetActive(true);
            minimap.SetActive(false);
        }

        if (timer < coolDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = coolDown;
        }

        float a = Vector3.Angle(transform.forward, creature.position - transform.position);
        float dist = Vector3.Distance(transform.position, creature.position);

        //if (a <= viewAngle && a >= -viewAngle && dist <= detectDistance && timer == coolDown)
        //{
        //    if (Input.GetButtonDown("Photo"+playerNum))
        //    {
        //        timer = 0;
        //        photo++;
        //        Debug.Log(photo);
        //    }
        //}
        if (timer == coolDown && Input.GetButtonDown("Photo" + playerNum))
        {
            timer = 0;
            aud.Play();
            StartCoroutine("Flash");
            if (a <= viewAngle && a >= -viewAngle && dist <= detectDistance)
            {
                photo++;
            }
        }
        if (!placeholder && !waited)
        {
            StartCoroutine("Wait");
        }
        if (Input.GetButtonDown("Special" + playerNum) && trigger == false)
        {
            //Debug.Log("yes");
            if (cams.Length < nCam)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
                Instantiate(Resources.Load("YoutuberCam"), pos, Quaternion.identity);
            }
        }

        //Animation

        if (rb.velocity.magnitude > 0.01 && !ytbAnim.GetBool("run"))
        {
            ytbAnim.SetBool("run", true);
        }

        if (rb.velocity.magnitude < 0.01 && ytbAnim.GetBool("run"))
        {
           ytbAnim.SetBool("run", false);
        }

        if (Input.anyKeyDown || Input.GetAxisRaw("Horizontal" + playerNum) != 0 || Input.GetAxisRaw("Vertical" + playerNum) != 0)
        {
            instruction.SetTrigger("fadeOut");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0);
            other.gameObject.GetComponent<Animator>().SetBool("trapTriggered", true);
            StartCoroutine("IsTrapped", other);
        }
        if (other.CompareTag("YoutuberCam"))
        {
            placeholder = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("YoutuberCam"))
        {
            //Debug.Log("triggeredd");
            trigger = true;
            if (Input.GetButtonDown("Special" + playerNum))
            {
                Destroy(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("YoutuberCam"))
        {
            trigger = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Creature"))
        {
            if (CreatureController.isRushing)
            {
                if (photo > 1)
                {
                    photo -= 2;
                }
                if (photo == 1)
                {
                    photo -= 1;
                }
            }
        }
    }

    IEnumerator Flash()
    {
        flashLight.SetActive(true);
        yield return new WaitForSeconds(.1f);
        flashLight.SetActive(false);
    }

    IEnumerator Wait()
    {
        waited = true;
        yield return new WaitForSeconds(.1f);
        trigger = false;
        waited = false;
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
