using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int timeLevelComplete = 60;
    public TMP_Text timerText;

    public TMP_Text scoreText;

    public TMP_Text turnText;

    int score;

    int turn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("Timer");

    }

    IEnumerator Timer()
    {
        int tempTime = timeLevelComplete;
        timerText.text = timeLevelComplete.ToString();

        while (tempTime > 0)
        {
            tempTime--;
            yield return new WaitForSeconds(1);

            timerText.text = tempTime.ToString();
        }
        // Game Over
        GameManager.instance.GameOver();


    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString("D4");
    }

    public void TurnCounter(int turnsUsed)
    {
        turn += turnsUsed;
        turnText.text = turn.ToString("D2");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
