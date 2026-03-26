using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Card cardPrefab;
    public Transform grid;
    public Sprite[] fruits;
    public Sprite cardBack;

    private List<Card> flipped = new List<Card>();
    public bool lockBoard = false;

    void Start()
    {
        List<Sprite> cards = new List<Sprite>();

        foreach (Sprite fruit in fruits)
        {
            cards.Add(fruit);
            cards.Add(fruit);
        }

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

        if (flipped[0].frontSprite != flipped[1].frontSprite)
        {
            flipped[0].Hide();
            flipped[1].Hide();
        }

        flipped.Clear();
        lockBoard = false;
    }
}