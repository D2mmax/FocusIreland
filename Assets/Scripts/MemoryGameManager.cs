using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Item_MiniGame")]
    public Card cardPrefab;
    public Transform grid;
    public Sprite[] fruits;
    public Sprite cardBack;

    [Header("UI")]
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Timer")]
    public float timeLimit = 40f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flipSound;
    public AudioClip matchSound;
    public AudioClip wrongSound;

    [Header("Scene Transition")]
    public string sceneToLoadOnComplete = "SchoolScene";
    public int scoreToComplete = 0;

    private List<Card> flipped = new List<Card>();
    public bool lockBoard = false;

    private int totalPairs;
    private int matchedPairs;
    private float timeRemaining;
    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timeRemaining = timeLimit;

        List<Sprite> cards = new List<Sprite>();

        foreach (Sprite fruit in fruits)
        {
            cards.Add(fruit);
            cards.Add(fruit);
        }

        totalPairs = fruits.Length;
        matchedPairs = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            Sprite temp = cards[i];
            int rand = Random.Range(i, cards.Count);
            cards[i] = cards[rand];
            cards[rand] = temp;
        }

        foreach (Sprite sprite in cards)
        {
            Card card = Instantiate(cardPrefab, grid);
            card.frontSprite = sprite;
            card.backSprite = cardBack;
        }
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(Mathf.Max(timeRemaining, 0f));

        if (timeRemaining <= 0f)
        {
            gameOver = true;
            StartCoroutine(FailGame());
        }
    }

    public void CardFlipped(Card card)
    {
        if (gameOver) return;

        flipped.Add(card);

        if (audioSource != null && flipSound != null)
            audioSource.PlayOneShot(flipSound);

        if (flipped.Count == 2)
            StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        lockBoard = true;
        yield return new WaitForSeconds(0.7f);

        if (flipped[0].frontSprite == flipped[1].frontSprite)
        {
            matchedPairs++;
            AddScore(10);

            if (audioSource != null && matchSound != null)
                audioSource.PlayOneShot(matchSound);

            if (matchedPairs >= totalPairs)
            {
                gameOver = true;
                StartCoroutine(CompleteGame());
            }
        }
        else
        {
            if (audioSource != null && wrongSound != null)
                audioSource.PlayOneShot(wrongSound);

            flipped[0].Hide();
            flipped[1].Hide();
        }

        flipped.Clear();
        lockBoard = false;
    }

    IEnumerator CompleteGame()
    {
        MinigameResult.hasPlayed = true;
        MinigameResult.passed = true;
        yield return new WaitForSeconds(1f);
        LoadNextScene();
    }

    IEnumerator FailGame()
    {
        MinigameResult.hasPlayed = true;
        MinigameResult.passed = false;
        yield return new WaitForSeconds(1f);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(sceneToLoadOnComplete);
        else
            SceneManager.LoadScene(sceneToLoadOnComplete);
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (scoreToComplete > 0 && score >= scoreToComplete)
        {
            gameOver = true;
            StartCoroutine(CompleteGame());
        }
    }
}
