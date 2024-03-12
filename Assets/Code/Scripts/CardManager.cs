using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Sprite[] spriteList;
    public GameObject cardPrefab;

    private int pairAmount;
    private List<GameObject> cardDeck = new List<GameObject>();
    private List<Vector3> cardPositions;

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // VARIABLES THAT DEPEND ON OTHER SCRIPTS SHOULD BE INITIALIZED IN `Start` METHOD
        pairAmount = GlobalProviders.instance.currentLevel.pairAmount;
        cardPositions = GlobalProviders.instance.currentLevel.cardPositions;
        CreatePlayField();
        ShuffleCards();
        StartCoroutine(PutCardsInPlayfield());
    }

    private void CreatePlayField()
    {
        for (int i = 0; i < pairAmount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject newCard = Instantiate(cardPrefab);
                // setting card id and sprite
                newCard.GetComponent<Card>().SetCard(i, spriteList[i]);
                cardDeck.Add(newCard);
            }
        }
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < cardDeck.Count; i++)
        {
            int index = Random.Range(0, cardDeck.Count);
            GameObject temp = cardDeck[i];
            cardDeck[i] = cardDeck[index];
            cardDeck[index] = temp;
        }
    }

    private IEnumerator PutCardsInPlayfield()
    {
        for (int i = 0; i < cardDeck.Count; i++)
        {
            Card cardScript = cardDeck[i].GetComponent<Card>();
            Coroutine coroutine = StartCoroutine(cardScript.AnimateCardIntoPosition(cardPositions[i]));
            StartCoroutine(cardScript.AnimateCardRotation());

            if (i == cardDeck.Count - 1)
            {
                // WAIT FOR THE ANIMATION OF THE LAST CARD TO FINISH
                yield return coroutine;
            }
            else
            {
                yield return new WaitForSeconds(0.15f);
            }
        }

        // MAYBE SHOW A COUNTDOWN TIMER ON SCREEN
        // 3 .... 2 .... 1 .... START

        // SHOULD CALL THIS IN `AnimateCameraIntoPosition` OF THE `CameraBehaviour` SCRIPT INSTEAD OF HERE???
        ScoreManager.instance.StartTimer();
    }
}
