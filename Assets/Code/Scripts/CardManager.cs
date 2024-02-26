using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // INITIALIZED FROM THE EDITOR
    public int width;

    // INITIALIZED FROM THE EDITOR
    public int height;

    private int pairAmount;

    public Sprite[] spriteList;

    float _offset = 1.6f;

    public GameObject cardPrefab;

    public List<GameObject> cardDeck = new List<GameObject>();

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        // NUMBER OF PAIRS IS EQUAL TO THE HALF OF NUMBER OF CARDS
        pairAmount = (width * height) / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCameraPosition();
        GameManager.instance.SetPairAmount(pairAmount);
        CreatePlayField();
    }

    private void SetCameraPosition()
    {
        // SET CAMERA POSITION TO THE CENTER OF ALL CARDS
        float cardWidth = 1.0f;
        float cardHeight = 1.45f;

        // CENTERS THE X COORDINATE PERFECTLY
        int a = width / 2;
        float xCoordinate = a * _offset * cardWidth;
        if (width % 2 == 0)
        {
            xCoordinate -= _offset / 2;
        }

        // THE Z COORDINATE IS NOT PERFECT, YET ;)
        int b = -(height / 2);
        float zCoordinate = b * _offset * cardHeight;
        /*if (height % 2 == 0)
        {
            zCoordinate += _offset / 2;
        }*/
        Camera.main.transform.position = new Vector3(xCoordinate, 4.5f, zCoordinate);
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

        ShuffleCards();
        PutCardsInPlayfield();
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

    private void PutCardsInPlayfield()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                int index = (x * height) + z;
                //Vector3 position = transform.position + new Vector3(x * _offset, 0, z * _offset);
                Vector3 position = new Vector3(x * _offset, 0, z * _offset);
                cardDeck[index].transform.position = position;
            }
        }
    }
}
