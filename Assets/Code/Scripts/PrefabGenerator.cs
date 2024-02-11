using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public static Instantiation GlobalInstance;

    void Awake() => GlobalInstance = this;

    private int firstCard = -1;
    private int secondCard = -1;
    private const int gridRows = 4;
    private const int gridCols = 8;
    //private const float offsetX = 100f;
    //private const float offsetY = 100f;


    private const int _screenWidth = 1920;
    private const int _screenHeight = 1080;

    private const float _cardWidth = _screenWidth / gridCols;
    private const float _cardHeight = _screenHeight / gridRows;

    private const float _mostRight = (_screenWidth / 2) - (_cardWidth / 2);
    private const float _mostLeft = _mostRight * -1;
    private const float _topMost = (_screenHeight / 2) - (_cardHeight / 2);
    private const float _bottomMost = _topMost * -1;

    [SerializeField] private Card myPrefab;

    private List<Card> _cards = new List<Card>();

    private void Start()
    {
        //GameObject box = GameObject.Find(/* "Box" */"Cube");
        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridCols; col++)
            {
                int index = (gridCols * row) + col;
                float xCoordinate = _mostLeft + (col * _cardWidth);
                float yCoordinate = _topMost - (row * _cardHeight);

                Card spawnedCard = Instantiate(myPrefab, new Vector3(xCoordinate, yCoordinate, 0), Quaternion.identity);

                //Card spawnedCard = Instantiate(myPrefab);
                //spawnedCard.transform.position = new Vector3(xCoordinate, yCoordinate, 0);
                //spawnedCard.transform.rotation = Quaternion.identity;


                spawnedCard.transform.localScale = new Vector3(_cardWidth - 20, _cardHeight - 20, 1);
                spawnedCard.index = index;
                _cards.Add(spawnedCard);
            }
        }
    }

    public void handleCardClick(int index)
    {
        if (firstCard == -1)
        {
            firstCard = index;
            _openCard(index);
        }
        else if (secondCard == -1)
        {
            secondCard = index;
            _openCard(index);
            _checkMatch();
        }
    }

    private void _checkMatch()
    {
        //Debug.Log(firstCard);
        //Debug.Log(secondCard);
    }

    private void _openCard(int index)
    {
        _cards[index].transform.eulerAngles = new Vector3(180, 0, 0);
    }

    private void _closeCard(int index)
    {
        _cards[index].transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
