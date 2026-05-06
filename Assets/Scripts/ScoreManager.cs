using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI completionText;

    [Header("Score Settings")]
    public int maxScore = 16;

    [Header("Scene Transition")]
    public string sceneToLoadOnComplete = "ShelterScene";
    public float completionDelay = 2f;

    private bool gameOver = false;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        if (gameOver) return;

        score += amount;

        if (score > maxScore)
            score = maxScore;

        UpdateUI();

        if (score >= maxScore)
            StartCoroutine(CompleteGame());
    }

    IEnumerator CompleteGame()
    {
        gameOver = true;

        if (completionText != null)
        {
            completionText.text = "Nice game!";
            completionText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(completionDelay);

        DayFlags.basketballCompleted = true;
        DayFlags.shelterState = 1;

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(sceneToLoadOnComplete);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadOnComplete);
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score + " / " + maxScore;
    }
}
