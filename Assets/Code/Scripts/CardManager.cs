using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // INITIALIZED FROM THE EDITOR
    public static int width = 8;

    // INITIALIZED FROM THE EDITOR
    public static int height = 2;

    private int pairAmount;

    public Sprite[] spriteList;

    public static float offset = 1.6f;

    public GameObject cardPrefab;

    private List<GameObject> cardDeck = new List<GameObject>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        // NUMBER OF PAIRS IS EQUAL TO THE HALF OF NUMBER OF CARDS
        pairAmount = (width * height) / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetPairAmount(pairAmount);
        CreatePlayField();
        ShuffleCards();
        PutCardsInPlayfield();
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
            var temp = cardDeck[i];
            cardDeck[i] = cardDeck[index];
            cardDeck[index] = temp;
        }
    }

    private async void PutCardsInPlayfield()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                int index = (x * height) + z;
                Vector3 position = new Vector3(x * offset, 0, z * offset);
                Card cardScript = cardDeck[index].GetComponent<Card>();

                await Task.Delay(150);
                cardScript.AnimateCardIntoPosition(position);
                cardScript.AnimateCardRotation();
            }
        }
    }
}
