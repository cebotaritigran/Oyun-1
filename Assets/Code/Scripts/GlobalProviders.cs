using System.Collections.Generic;
using UnityEngine;

public class GlobalProviders : MonoBehaviour
{
    public static GlobalProviders instance;

    public List<Level> levels = new List<Level>()
    {
        new Level(1, 3, 2), // 6
        new Level(2, 4, 2), // 8 (+2)
        new Level(3, 4, 3), // 12 (+4)
        new Level(4, 4, 4), // 16 (+4)
        new Level(5, 11, 2), // 22 (+6)
        new Level(6, 7, 4), // 28 (+6)
        new Level(7, 6, 6), // 36 (+8)
        new Level(8, 11, 4), // 44 (+8)
        new Level(9, 9, 6), // 54 (+10)
        new Level(10, 8, 8), // 64 (+10)
    };

    public Level currentLevel;

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;
        currentLevel = levels[0];
    }
}
