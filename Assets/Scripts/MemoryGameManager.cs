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

    [Header("Scene Transition")]
    [Tooltip("Scene to load when the player completes the minigame")]
    public string sceneToLoadOnComplete = "SchoolScene";

    [Tooltip("Score needed to complete the minigame. Set to 0 to disable score-based completion.")]
    public int scoreToComplete = 0;

    private List<Card> flipped = new List<Card>();
    public bool lockBoard = false;

    private int totalPairs;
    private int matchedPairs;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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

    public void CardFlipped(Card card)
    {
        flipped.Add(card);

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
            Debug.Log("Matched pairs: " + matchedPairs + " / " + totalPairs);

            if (matchedPairs >= totalPairs)
                StartCoroutine(CompleteGame());
        }
        else
        {
            flipped[0].Hide();
            flipped[1].Hide();
        }

        flipped.Clear();
        lockBoard = false;
    }

    IEnumerator CompleteGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoadOnComplete);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (scoreToComplete > 0 && score >= scoreToComplete)
            StartCoroutine(CompleteGame());
    }
}
