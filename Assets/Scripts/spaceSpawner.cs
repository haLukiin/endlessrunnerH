using UnityEngine;

public class SpaceSpawner : MonoBehaviour
{
    public GameObject[] spaceObjects;

    public float minX = 18f;
    public float maxX = 22f;

    public float minY = -5f;
    public float maxY = 5f;

    public float minSpawnDelay = 1.5f;
    public float maxSpawnDelay = 4f;

    public float minScale = 0.7f;
    public float maxScale = 1.5f;

    void Start()
    {
        SpawnLoop();
    }

    void SpawnLoop()
    {
        SpawnObject();
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(SpawnLoop), delay);
    }

    void SpawnObject()
    {
        int index = Random.Range(0, spaceObjects.Length);
        GameObject prefab = spaceObjects[index];

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(x, y, 0);

        // Spawn using prefab's own rotation
        GameObject obj = Instantiate(prefab, spawnPos, prefab.transform.rotation);

        float scale = Random.Range(minScale, maxScale);
        obj.transform.localScale = Vector3.one * scale;
    }
}