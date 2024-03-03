using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource sound;
    public bool twoCardsPicked; // set this true if we have 2 cards selected
    private int pairAmount;
    private int pairCounter;

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
            CheckForWin();
        }
        else
        {
            pickedCards.ForEach((Card card) => card.FlipUp(false));
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

    // getting pair amount from card manager :)
    public void SetPairAmount(int pairAmount)
    {
        this.pairAmount = pairAmount;
    }
}
