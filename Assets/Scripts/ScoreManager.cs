using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    public int maxScore = 16;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        // Clamp so it never exceeds max
        if (score > maxScore)
            score = maxScore;

        UpdateUI();

        Debug.Log("Score: " + score);
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score + " / " + maxScore;
        }
    }
}