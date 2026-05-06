using UnityEngine;

public class MovingHoop : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float leftLimit = -4f;
    public float rightLimit = 4f;

    private bool isMoving = false;
    private int direction = 1;

    void Update()
    {
        // Start moving after score reaches 10
        if (!isMoving && ScoreManager.instance.score >= 10)
        {
            isMoving = true;
        }

        if (!isMoving) return;

        MoveHoop();
    }

    void MoveHoop()
    {
        Vector3 pos = transform.position;

        pos.x += direction * moveSpeed * Time.deltaTime;

        // Clamp + reverse direction
        if (pos.x >= rightLimit)
        {
            pos.x = rightLimit;
            direction = -1;
        }
        else if (pos.x <= leftLimit)
        {
            pos.x = leftLimit;
            direction = 1;
        }

        transform.position = pos;
    }
}