using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array con los 4 prefabs de obstáculos
    public float spawnRate = 2f; // Tiempo inicial entre obstáculos
    public float minSpawnRate = 0.5f; // Límite mínimo de spawn
    private float difficultyMultiplier = 0.01f; // Cuánto se reduce el tiempo de spawn con el tiempo
    public float spawnXMin = -2.5f, spawnXMax = 2.5f;
    public float spawnY = 6f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Elegir un tipo de obstáculo aleatorio
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Reducir el tiempo de spawn con el tiempo
        spawnRate -= difficultyMultiplier;
        spawnRate = Mathf.Max(spawnRate, minSpawnRate); // Evitar que sea menor al mínimo

        // Reiniciar el spawn con el nuevo tiempo ajustado
        CancelInvoke();
        InvokeRepeating("SpawnObstacle", spawnRate, spawnRate);
    }
}
