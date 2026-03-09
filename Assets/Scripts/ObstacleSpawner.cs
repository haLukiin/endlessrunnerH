using UnityEngine;

/// <summary>
/// Spawns pipe-pair obstacles at regular intervals and scrolls them leftward.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private GameObject obstaclePairPrefab;
    [SerializeField] private float spawnInterval = 2.2f;
    [SerializeField] private float spawnX = 8f;
    [SerializeField] private float gapCenterMinY = -1.5f;
    [SerializeField] private float gapCenterMaxY = 1.5f;

    private float spawnTimer;
    private bool isRunning;

    private void Start()
    {
        spawnTimer = spawnInterval * 0.5f; // spawn first pair sooner
    }

    private void Update()
    {
        if (!isRunning) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnPair();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnPair()
    {
        float gapCenterY = Random.Range(gapCenterMinY, gapCenterMaxY);
        Vector3 spawnPosition = new Vector3(spawnX, gapCenterY, 0f);
        Instantiate(obstaclePairPrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Starts spawning obstacles.
    /// </summary>
    public void StartSpawning()
    {
        isRunning = true;
        spawnTimer = spawnInterval * 0.5f;
    }

    /// <summary>
    /// Stops spawning obstacles.
    /// </summary>
    public void StopSpawning()
    {
        isRunning = false;
    }
}
