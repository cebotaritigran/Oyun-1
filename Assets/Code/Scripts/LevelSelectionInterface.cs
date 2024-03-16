using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionInterface : MonoBehaviour
{
    public GameObject mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
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

    private IEnumerator FadeIn()
    {
        float durationSeconds = 1.5f;
        float timeElapsed = 0.0f;
        CanvasGroup canvasGroup = mainCanvas.GetComponent<CanvasGroup>();

        while (timeElapsed < durationSeconds)
        {
            float t = EasingFunctions.EaseInOutSine(0, 1, timeElapsed / durationSeconds);
            canvasGroup.alpha = t;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }
}
