using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_game_script : MonoBehaviour
{
    //Input from Arduino
    public arduinoInput arduinoInput;
    public int arduinoLeftInput;
    public int arduinoRightInput;

    //Single Push Only
    private bool isPushTrigger = false;

    void Start()
    {
        arduinoInput = GameObject.FindWithTag("arduinoInput").GetComponent<arduinoInput>();
    }

    void Update()
    {
        arduinoLeftInput = arduinoInput.leftInput; //Input From Arduino <
        arduinoRightInput = arduinoInput.rightInput; //Input From Arduino >

        if (arduinoRightInput == 1 || arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            checkPush();
        }

        if (arduinoRightInput == 0 && arduinoLeftInput == 0) //For Single Push
        {
            isPushTrigger = false;
        }
    }

    private void checkPush()
    {
        if (isPushTrigger == false)
        {
            isPushTrigger = true;

            if (arduinoRightInput == 1 || Input.GetKeyDown(KeyCode.E)) //Right Answer
            {

                SceneManager.LoadScene("startScene");
            }
            else if (arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q)) //Left Answer
            {
                // Get the current active scene
                Scene currentScene = SceneManager.GetActiveScene();

                // Reload the current scene
                SceneManager.LoadScene(currentScene.name);
            }
        }
    }
}
