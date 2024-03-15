using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionInterface : MonoBehaviour
{
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
        GlobalProviders.instance.currentLevel = GlobalProviders.instance.levels[level];
        SceneManager.LoadScene("Game-01");
    }
}
