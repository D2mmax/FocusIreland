using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClosingSequence : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI lineOne;
    public TextMeshProUGUI lineTwo;
    public TextMeshProUGUI lineThree;
    public TextMeshProUGUI lineFour;

    [Header("Timing")]
    public float fadeInDuration = 1.5f;
    public float holdDuration = 4f;
    public float fadeOutDuration = 1f;
    public float delayBetweenLines = 1.5f;

    [Header("Scene")]
    public string mainMenuScene = "StartScene";

    void Start()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;

        if (lineOne != null) lineOne.gameObject.SetActive(false);
        if (lineTwo != null) lineTwo.gameObject.SetActive(false);
        if (lineThree != null) lineThree.gameObject.SetActive(false);
        if (lineFour != null) lineFour.gameObject.SetActive(false);
    }

    public void StartClosingSequence()
    {
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        // Fade to black
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeInDuration;
            if (canvasGroup != null) canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        if (canvasGroup != null) canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        // Show THE END
        if (lineOne != null) lineOne.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenLines);

        // Show statistic line
        if (lineTwo != null) lineTwo.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenLines);

        // Show Focus Ireland tagline
        if (lineThree != null) lineThree.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenLines);

        // Show website
        if (lineFour != null) lineFour.gameObject.SetActive(true);

        yield return new WaitForSeconds(holdDuration);

        // Fade out and load main menu
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeOutDuration;
            if (canvasGroup != null) canvasGroup.alpha = 1f - Mathf.Clamp01(t);
            yield return null;
        }

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(mainMenuScene);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }
}
