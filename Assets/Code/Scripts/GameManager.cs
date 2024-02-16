using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
            // check if we have a match :)
        }
        else
        {
            picked = false;
        }
    }

    public bool TwoCardsPicked()
    {
        return picked;
    }
}
