using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public Transform positionR1;
    public Transform positionR2;
    public Transform positionR3;
    public Transform positionR4;
    public Transform positionL1;
    public Transform positionL2;
    public Transform positionL3;
    public Transform positionL4;

    public Transform positionR_end;
    public Transform positionL_end;

    public SpriteRenderer spriteRenderer;

    //Hit Effect
    public GameObject leftHit;
    public GameObject rightHit;

    //Enemy Position
    private int position = 5;

    //Answer
    public int finalEnemyAnswer; // 0 = Right , 1 = Left
    public int finalPlayerAnswer; // 0 = Right , 1 = Left

    //Input from Arduino
    public arduinoInput arduinoInput;
    public int arduinoLeftInput;
    public int arduinoRightInput;

    //Sound manager
    public enemySoundManager_script soundManager;

    //UI Manager
    public spawnEnemy spawnEnemy;

    //Single Push Only
    private bool isPushTrigger = false;

    public bool isGameEnd;

    public GameObject BGM_Audio;

    private void Start()
    {
        arduinoInput = GameObject.FindWithTag("arduinoInput").GetComponent<arduinoInput>();
        spawnEnemy = GameObject.FindWithTag("spawnEnemy").GetComponent<spawnEnemy>();
        BGM_Audio = GameObject.FindWithTag("bgmAudio");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        int randomNum;
        randomNum = Random.Range(0,2);

        if (randomNum == 0) //Right side
        {
            gameObject.transform.position = positionR4.transform.position;
        }
        else //Left side
        {
            gameObject.transform.position = positionL4.transform.position;
            spriteRenderer.flipX = true;
        }
    }
    private void Update()
    {
        arduinoLeftInput = arduinoInput.leftInput; //Input From Arduino <
        arduinoRightInput = arduinoInput.rightInput; //Input From Arduino >

        isGameEnd = spawnEnemy.isGameEnd;

        if (arduinoRightInput == 1 || arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            checkPlayerInput();
            checkPush();
        }
        if (arduinoRightInput == 0 && arduinoLeftInput == 0) //For Single Push
        {
            isPushTrigger = false;
        }
    }

    private void checkPlayerInput()
    {
        if (arduinoRightInput == 1 || Input.GetKeyDown(KeyCode.E)) //Right Answer
        {
            finalPlayerAnswer = 0;
        }
        else if (arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q)) //Left Answer
        {
            finalPlayerAnswer = 1;
        }
    }

    private void checkPush()
    {
        if (isPushTrigger == false && isGameEnd == false)
        {
            isPushTrigger = true;
            int randomNum;
            randomNum = Random.Range(0, 2);

            position--;
            updatePosEnemy(randomNum);
            endCheck();
        }
        else 
        {
            //Debug.Log("Game has Ended");
        }
    }

    private void endCheck()
    {
        if (position == 0)
        {
            if (finalEnemyAnswer == 0) //Right side
            {
                gameObject.transform.position = positionR_end.transform.position;
                spriteRenderer.flipX = false;

                if (finalPlayerAnswer == 0) //Right Answer
                {
                    StartCoroutine(killEnemy(0.5f , finalPlayerAnswer));
                }
                else 
                {
                    endScene();
                }
            }
            else //Left side
            {
                gameObject.transform.position = positionL_end.transform.position;
                spriteRenderer.flipX = true;

                if (finalPlayerAnswer == 1) //Left Answer
                {
                    StartCoroutine(killEnemy(0.4f, finalPlayerAnswer));
                }
                else
                {
                    endScene();
                }
            }
        }
    }

    private void endScene()
    {
        BGM_Audio.SetActive(false);
        soundManager.playEndSound(0.5f);
        spawnEnemy.endScene();
    }

    private IEnumerator killEnemy(float waitTime , int playerAnswer)
    {
        GameObject hit;

        if (playerAnswer == 0) //Right
        {
            hit = Instantiate(rightHit);
        }
        else 
        {
            hit = Instantiate(leftHit);
        }

        soundManager.playRandomHitSound(0.5f);

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        Destroy(hit);
    }

    private void updatePosEnemy(int isLeft)
    {
        if (isLeft == 0) //Right side
        {
            if (position == 4)
            {
                gameObject.transform.position = positionR4.transform.position;
                spriteRenderer.flipX = false;
                soundManager.playRandomSound(0.1f);
            }
            else if (position == 3)
            {
                gameObject.transform.position = positionR3.transform.position;
                spriteRenderer.flipX = false;
                soundManager.playRandomSound(0.15f);
            }
            else if (position == 2)
            {
                gameObject.transform.position = positionR2.transform.position;
                spriteRenderer.flipX = false;
                soundManager.playRandomSound(0.2f);
            }
            else if (position == 1)
            {
                gameObject.transform.position = positionR1.transform.position;
                spriteRenderer.flipX = false;
                soundManager.playRandomSound(0.25f);

                finalEnemyAnswer = 0; //Player choose
            }
        }
        else //Left side
        {
            if (position == 4)
            {
                gameObject.transform.position = positionL4.transform.position;
                spriteRenderer.flipX = true;
                soundManager.playRandomSound(0.1f);
            }
            else if (position == 3)
            {
                gameObject.transform.position = positionL3.transform.position;
                spriteRenderer.flipX = true;
                soundManager.playRandomSound(0.15f);
            }
            else if (position == 2)
            {
                gameObject.transform.position = positionL2.transform.position;
                spriteRenderer.flipX = true;
                soundManager.playRandomSound(0.2f);
            }
            else if (position == 1)
            {
                gameObject.transform.position = positionL1.transform.position;
                spriteRenderer.flipX = true;
                soundManager.playRandomSound(0.25f);

                finalEnemyAnswer = 1; //Player choose
            }
        }
    }
}
