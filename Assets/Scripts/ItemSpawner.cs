using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnRate = 1f;
    public float spawnWidth = 8f;
    public float spawnHeight = 6f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 1f, spawnRate);
    }

    void SpawnItem()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector3 spawnPos = new Vector3(randomX, spawnHeight, 0f);

        Instantiate(itemPrefab, spawnPos, Quaternion.identity);
    }
}