using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSkipManager : MonoBehaviour
{
    public static TimeSkipManager Instance { get; private set; }

    [Header("UI")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI skipText;

    public float fadeDuration = 0.4f;
    public float holdDuration = 2f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show(string text, System.Action onComplete)
    {
        StartCoroutine(RunTimeSkip(text, onComplete));
    }

    IEnumerator RunTimeSkip(string text, System.Action onComplete)
    {
        skipText.text = text;
        canvasGroup.blocksRaycasts = true;

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Hold
        yield return new WaitForSeconds(holdDuration);

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));

        canvasGroup.blocksRaycasts = false;
        onComplete?.Invoke();
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(from, to, Mathf.Clamp01(t));
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
