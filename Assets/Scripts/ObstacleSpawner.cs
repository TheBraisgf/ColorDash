using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f; // Tiempo entre cada obstáculo
    public float spawnXMin = -3f, spawnXMax = 3f; // Posiciones aleatorias en X
    public float spawnY = 6f; // Altura fija de spawn

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
