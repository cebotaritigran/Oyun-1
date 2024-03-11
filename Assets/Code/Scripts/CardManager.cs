using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    // INITIALIZED FROM THE EDITOR
    // `CameraBehaviour` SCRIPT DEPENDS ON THIS VARIABLE
    public int width = 9;

    // INITIALIZED FROM THE EDITOR
    // `CameraBehaviour` SCRIPT DEPENDS ON THIS VARIABLE
    public int height = 2;

    // `GameManager` AND `CameraBehaviour` SCRIPTS DEPEND ON THIS VARIABLE
    public int pairAmount;

    public Sprite[] spriteList;

    public float offset = 1.6f;

    public GameObject cardPrefab;

    private List<GameObject> cardDeck = new List<GameObject>();

    // `CameraBehaviour` SCRIPT DEPENDS ON THIS VARIABLE
    public List<Vector3> cardPositions = new List<Vector3>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;

        int numberOfCards = width * height;
        pairAmount = numberOfCards / 2;
        offset = 5.0f / numberOfCards;
        Debug.Log("Offset: " + offset);
        CalculateCardPositions();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayField();
        ShuffleCards();
        StartCoroutine(PutCardsInPlayfield());
    }

    private void CalculateCardPositions()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float cardWidth = 1.0f;
                float cardHeight = 1.45f;

                Vector3 position = new Vector3(x * (offset + cardWidth), 0.0f, z * (offset + cardHeight));
                cardPositions.Add(position);
            }
        }
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
