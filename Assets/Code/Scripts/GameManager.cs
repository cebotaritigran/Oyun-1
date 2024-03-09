using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource sound;
    public bool twoCardsPicked; // set this true if we have 2 cards selected
    private int pairAmount;
    private int pairCounter;

    public int scorePerMatch = 50;

    public int turnCounter = 1;

    List<Card> pickedCards = new List<Card>();

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
            // check if we have a match :)
            StartCoroutine(CheckMatch());
        }

    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1.5f);
        if (pickedCards[0].cardId == pickedCards[1].cardId)
        {
            // match
            sound.Play();
            Debug.Log("lol");
            pickedCards.ForEach((Card card) => card.gameObject.SetActive(false));
            pairCounter++;
            // adding score this could be different for each card for different type latter to be done
            ScoreManager.instance.AddScore(scorePerMatch);
            ScoreManager.instance.TurnCounter(turnCounter);
            CheckForWin();
        }
        else
        {
            pickedCards.ForEach((Card card) => card.FlipUp(false));
            // adding turns or subtracting to be done later
            ScoreManager.instance.TurnCounter(turnCounter);
            yield return new WaitForSeconds(1.2f);
        }

        // :)
        // clear the board 
        twoCardsPicked = false;
        pickedCards.Clear();
    }

    void CheckForWin()
    {
        if (pairAmount == pairCounter)
        {
            // WIN
            Debug.Log("ZORT");
        }
    }

    public void GameOver()
    {
        Debug.Log("YOU LOST");
    }

    // getting pair amount from card manager :)
    public void SetPairAmount(int pairAmount)
    {
        this.pairAmount = pairAmount;
    }
}
