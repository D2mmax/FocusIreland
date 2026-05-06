using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnWidth = 8f;
    public float spawnHeight = 6f;

    private GameObject currentBall;

    void Update()
    {
        if (currentBall == null)
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector3 spawnPos = new Vector3(randomX, spawnHeight, 0f);

        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }
}