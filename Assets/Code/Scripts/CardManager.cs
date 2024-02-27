using System.Collections.Generic;
using System.Threading.Tasks;
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
        float xCoordinate = CalculateCameraXCoordinate();
        float zCoordinate = -CalculateCameraZCoordinate() * 1.45f;
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

    private async void PutCardsInPlayfield()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                int index = (x * height) + z;
                Vector3 position = new Vector3(x * _offset, 0, z * _offset);
                Card cardScript = cardDeck[index].GetComponent<Card>();

                await Task.Delay(200);
                cardScript.AnimateCardIntoPosition(position);
                cardScript.AnimateCardRotation(new Vector3(360, 0, 0));
                //cardDeck[index].transform.position = position;
            }
        }

        AnimateCameraIntoPosition();
    }

    private void AnimateCameraIntoPosition()
    {
        Vector3 currentRotation = Camera.main.transform.eulerAngles;
        float durationSeconds = 2.5f;
        StartCoroutine(Camera.main.transform.AnimateRotation(currentRotation, new Vector3(90.0f, 0.0f, 0.0f), durationSeconds, EasingFunctions.EaseOutCubic));

        float zCoordinate = CalculateCameraZCoordinate();
        Vector3 currentPosition = Camera.main.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, zCoordinate);
        StartCoroutine(Camera.main.transform.AnimateToPosition(currentPosition, targetPosition, durationSeconds, EasingFunctions.EaseOutCubic));
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE X AXIS
    private float CalculateCameraXCoordinate()
    {
        //float cardWidth = 1.0f;

        int a = width / 2;
        float xCoordinate = a * _offset;
        if (width % 2 == 0)
        {
            xCoordinate -= _offset / 2;
        }

        return xCoordinate;
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE Z AXIS
    private float CalculateCameraZCoordinate()
    {
        //float cardHeight = 1.45f;

        int b = height / 2;
        float zCoordinate = b * _offset;
        if (height % 2 == 0)
        {
            zCoordinate -= _offset / 2;
        }
        return zCoordinate;
    }

    // SHOULD CALCULATE HOW FAR THE CAMERA SHOULD BE, IN ORDER TO SEE ALL THE CARDS
    private void CalculateCameraYCoordinate() { }
}
