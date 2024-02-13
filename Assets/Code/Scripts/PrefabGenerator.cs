using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.UI;

public class Instantiation : MonoBehaviour
{
    public static Instantiation GlobalInstance;

    void Awake() => GlobalInstance = this;

    private int _firstCardIndex = -1;
    private int _secondCardIndex = -1;
    private const int _gridRows = 4;
    private const int _gridCols = 8;
    private const int _dimension = _gridRows * _gridCols;
    private const int _screenWidth = 1920;
    private const int _screenHeight = 1080;
    private const float _cardWidth = _screenWidth / _gridCols;
    private const float _cardHeight = _screenHeight / _gridRows;
    private const float _mostRight = (_screenWidth / 2) - (_cardWidth / 2);
    private const float _mostLeft = _mostRight * -1;
    private const float _topMost = (_screenHeight / 2) - (_cardHeight / 2);
    private const float _bottomMost = _topMost * -1;

    [SerializeField] private Card myPrefab;

    private List<int> _randomNumbers;
    private List<bool> _isMatched = Enumerable.Range(0, _dimension).Select(x => false).ToList();

    private List<Card> _cards = new List<Card>();
    private List<Animator> _animators = new List<Animator>();

    private void Start()
    {
        _initializeGameState();
        for (int row = 0; row < _gridRows; row++)
        {
            for (int col = 0; col < _gridCols; col++)
            {
                int index = (_gridCols * row) + col;
                float xCoordinate = _mostLeft + (col * _cardWidth);
                float yCoordinate = _topMost - (row * _cardHeight);

                Card spawnedCard = Instantiate(myPrefab, new Vector3(xCoordinate, yCoordinate, 0), Quaternion.identity);
                Animator animator = spawnedCard.GetComponent<Animator>();

                //Card spawnedCard = Instantiate(myPrefab);
                //spawnedCard.transform.position = new Vector3(xCoordinate, yCoordinate, 0);
                //spawnedCard.transform.rotation = Quaternion.identity;

                spawnedCard.transform.localScale = new Vector3(_cardWidth - 20, _cardHeight - 20, 10);
                spawnedCard.index = index;

                //Text textComponent = spawnedCard.GetComponent<Text>();
                //textComponent.text = _randomNumbers[index].ToString();
                
                _cards.Add(spawnedCard);
                _animators.Add(animator);

                _staggeredAnimation();
            }
        }
    }

    private void _initializeGameState()
    {
        _randomNumbers = _generateRandomNumbers(_dimension / 2);

        List<int> indices = Enumerable.Range(0, _dimension).ToList();

        _randomNumbers.AddRange(_randomNumbers.GetRange(0, _randomNumbers.Count));
        _randomNumbers.Shuffle();

        List<int> tmpRandomNumbers = new List<int>(_randomNumbers);
        _randomNumbers.Clear();

        for (int j = 0; j < _dimension; j++)
        {
            _randomNumbers.Add(tmpRandomNumbers[indices[j]]);
        }
    }

    private List<int> _generateRandomNumbers(int dimension)
    {
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < dimension; i++)
        {
            System.Random rnd = new System.Random();
            int randomNumber = rnd.Next();
            while (randomNumbers.Contains(randomNumber))
            {
                randomNumber = rnd.Next();
            }
            randomNumbers.Add(randomNumber);
        }

        return randomNumbers;
    }

    private async void _staggeredAnimation()
    {
        await Task.Delay(1000);
        for (int i = 0; i < _cards.Count; i++)
        {
            await Task.Delay(100);
            _animators[i].Play("Base Layer.OpenCard", 0, 0.0f);
        }
    }

    public void handleCardClick(int index)
    {
        _animators[index].Play("Base Layer.OpenCard", 0, 0.0f);
        if (_firstCardIndex == -1)
        {
            _firstCardIndex = index;
            _openCard(index);
        }
        else if (_secondCardIndex == -1)
        {
            _secondCardIndex = index;
            _openCard(index);
            _checkMatch();
        }
    }

    private void _checkMatch()
    {
        //Debug.Log(firstCard);
        //Debug.Log(secondCard);
        if (_randomNumbers[_firstCardIndex] == _randomNumbers[_secondCardIndex])
        {
            // CORRECT MATCH
            Debug.Log("CORRECT MATCH");
        }
        else
        {
            // FALSE MATCH
            Debug.Log("FALSE MATCH");
        }
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

static class MyExtensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
