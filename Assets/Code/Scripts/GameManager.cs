using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // INITIALIZED FROM THE EDITOR
    public AudioSource sound;
    public bool twoCardsPicked = false; // set this to true if we have 2 cards selected
    private int pairCounter = 0;
    private int scorePerMatch = 50;
    private List<Card> pickedCards = new List<Card>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;
    }

    public void AddCardToPickedList(Card card)
    {
        pickedCards.Add(card);
        if (pickedCards.Count == 2)
        {
            twoCardsPicked = true;
            ScoreManager.instance.IncrementAttemptCounter();
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

        Coroutine heightAnimationCoroutine = null;
        for (int i = 0; i < pickedCards.Count; i++)
        {
            heightAnimationCoroutine = StartCoroutine(pickedCards[i].transform.AnimateToPositionYAxis(Camera.main.transform.position.y / 2.0f, 1.5f, EasingFunctions.EaseInOutSine));
        }
        //yield return heightAnimationCoroutine;
        yield return new WaitForSeconds(0.25f);

        Coroutine positionAnimationCoroutine = null;
        for (int i = 0; i < pickedCards.Count; i++)
        {
            positionAnimationCoroutine = StartCoroutine(pickedCards[i].transform.AnimateToPositionXAxis(Camera.main.transform.position.x, 1.0f, EasingFunctions.EaseInOutCubic));
            positionAnimationCoroutine = StartCoroutine(pickedCards[i].transform.AnimateToPositionZAxis(Camera.main.transform.position.z, 1.0f, EasingFunctions.EaseInOutCubic));
        }
        yield return heightAnimationCoroutine;

        EnableHoverEffect();

        yield return null;
    }

    private IEnumerator CheckMatch()
    {
        if (pickedCards[0].cardId == pickedCards[1].cardId)
        {
            // match
            pairCounter++;
            ScoreManager.instance.AddScore(scorePerMatch);
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
        if (GlobalProviders.instance.currentLevel.pairAmount == pairCounter)
        {
            // WIN
            ScoreManager.instance.FinishGame();
            SceneManager.LoadScene("LevelSelection");
        }
    }
}
