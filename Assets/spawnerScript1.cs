using System.Runtime.CompilerServices;
using UnityEngine;

public class spawnerScript1 : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    public float obstaclesSpawnTimer = 2f;
    private float timeUntilOblasceSpawn;


    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()

    {
        timeUntilOblasceSpawn -= Time.deltaTime;
        if (timeUntilOblasceSpawn >= obstacelsSpawnTimer)
        {
            Spawn();
        }


    }
    private void Spawn()
    {

    }



}

















