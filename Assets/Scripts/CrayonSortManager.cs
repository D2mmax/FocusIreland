using System.Collections;
using UnityEngine;
using TMPro;

public class CrayonSortManager : MonoBehaviour
{
    public static CrayonSortManager Instance;

    [Header("Settings")]
    public int totalCrayons = 12;

    [Header("UI")]
    public TextMeshProUGUI completionText;

    [Header("Scene Transition")]
    public string sceneToLoadOnComplete = "ShelterScene";
    public float completionDelay = 2f;

    private int crayonsSorted = 0;
    private bool complete = false;

    void Awake()
    {
        Instance = this;
    }

    public void OnCrayonSorted()
    {
        if (complete) return;

        crayonsSorted++;

        if (crayonsSorted >= totalCrayons)
            StartCoroutine(CompleteMinigame());
    }

    IEnumerator CompleteMinigame()
    {
        complete = true;

        if (completionText != null)
        {
            completionText.text = "All sorted!";
            completionText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(completionDelay);

        DayFlags.crayonSortCompleted = true;
        DayFlags.shelterState = 2;

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(sceneToLoadOnComplete);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadOnComplete);
    }
}
