using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public const int gridRows = 4;
    public const int gridCols = 2;
    public const float offsetX = 100f;
    public const float offsetY = 100f;

    public GameObject myPrefab;
    private void Start()
    {
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        for (int i = 0; i < gridCols; i++)
        {
            for (int k = 0; k < gridRows; k++)
            {
                Instantiate(myPrefab, new Vector3(i * offsetX, 0, k * offsetY), Quaternion.identity);
            }
        }

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
