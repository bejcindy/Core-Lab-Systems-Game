using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static bool startGame;
    public float waitTime, gapTime, levelCoolDown;

    GameObject Frida;
    GameObject HP1, HP2, HP3;
    GameObject StartTitle, PauseTitle, DieTitle;
    GameObject mouse, ball, laser;
    public static GameObject Opening, Winning;
    public static int levels;
    bool two, three, four, five, meowed;
    AudioSource aud;
    AudioClip die;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Frida = GameObject.FindGameObjectWithTag("Player");
        Opening = GameObject.Find("OpeningComic");
        Winning = GameObject.Find("Win");
        Opening.SetActive(false);
        Winning.SetActive(false);
        HP1 = GameObject.Find("HP1");
        HP2 = GameObject.Find("HP2");
        HP3 = GameObject.Find("HP3");
        HP1.SetActive(false);
        HP2.SetActive(false);
        HP3.SetActive(false);
        Frida.SetActive(false);
        StartTitle = GameObject.Find("StartTitle");
        PauseTitle = GameObject.Find("PauseTitle");
        DieTitle = GameObject.Find("FailedTitle");
        StartTitle.SetActive(true);
        PauseTitle.SetActive(false);
        DieTitle.SetActive(false);
        startGame = false;
        levels = 0;
        two = false;
        three = false;
        four = false;
        five = false;
        meowed = false;
        aud = GetComponent<AudioSource>();
        die = Resources.Load("Sounds/LoseMeow") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(startGame);
        if (Input.GetKeyDown(KeyCode.Escape) && !StartTitle.activeSelf && !DieTitle.activeSelf)
        {
            PauseTitle.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !StartTitle.activeSelf && !DieTitle.activeSelf)
        {
            PauseTitle.SetActive(true);
            Time.timeScale = 0f;
        }
        if (!Frida)
        {
            if (meowed == false)
            {
                StartCoroutine("Meow");
            }
            DieTitle.SetActive(true);
        }

        if (Opening.activeSelf == true && Input.anyKeyDown)
        {
            Opening.SetActive(false);
            startGame = true;
            Time.timeScale = 1f;
        }

        //for testing
        if (startGame == true)
        {
            Frida.SetActive(true);
        }

        if (startGame == true)
        {
            StartCoroutine("LevelOne");
        }
        if (levels == 1 && !GameObject.FindGameObjectWithTag("Enemy") && two == false)
        {
            StartCoroutine("LevelTwo");
        }
        if(levels == 2 && !GameObject.FindGameObjectWithTag("Enemy") && three == false)
        {
            StartCoroutine("Vacuum");
        }
        if(levels == 3 && !GameObject.FindGameObjectWithTag("Enemy") && four == false)
        {
            StartCoroutine("Shower");
        }
        if (levels == 4 && !GameObject.FindGameObjectWithTag("Enemy") && five == false)
        {
            StartCoroutine("Witch");
        }
        if(levels==5 && !GameObject.FindGameObjectWithTag("Enemy"))
        {
            Winning.SetActive(true);
        }
        if (Winning.activeSelf == true && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator Meow()
    {
        meowed = true;
        aud.PlayOneShot(die);
        yield return null;
    }

    IEnumerator LevelOne()
    {
        //Debug.Log("LevelGenerated");
        startGame = false;
        Frida.SetActive(true);
        HP1.SetActive(true);
        HP2.SetActive(true);
        HP3.SetActive(true);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        float y = Random.Range(-4.8f, 4.8f);
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(Resources.Load("Mouse"), new Vector2(9, y), Quaternion.identity);
        }
        yield return new WaitForSeconds(gapTime);
        y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("TreatBall"), new Vector2(9, y), Quaternion.identity);
        yield return new WaitForSeconds(gapTime);
        y = Random.Range(-4.8f, 4.8f);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(Resources.Load("Mouse"), new Vector2(9, y), Quaternion.identity);
        }
        yield return new WaitForSeconds(gapTime);
        y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("TreatBall"), new Vector2(9, y), Quaternion.identity);
        levels = 1;
    }

    IEnumerator LevelTwo()
    {
        two = true;
        yield return new WaitForSeconds(levelCoolDown);
        float y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("TreatBall"), new Vector2(9, y), Quaternion.identity);
        yield return new WaitForSeconds(gapTime);
        y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("LaserPointer"), new Vector2(9, y), Quaternion.identity);
        y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("LaserPointer"), new Vector2(9, y), Quaternion.identity);
        yield return new WaitForSeconds(gapTime);
        y = Random.Range(-4.8f, 4.8f);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(Resources.Load("Mouse"), new Vector2(9, y), Quaternion.identity);
        }
        y = Random.Range(-4.8f, 4.8f);
        Instantiate(Resources.Load("TreatBall"), new Vector2(9, y), Quaternion.identity);
        levels = 2;
    }

    IEnumerator Vacuum()
    {
        three = true;
        yield return new WaitForSeconds(levelCoolDown);
        Instantiate(Resources.Load("VacuumCleaner"));
        levels = 3;
    }

    IEnumerator Shower()
    {
        four = true;
        yield return new WaitForSeconds(levelCoolDown);
        Instantiate(Resources.Load("Shower"), new Vector2(10, 0), Quaternion.identity);
        levels = 4;
    }

    IEnumerator Witch()
    {
        five = true;
        yield return new WaitForSeconds(levelCoolDown);
        Vector2 witchPos = new Vector2(10.5f, 0);
        Instantiate(Resources.Load("Witch"), witchPos, Quaternion.identity);
        levels = 5;
    }
}
