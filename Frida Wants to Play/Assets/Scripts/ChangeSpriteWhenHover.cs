using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSpriteWhenHover : MonoBehaviour
{

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ChangeSprite()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ChangeSpriteBack()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void StartGame()
    {
        //Time.timeScale = 1f;
        //GameManagement.startGame = true;
        GameManagement.Opening.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void Continue()
    {
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
