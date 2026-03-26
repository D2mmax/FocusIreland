using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image image;
    public Sprite frontSprite;
    public Sprite backSprite;

    private GameManager manager;
    private bool revealed = false;

    void Start()
    {
        manager = GameObject.FindFirstObjectByType<GameManager>();
        image.sprite = backSprite;
        GetComponent<Button>().onClick.AddListener(Flip);
    }

    void Flip()
    {
        if (revealed || manager.lockBoard) return;

        revealed = true;
        image.sprite = frontSprite;

        manager.CardFlipped(this);
    }

    public void Hide()
    {
        revealed = false;
        image.sprite = backSprite;
    }
}