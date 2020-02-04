using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Continue()
    {
        GameManagement.state = 1;
    }
    public void StartGame()
    {
        GameManagement.state = 2;
    }
    public void PauseGameTime()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGameTime()
    {
        Time.timeScale = 1f;
    }
    public void Door()
    {
        FenceDoorController.open = !FenceDoorController.open;
    }
    public void Restart()
    {
        //SceneManager.LoadScene("MenuPrototype");
        GameObject[] chicken = GameObject.FindGameObjectsWithTag("Chicken");
        for(int i = 0; i < chicken.Length; i++)
        {
            Destroy(chicken[i]);
        }
        GameManagement.t = 0;
        GameManagement.chickenCounter = 0;
        GameManagement.state = 2;
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("MenuPrototype");
    }
    public void GoToGameOver()
    {
        //just for testing things
        GameManagement.state = 3;
    }
    public void PosForwardDown()
    {
        SetEnvironment.moveForward = true;
    }
    public void PosForwardUp()
    {
        SetEnvironment.moveForward = false;
    }
    public void PosBackwardDown()
    {
        SetEnvironment.moveBackward = true;
    }
    public void PosBackwardUp()
    {
        SetEnvironment.moveBackward = false;
    }
    public void PosLeftDown()
    {
        SetEnvironment.moveLeft = true;
    }
    public void PosLeftUp()
    {
        SetEnvironment.moveLeft = false;
    }
    public void PosRightDown()
    {
        SetEnvironment.moveRight = true;
    }
    public void PosRightUp()
    {
        SetEnvironment.moveRight = false;
    }
    public void SizeUpDown()
    {
        SetEnvironment.sizeUp = true;
    }
    public void SizeUpUp()
    {
        SetEnvironment.sizeUp = false;
    }
    public void SizeDownDown()
    {
        SetEnvironment.sizeDown = true;
    }
    public void SizeDownUp()
    {
        SetEnvironment.sizeDown = false;
    }
    //public void SpawnChicken()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    GameObject ground = GameObject.FindGameObjectWithTag("Ground");
    //    Vector3 pos = player.transform.position + player.transform.forward * .5f;
    //    Vector3 spawnPos = new Vector3(pos.x, ground.transform.position.y + .25f, pos.z);
    //    Quaternion spawnRotation = Quaternion.Euler(0, player.transform.rotation.y, 0);
    //    Instantiate(Resources.Load("Chicken"), spawnPos, spawnRotation);
    //}
}
