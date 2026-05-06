using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;

    private GameObject currentBall;

    [Header("Anti-spam")]
    public float spawnDelay = 0.5f;
    private float spawnTimer = 0f;
    private bool canSpawn = true;

    void Update()
    {
        if (!canSpawn)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay)
            {
                canSpawn = true;
                spawnTimer = 0f;
            }

            return;
        }

        if (currentBall == null && !PlayerBallPickup.PlayerHasBall)
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        canSpawn = false; // lock spawn immediately

        currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void ClearBall()
    {
        currentBall = null;
    }
}