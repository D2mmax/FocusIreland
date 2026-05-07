using System.Collections;
using UnityEngine;
using TMPro;

public class IntroController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private float holdPerLine = 2.5f;
    [SerializeField] private float textFadeDuration = 0.5f;

    private string[] lines = new string[]
    {
        "Every night in Ireland, over 4,000 people sleep in emergency accommodation.",
        "More than 1,500 of them are children.",
        "This is one day of one of their lives."
    };

    IEnumerator Start()
    {
        introText.text = "";
        introText.color = new Color(1f, 1f, 1f, 0f);

        yield return new WaitForSeconds(0.5f);

        foreach (string line in lines)
        {
            introText.text = line;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / textFadeDuration;
                introText.color = new Color(1f, 1f, 1f, Mathf.Clamp01(t));
                yield return null;
            }

            yield return new WaitForSeconds(holdPerLine);

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / textFadeDuration;
                introText.color = new Color(1f, 1f, 1f, 1f - Mathf.Clamp01(t));
                yield return null;
            }

            yield return new WaitForSeconds(0.3f);
        }

        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.ForceReady();
            SceneFader.Instance.FadeTo("ShelterScene");
        }
    }
}
