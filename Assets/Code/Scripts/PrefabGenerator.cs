using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public int firstCard = -1;
    public int secondCard = -1;
    public const int gridRows = 4;
    public const int gridCols = 2;
    public const float offsetX = 100f;
    public const float offsetY = 100f;

    public GameObject myPrefab;

    public Card myCard;
    private void Start()
    {
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridCols; col++)
            {
                GameObject newSpawn = Instantiate(myPrefab, new Vector3(col * offsetX, 0, (gridRows - row - 1) * offsetY), Quaternion.identity);
                newSpawn.GetComponent<Card>().index = (gridCols * row) + col;
            }
        }
        /*for (int i = 0; i < gridCols; i++)
        {
            for (int k = 0; k < gridRows; k++)
            {
                GameObject newSpawn = Instantiate(myPrefab, new Vector3(i * offsetX, 0, k * offsetY), Quaternion.identity);
                newSpawn.GetComponent<Card>().index = (gridRows * k) + i;
            }
        }*/

    }

    public void handleCardClick(int index){
        if (firstCard == -1)
        {
            firstCard = index;
        }
        else if (secondCard == -1)
        {
            secondCard = index;
            checkMatch();
        }
    }

    public void checkMatch(){
        Debug.Log(firstCard);
        Debug.Log(secondCard);
    }


    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    //public GameObject myPrefab;

    // This script will simply instantiate the Prefab when the game starts.
    // void Start()
    // {
    //     for (int k = 0; k < 4; k++)
    //     {
    //         for (int i = 0; i < 5; i++)
    //         {
    //             Instantiate(myPrefab, new Vector3(i * 100, 0, k * 100), Quaternion.identity);
    //         }
    //     }

    //     // Instantiate at position (0, 0, 0) and zero rotation.

    // }
}
