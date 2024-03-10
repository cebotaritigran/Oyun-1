using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text timerText;

    public TMP_Text scoreText;

    public TMP_Text turnText;

    private int score;

    public bool timerRunning = false;
    public float timeElapsed = 0.0f;

    // THIS WILL VARY BASED ON THE LEVEL (AND MAYBE SOME OTHER ASPECTS AS WELL)
    //private int maxNumberOfAttempts;
    public int attemptCounter = 0;

    // SHOW TIME WITHOUT MILLISECONDS IN THE GUI
    private int seconds = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString("D4");
    }

    public void IncrementAttemptCounter()
    {
        attemptCounter++;
        Debug.Log("Attempt " + attemptCounter);
        turnText.text = attemptCounter.ToString("D2");
    }

    public void FinishGame()
    {
        float time = timeElapsed;
        StopTimer();
        Debug.Log("Finished in " + time + " seconds with " + attemptCounter + " attempts in total");
        attemptCounter = 0;
    }

    // Gets called every 0.02 seconds (2 milliseconds)
    void FixedUpdate()
    {
        if (!timerRunning)
        {
            return;
        }

        timeElapsed += Time.fixedDeltaTime;

        // SHOW TIME WITHOUT MILLISECONDS IN THE GUI
        int sec = (int)timeElapsed;
        if (sec != seconds)
        {
            seconds = sec;
            timerText.text = seconds.ToString();
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        Debug.Log("Started");
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void StopTimer()
    {
        PauseTimer();
        timeElapsed = 0.0f;
    }
}
