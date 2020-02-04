using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : MonoBehaviour
{
    public static int photo;
    public static float timer;

    public GameObject canvas;
    public GameObject minimap;
    public int nTraps;
    public float viewAngle;
    public float detectDistance;
    public float coolDown;
    public Transform creature;
    //public int playerNum;
    int playerNum;
    public float speed;
    public float rotateSpeed;

    GameObject placeholder;
    bool waited, trigger;
    Rigidbody rb;
    GameObject[] traps;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        photo = 0;
        rb = GetComponent<Rigidbody>();
        minimap.SetActive(false);
        //playerNum = PublicVars.characters[0];
        playerNum = 1;
        timer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        traps = GameObject.FindGameObjectsWithTag("Trap");

        float x = Input.GetAxis("Horizontal" + playerNum);
        float z = Input.GetAxis("Vertical" + playerNum);
        float rx = Input.GetAxis("RotateX" + playerNum);
        //float rz = Input.GetAxis("RotateY" + playerNum);
        //Vector3 look = new Vector3(rx, 0, rz);
        //if (look.x != 0 && look.z != 0)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(look, Vector3.up);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        //}
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


        //rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
        rb.velocity = transform.TransformDirection(new Vector3(x * speed, rb.velocity.y, z * speed));

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

        //if (a <= viewAngle && a >= - viewAngle && dist <= detectDistance && timer == coolDown)
        //{
        //    if (Input.GetButtonDown("Photo" + playerNum))
        //    {
        //        photo++;
        //        Debug.Log(photo);
        //        timer = 0;
        //    }
        //}
        if (timer == coolDown && Input.GetButtonDown("Photo" + playerNum))
        {
            timer = 0;
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
            if (traps.Length < nTraps)
            {
                //maybe a cool down time between setting traps
                Vector3 pos = new Vector3(transform.position.x, transform.position.y-2.2f, transform.position.z);
                Instantiate(Resources.Load("Trap"), pos, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            placeholder = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            trigger = true;
            if (Input.GetButtonDown("Special" + playerNum))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trap"))
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
    IEnumerator Wait()
    {
        waited = true;
        yield return new WaitForSeconds(.1f);
        trigger = false;
        waited = false;
    }
}
