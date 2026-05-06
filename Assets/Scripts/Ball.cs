using UnityEngine;

public class Ball : MonoBehaviour
{
    public float lifetimeAfterFirstBounce = 2f;

    private bool hasHitGround = false;
    private float timer = 0f;

    void Update()
    {
        if (hasHitGround)
        {
            timer += Time.deltaTime;

            if (timer >= lifetimeAfterFirstBounce)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasHitGround && collision.gameObject.CompareTag("Ground"))
        {
            hasHitGround = true;
        }
    }

    void OnDestroy()
    {
        BallSpawner spawner = FindObjectOfType<BallSpawner>();

        if (spawner != null)
        {
            spawner.ClearBall();
        }
    }
}