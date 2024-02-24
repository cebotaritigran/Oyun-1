using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public int pairAmount;
    public Sprite[] spriteList;

    float _offSet = 1.6f;

    public GameObject cardPrefab;

    public List<GameObject> cardDeck = new List<GameObject>();
    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetPairs(pairAmount);
        CreatePlayField();
    }

    void CreatePlayField()
    {
        for (int i = 0; i < pairAmount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3 position = new Vector3(i * _offSet, 0, 0);
                GameObject newCard = Instantiate(cardPrefab, position, Quaternion.identity);
                // setting card id
                newCard.GetComponent<Card>().SetCard(i, spriteList[i]);
                cardDeck.Add(newCard);
            }
        }

        //shufler
        for (int i = 0; i < cardDeck.Count; i++)
        {
            int index = Random.Range(0, cardDeck.Count);
            var temp = cardDeck[i];
            cardDeck[i] = cardDeck[index];
            cardDeck[index] = temp;
        }

        int num = 0;
        // pass out the cards on the playfield at different positions
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = transform.position + new Vector3(x * _offSet, 0, z * _offSet);
                cardDeck[num].transform.position = position;
                num++;
            }
        }
    }
}
