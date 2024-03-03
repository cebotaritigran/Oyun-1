using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // INITIALIZED FROM THE EDITOR
    public static int width = 5;

    // INITIALIZED FROM THE EDITOR
    public static int height = 4;

    private int pairAmount;

    public Sprite[] spriteList;

    public static float offset = 1.6f;

    public GameObject cardPrefab;

    private List<GameObject> cardDeck = new List<GameObject>();

    public static List<Vector3> cardPositions = new List<Vector3>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        // NUMBER OF PAIRS IS EQUAL TO THE HALF OF NUMBER OF CARDS
        int numberOfCards = width * height;
        pairAmount = numberOfCards / 2;
        offset = 5.0f / numberOfCards;
        Debug.Log("Offset: " + offset);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetPairAmount(pairAmount);
        CalculateCardPositions();
        CameraBehaviour.instance.Initialize();
        CreatePlayField();
        ShuffleCards();
        PutCardsInPlayfield();
    }

    private void CalculateCardPositions()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float cardWidth = 1.0f;
                float cardHeight = 1.45f;

                Vector3 position = new Vector3(x * (offset + cardWidth), 0, z * (offset + cardHeight));
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

    private async void PutCardsInPlayfield()
    {
        for (int i = 0; i < cardDeck.Count; i++)
        {
            Card cardScript = cardDeck[i].GetComponent<Card>();
            cardScript.AnimateCardIntoPosition(cardPositions[i]);
            cardScript.AnimateCardRotation();
            await Task.Delay(150);
        }
    }
}
