using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnTime);

            GameObject obstaclePrefab =
                obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnWidth, spawnWidth),
                transform.position.y,
                transform.position.z
            );

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, obstacleParent);
        }
    }
}
