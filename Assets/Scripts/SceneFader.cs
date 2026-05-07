using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    public float fadeDuration = 0.5f;

    [Header("Timeskip Text (optional)")]
    public TextMeshProUGUI timeskipText;

    private Image fadeImage;
    private bool isFading = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        if (Instance != null) return;
        GameObject prefab = Resources.Load<GameObject>("SceneFader");
        if (prefab != null)
            Instantiate(prefab);
        else
            Debug.LogWarning("SceneFader: No prefab found in Resources/SceneFader. Add it to Assets/Resources/.");
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        fadeImage = GetComponentInChildren<Image>();
        SetAlpha(0f);
        fadeImage.raycastTarget = false;
        if (timeskipText != null) timeskipText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInDelayed());
    }

    IEnumerator FadeInDelayed()
    {
        SetAlpha(1f);
        yield return null;
        yield return null;
        yield return null;
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string sceneName)
    {
        if (!isFading)
            StartCoroutine(FadeOutAndLoad(sceneName));
    }

    public void FadeToBlackWithText(string text, float holdDuration, System.Action onBlack)
    {
        if (!isFading)
            StartCoroutine(RunTimeskip(text, holdDuration, onBlack));
    }

    IEnumerator RunTimeskip(string text, float holdDuration, System.Action onBlack)
    {
        isFading = true;
        fadeImage.raycastTarget = true;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            SetAlpha(Mathf.Clamp01(t));
            yield return null;
        }
        SetAlpha(1f);

        if (timeskipText != null)
        {
            timeskipText.text = text;
            timeskipText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(holdDuration);

        onBlack?.Invoke();

        yield return null;
        yield return null;

        if (timeskipText != null) timeskipText.gameObject.SetActive(false);

        isFading = false;
        fadeImage.raycastTarget = false;
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        isFading = true;
        fadeImage.raycastTarget = false;
        SetAlpha(1f);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            SetAlpha(1f - Mathf.Clamp01(t));
            yield return null;
        }
        SetAlpha(0f);
        isFading = false;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        isFading = true;
        fadeImage.raycastTarget = true;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            SetAlpha(Mathf.Clamp01(t));
            yield return null;
        }
        SetAlpha(1f);
        isFading = false;
        SceneManager.LoadScene(sceneName);
    }

    void SetAlpha(float alpha)
    {
        if (fadeImage) fadeImage.color = new Color(0f, 0f, 0f, alpha);
    }

    public void ForceReady()
    {
        isFading = false;
        StopAllCoroutines();
    }
}
