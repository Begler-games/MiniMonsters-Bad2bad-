using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;  // Assign your enemy prefab here
    public int maxEnemies = 10;     // Maximum number of enemies to spawn
    public float spawnInterval = 3f; // Time between spawns in seconds
    public Vector2 spawnAreaMin;    // Bottom-left corner of the spawn area
    public Vector2 spawnAreaMax;    // Top-right corner of the spawn area
    public Tilemap tilemap;         // Reference to the Tilemap object

    private int currentEnemies = 0;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies)
        {
            Vector3Int spawnCellPosition;
            Vector3 spawnWorldPosition;
            do
            {
                Vector2 spawnPosition = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                spawnWorldPosition = spawnPosition;
                spawnCellPosition = tilemap.WorldToCell(spawnWorldPosition);

            } while (!tilemap.HasTile(spawnCellPosition));

            Instantiate(enemyPrefab, spawnWorldPosition, Quaternion.identity);
            currentEnemies++;
        }
    }
}
