using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Reference to the obstacle prefab and player transform
    public GameObject obstaclePrefab;
    public Transform player;

    // Parameters for obstacle spawning
    public float spawnDistance = 40f;
    public float spawnInterval = 2f;

    // Timer to track time between spawns
    float timer;

    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time elapsed since the last frame

        // Check if the timer has exceeded the spawn interval
        if (timer > spawnInterval)
        {
            SpawnObstacle(); // Spawn a new obstacle
            timer = 0; // Reset the timer after spawning
        }
    }

    // Method to spawn an obstacle at a random lane
    void SpawnObstacle()
    {
        int lane = Random.Range(0, 3); // Randomly select a lane (0, 1, or 2)
        float x = (lane - 1) * 2; // Calculate the x position based on the lane (left, center, right)
        Vector3 pos = new Vector3(x, 1, player.position.z + spawnDistance); // Set the position of the obstacle in front of the player at a certain distance
        GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity); // Instantiate the obstacle prefab at the calculated position with no rotation
    }
}