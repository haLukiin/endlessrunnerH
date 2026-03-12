

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnInterval = 2f;
    public float heightRange = 2.5f;

    public float spawnX = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        float randomY = Random.Range(-heightRange, heightRange);

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}