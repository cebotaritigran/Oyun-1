using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // INITIALIZED FROM THE EDITOR
    public AudioSource sound;
    public bool twoCardsPicked = false; // set this to true if we have 2 cards selected
    private int pairAmount;
    private int pairCounter = 0;

    // THIS WILL VARY BASED ON THE LEVEL (AND MAYBE SOME OTHER ASPECTS AS WELL)
    private int maxNumberOfAttempts;
    private int attemptCounter = 0;

    public bool timerRunning = false;
    private float timeElapsed = 0.0f;

    // MAYBE TO BE USED IN THE GUI (SHOW TIME IN SECONDS, WITHOUT MILLISECONDS)
    //private int seconds = 0;

    private List<Card> pickedCards = new List<Card>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // VARIABLES THAT DEPEND ON OTHER SCRIPTS SHOULD BE INITIALIZED IN `Start` METHOD
        // TO BE CERTAIN THAT THEY ARE ALREADY INITIALIZED IN THAT SCRIPT'S `Awake` METHOD
        pairAmount = CardManager.pairAmount;
    }

    public void AddCardToPickedList(Card card)
    {
        pickedCards.Add(card);
        if (pickedCards.Count == 2)
        {
            twoCardsPicked = true;
            attemptCounter++;
            Debug.Log("Attempt " + attemptCounter);
            // check if we have a match :)
            StartCoroutine(CheckMatch());
        }
    }

    private void DisableHoverEffect()
    {
        for (int i = 0; i < pickedCards.Count; i++)
        {
            CardHoverMovement hoverScript = pickedCards[i].GetComponent<CardHoverMovement>();
            hoverScript.DisableHoverEffect();
        }
    }

    private void EnableHoverEffect()
    {
        for (int i = 0; i < pickedCards.Count; i++)
        {
            CardHoverMovement hoverScript = pickedCards[i].GetComponent<CardHoverMovement>();
            hoverScript.EnableHoverEffect();
        }
    }

    private IEnumerator CardMatchAnimation()
    {
        DisableHoverEffect();

        // DOING THIS WITH FOR LOOPS, IN CASE WE WANT TO MATCH 3+ CARDS IN SOME LEVELS

        for (int i = 0; i < pickedCards.Count; i++)
        {
            Vector3 targetPosition = new Vector3(pickedCards[i].transform.position.x, 0.35f, pickedCards[i].transform.position.z);
            Coroutine coroutine = StartCoroutine(pickedCards[i].transform.AnimateToPosition(pickedCards[i].transform.position, targetPosition, 0.5f, EasingFunctions.EaseOutQuart));
            if (i == pickedCards.Count - 1)
            {
                yield return coroutine;
            }
        }

        for (int i = 0; i < pickedCards.Count; i++)
        {
            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, pickedCards[i].transform.position.y, Camera.main.transform.position.z);
            Coroutine coroutine = StartCoroutine(pickedCards[i].transform.AnimateToPosition(pickedCards[i].transform.position, targetPosition, 1.0f, EasingFunctions.EaseOutCubic));
            if (i == pickedCards.Count - 1)
            {
                yield return coroutine;
            }
        }

        EnableHoverEffect();

        yield return null;
    }

    private IEnumerator CheckMatch()
    {
        if (pickedCards[0].cardId == pickedCards[1].cardId)
        {
            // match
            pairCounter++;
            CheckForWin();

            yield return new WaitForSeconds(0.5f);
            sound.Play();
            Debug.Log("lol");

            // WAITS UNTIL `CardMatchAnimation` FINISHES
            // SHOULD PAUSE THE TIMER HERE BECAUSE THE CARDS AREN'T CLICKABLE?
            // OR SHOULD MAKE THE CARDS CLICKABLE EVEN WHILE THE ANIMATION IS PLAYING?
            yield return StartCoroutine(CardMatchAnimation());

            pickedCards.ForEach((Card card) =>
            {
                card.gameObject.SetActive(false);
            });
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            pickedCards.ForEach((Card card) => card.FlipUp(false));
            //yield return new WaitForSeconds(0.5f);
        }

        // :)
        // clear the board 
        twoCardsPicked = false;
        pickedCards.Clear();
    }

    private void CheckForWin()
    {
        if (pairAmount == pairCounter)
        {
            // WIN
            float time = timeElapsed;
            StopTimer();
            Debug.Log("Finished in " + time + " seconds with " + attemptCounter + " attempts in total");
            attemptCounter = 0;
        }
    }

    // Gets called every 0.02 seconds (2 milliseconds)
    void FixedUpdate()
    {
        if (!timerRunning)
        {
            return;
        }

        timeElapsed += Time.fixedDeltaTime;

        // MAYBE TO BE USED IN THE GUI (SHOW TIME IN SECONDS, WITHOUT MILLISECONDS)
        /*int sec = Mathf.FloorToInt(timeElapsed);
        if (sec != seconds)
        {
            seconds = sec;
            //Debug.Log(seconds);
        }*/
    }

    public async void StartTimer(int afterMilliseconds = 0)
    {
        if (afterMilliseconds > 0)
        {
            float seconds = afterMilliseconds / 1000.0f;
            Debug.Log("Starting in " + seconds + " seconds...");
            await Task.Delay(afterMilliseconds);
        }
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
