using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject pauseMenuPanel;
    public GameObject resumeButton;
    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start(){
        Button btn = resumeButton.GetComponent<Button>();
        btn.onClick.AddListener(() => {StartCoroutine(Resume());});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                StartCoroutine(Resume());
                return;
            }
            StartCoroutine(Pause());
        }
    }

    private IEnumerator Pause()
    {
        Debug.Log("Paused the game");
        gamePaused = true;
        ScoreManager.instance.PauseTimer();
        pauseMenuPanel.SetActive(true);
        yield return StartCoroutine(FadeIn());
        Time.timeScale = 0.0f;
    }

    private IEnumerator Resume()
    {
        Debug.Log("Resumed the game");
        gamePaused = false;
        Time.timeScale = 1.0f;
        yield return StartCoroutine(FadeOut());
        pauseMenuPanel.SetActive(false);
        ScoreManager.instance.StartTimer();
    }

    private IEnumerator FadeIn()
    {
        float durationSeconds = 0.5f;
        float timeElapsed = 0.0f;
        CanvasGroup canvasGroup = pauseMenuCanvas.GetComponent<CanvasGroup>();

        while (timeElapsed < durationSeconds)
        {
            float t = EasingFunctions.EaseInOutSine(0, 1, timeElapsed / durationSeconds);
            canvasGroup.alpha = t;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    private IEnumerator FadeOut()
    {
        float durationSeconds = 0.5f;
        float timeElapsed = 0.0f;
        CanvasGroup canvasGroup = pauseMenuCanvas.GetComponent<CanvasGroup>();

        while (timeElapsed < durationSeconds)
        {
            float t = EasingFunctions.EaseInOutSine(0, 1, timeElapsed / durationSeconds);
            canvasGroup.alpha = 1.0f - t;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
