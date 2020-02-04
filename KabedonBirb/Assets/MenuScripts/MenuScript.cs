using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private AudioSource playButton;
    public AudioClip playButtonAudio;

    public void Awake()
    {
        playButton = GetComponent<AudioSource>();

    }

    public void PlayButtonSound() {
        playButton.clip = playButtonAudio;
        playButton.Play();
        Debug.Log("Warm Piano 1 is playing");
    }

    public void StartButton() {
        StartCoroutine(gameStart());

    }

    public void ExitButton() {

        Application.Quit();
        Debug.Log("Player has quit");
    }

    IEnumerator gameStart()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
