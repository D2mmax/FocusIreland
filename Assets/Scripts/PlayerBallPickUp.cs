using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerBallPickup : MonoBehaviour
{
    public static bool PlayerHasBall = false;

    public GameObject ballPrefab;
    public GameObject ballIndicator;
    public Transform shootPoint;

    [Header("Trajectory")]
    public GameObject dotPrefab;
    public int dotCount = 15;
    public float dotSpacing = 0.1f;
    public float forceMultiplier = 10f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    private List<GameObject> dots = new List<GameObject>();

    private bool canShoot = false;
    private bool isHolding = false;

    void Start()
    {
        ballIndicator.SetActive(false);

        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, shootPoint.position, Quaternion.identity);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }

    void Update()
    {
        if (!canShoot) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isHolding = true;
        }

        if (isHolding)
        {
            ShowTrajectory();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isHolding)
        {
            ShootBall();
            isHolding = false;
            HideDots();
        }
    }

    void ShowTrajectory()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - shootPoint.position);
        Vector2 velocity = direction * forceMultiplier;

        for (int i = 0; i < dots.Count; i++)
        {
            float t = i * dotSpacing;

            Vector2 pos = (Vector2)shootPoint.position +
                          velocity * t +
                          0.5f * Physics2D.gravity * t * t;

            dots[i].transform.position = pos;
            dots[i].SetActive(true);
        }
    }

    void HideDots()
    {
        foreach (var dot in dots)
        {
            dot.SetActive(false);
        }
    }

    void ShootBall()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - shootPoint.position);

        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * forceMultiplier, ForceMode2D.Impulse);

        // 🔊 SHOOT SOUND
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        canShoot = false;
        PlayerHasBall = false;

        ballIndicator.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            canShoot = true;
            PlayerHasBall = true;

            ballIndicator.SetActive(true);

            Destroy(collision.gameObject);
        }
    }
}