using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnDelay = 1.0f;
    public float maxSpawnDelay = 3.0f;
    public float minYSpawn = -4.0f;
    public float maxYSpawn = 4.0f;
    
    private float _nextSpawnTime;

    void Start()
    {
        // Set initial spawn time
        _nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }

    void SpawnEnemy()
    {
        // Randomize the Y position of the spawn within minYSpawn and maxYSpawn
        float randomY = Random.Range(minYSpawn, maxYSpawn);

        // Spawn the enemy at the spawn point's position
        Vector3 spawnPosition = transform.position + new Vector3(0f, randomY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}