using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    public GameObject platePrefab; // The plate prefab to spawn
    public float spawnDelay = 5.0f; // The delay between plate spawns
    public Transform[] spawnPoints; // The array of spawn points to choose from

    private float timeSinceLastSpawn; // The time elapsed since the last plate was spawned

    void Update()
    {
        // Increment the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new plate
        if (timeSinceLastSpawn >= spawnDelay)
        {
            // Reset the time since the last spawn
            timeSinceLastSpawn = 0.0f;

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn a new plate at the chosen spawn point
            Instantiate(platePrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.yellow;

        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawCube(spawnPoint.position, Vector3.one * 0.1f);
        }
    }
}