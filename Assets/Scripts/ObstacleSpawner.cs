using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float spawnDistance = 40f;
    public float spawnInterval = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        int lane = Random.Range(0, 3);
        float x = (lane - 1) * 2;

        Vector3 pos = new Vector3(x, 1, player.position.z + spawnDistance);

        GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}