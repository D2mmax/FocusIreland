using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    public float fadeDuration = 0.5f;

    private Image fadeImage;
    private bool isFading = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        fadeImage = GetComponentInChildren<Image>();
        SetAlpha(0f);
        fadeImage.raycastTarget = false;
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
    if (scene.name != "IntroScene" && scene.name != "StartScene")
        StartCoroutine(FadeIn());
}

    public void FadeTo(string sceneName)
{
    Debug.Log("[SceneFader] FadeTo called. isFading: " + isFading + " scene: " + sceneName);
    if (!isFading)
        StartCoroutine(FadeOutAndLoad(sceneName));
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