using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnEnemy : MonoBehaviour
{
    public int maxSpawnRate;

    public arduinoInput arduinoInput;
    public int arduinoLeftInput;
    public int arduinoRightInput;

    private bool isPushTrigger = false;

    //Enemy Prefab
    public GameObject enemy;

    //Score Text
    public TMP_Text ScoreText;
    public TMP_Text ScoreText_End;

    public GameObject end_Panel;
    public GameObject score_Panel;
    public GameObject sceneManager;

    public GameObject tutorial_Text;

    public int score;

    public bool isGameEnd = false;

    void Start()
    {
        tutorial_Text.SetActive(true);

        score = 0;
        ScoreText.text = $"{score}";
        ScoreText_End.text = $"{score}";
        arduinoInput = GameObject.FindWithTag("arduinoInput").GetComponent<arduinoInput>();

        sceneManager.SetActive(false);
        score_Panel.SetActive(true);
        end_Panel.SetActive(false);
    }
    void Update()
    {
        arduinoLeftInput = arduinoInput.leftInput; //Input From Arduino <
        arduinoRightInput = arduinoInput.rightInput; //Input From Arduino >

        if (arduinoRightInput == 1 || arduinoLeftInput == 1 || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            checkPush();
            hideTutorial();
        }
        if (arduinoRightInput == 0 && arduinoLeftInput == 0)
        {
            isPushTrigger = false;
        }
    }

    private void checkPush()
    {
        if (isPushTrigger == false && isGameEnd == false)
        {
            isPushTrigger = true;

            updateScore();

            int randomNum = Random.Range(0 , maxSpawnRate - 1); //Difficulty
            if (randomNum == 0)
            {
                Instantiate(enemy, new Vector3(0, 100, 0), Quaternion.identity);
            }
        }

    }

    private void updateScore()
    {
        score++;
        ScoreText.text = $"{score}";
        ScoreText_End.text = $"{score}";
    }

    public void endScene()
    {
        StartCoroutine(endStep(1f));
    }

    private IEnumerator endStep(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isGameEnd = true;
        score_Panel.SetActive(false);
        end_Panel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sceneManager.SetActive(true);
    }

    private void hideTutorial()
    {
        if (score > 5)
        {
            tutorial_Text.SetActive(false);
        }
    }
}
