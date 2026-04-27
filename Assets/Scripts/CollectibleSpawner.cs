using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab; // Reference to the collectible prefab
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] Transform player;

    private float spawnDistance;
    private float spawnInterval;

    float timer;

    private void Update()
    {
        spawnDistance = obstacleSpawner.GetSpawnDistance(); // Get the spawn distance from the ObstacleSpawner
        spawnInterval = obstacleSpawner.GetSpawnInterval(); // Set the spawn interval to be 1.5 times the obstacle spawn interval

        timer += Time.deltaTime; // Increment the timer by the time elapsed since the last frame
        if (timer > spawnInterval) 
        { 
            SpawnCollectible();
            timer = 0; // Reset the timer after spawning
        }
    }

    // Method to spawn a collectible at a random lane
    public void SpawnCollectible()
    {
        int lane = Random.Range(0, 3); // Randomly select a lane (0, 1, or 2)
        float x = (lane - 1) * 2; // Calculate the x position based on the lane (left, center, right)
        Vector3 pos = new Vector3(x, 1, player.position.z + spawnDistance); // Set the position of the collectible in front of the player at a certain distance
        if (Physics.CheckSphere(pos, 0.1f)) return; // Check if there is already an object at the spawn position using a sphere check, and if so, do not spawn a collectible
        GameObject obj = Instantiate(collectiblePrefab, pos, Quaternion.identity); // Instantiate the collectible prefab at the calculated position with no rotation
    }

    // Gizmos to visualize the spawn points for collectibles in the editor (commented out for now)
    /*private void OnDrawGizmos()
    {
        if (player == null) return;
        Gizmos.color = Color.yellow;
        for (int lane = 0; lane < 3; lane++)
        {
            float x = (lane - 1) * 2;
            Vector3 pos = new Vector3(x, 1, player.position.z + spawnDistance);
            Gizmos.DrawWireSphere(pos, 0.3f);
        }
    }*/
}