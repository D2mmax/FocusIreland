using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBallPickup : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject ballIndicator;
    public Transform shootPoint;
    public Transform arrow;

    public float power = 10f;

    private bool canShoot = false;

    void Start()
    {
        ballIndicator.SetActive(false);
        arrow.gameObject.SetActive(false);
    }

    void Update()
    {
        // ✅ NEW INPUT SYSTEM CLICK
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("CLICK DETECTED");

            if (canShoot)
            {
                ShootBall();
            }
        }

        if (canShoot)
        {
            AimArrow();
        }
    }

    void AimArrow()
    {
        if (Camera.main == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = mousePos - shootPoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ShootBall()
    {
        Debug.Log("SHOOTING BALL");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - shootPoint.position).normalized;

        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);

        canShoot = false;

        ballIndicator.SetActive(false);
        arrow.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("PICKED UP BALL");

            canShoot = true;

            ballIndicator.SetActive(true);
            arrow.gameObject.SetActive(true);

            Destroy(collision.gameObject);
        }
    }
}