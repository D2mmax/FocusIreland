using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBallPickup : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject ballIndicator;
    public Transform shootPoint;

    public float power = 10f;

    private bool canShoot = false;

    void Start()
    {
        ballIndicator.SetActive(false);
    }

    void Update()
    {
        if (!canShoot) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - shootPoint.position).normalized;

        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);

        canShoot = false;

        ballIndicator.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            canShoot = true;

            ballIndicator.SetActive(true);

            Destroy(collision.gameObject);
        }
    }
}