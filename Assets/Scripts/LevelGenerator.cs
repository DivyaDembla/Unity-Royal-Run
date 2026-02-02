using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 12f;

    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    // ---------------- SPAWN ----------------

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();

        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            spawnPositionZ
        );

        GameObject newChunk = Instantiate(
            chunkPrefab,
            spawnPosition,
            Quaternion.identity,
            chunkParent
        );

        chunks.Add(newChunk);
    }

    float CalculateSpawnPositionZ()
    {
        if (chunks.Count == 0)
        {
            return transform.position.z;
        }

        return chunks[chunks.Count - 1].transform.position.z + chunkLength;
    }

    // ---------------- MOVE & DESTROY ----------------

    void MoveChunks()
    {
        for (int i = chunks.Count - 1; i >= 0; i--)
        {
            GameObject chunk = chunks[i];

            chunk.transform.Translate(
                -transform.forward * moveSpeed * Time.deltaTime
            );

            if (chunk.transform.position.z <=
                Camera.main.transform.position.z - chunkLength)
            {
                chunks.RemoveAt(i);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
