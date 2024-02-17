using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource sound;
    bool picked; // set this true if we have 2 cards sleceted

    List<Card> pickedCards = new List<Card>();

    void Awake()
    {
        instance = this;
    }

    public void AddCardToPickedList(Card card)
    {
        pickedCards.Add(card);
        if (pickedCards.Count == 2)
        {
            picked = true;
            StartCoroutine(CheckMatch());
            // check if we have a match :)
        }

    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1.5f);
        if (pickedCards[0].GetCardId() == pickedCards[1].GetCardId())
        {
            //matych
            sound.Play();
            Debug.Log("lol");
            pickedCards[0].gameObject.SetActive(false);
            pickedCards[1].gameObject.SetActive(false);
        }
        else
        {
            pickedCards[0].FlipUp(false);
            pickedCards[1].FlipUp(false);
        }
        yield return new WaitForSeconds(2f);

        // :)
        picked = false;
        pickedCards.Clear();
    }

    public bool TwoCardsPicked()
    {
        return picked;
    }
}
