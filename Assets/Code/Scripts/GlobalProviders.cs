using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        // WHENEVER THIS SCENE GETS LOADED AGAIN (WHEN A LEVEL ENDS), THIS OBJECT GETS DUPLICATED
        // AND IN THAT CASE, I DESTROY THE OLD OBJECT
        // (THIS IS UGLY AND NEEDS TO BE DONE IN A BETTER WAY)
        if (instance != null && instance != this)
        {
            Debug.Log("GlobalProviders object duplicated");
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Button btn = GameObject.Find("Level " + (i + 1)).GetComponent<Button>();
            int veryHelpfulVariable = i;
            btn.onClick.AddListener(() => LoadLevel(veryHelpfulVariable));
        }
    }

    private void LoadLevel(int level)
    {
        Debug.Log("Loading level " + (level + 1));
        currentLevel = levels[level];
        SceneManager.LoadScene("Game-01");
    }
}
