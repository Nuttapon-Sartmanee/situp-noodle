using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_start_script : MonoBehaviour
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

        if (isPushTrigger == false)
        {
            isPushTrigger = true;

            if (arduinoRightInput == 1 || Input.GetKeyDown(KeyCode.E) || arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("gameScene");
            }
        }

        if (arduinoRightInput == 0 && arduinoLeftInput == 0) //For Single Push
        {
            isPushTrigger = false;
        }
    }
}
