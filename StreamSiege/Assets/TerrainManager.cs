using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform player;

    public List<GameObject> groundChunks;

    public float chunkSize = 100f;

    public int viewDistance = 2;

    private Dictionary<Vector2Int, GameObject>
        spawnedChunks =
        new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        UpdateChunks();
    }

    void Update()
    {
        UpdateChunks();
    }

    void UpdateChunks()
    {
        int playerChunkX =
            Mathf.FloorToInt(
                player.position.x / chunkSize);

        int playerChunkZ =
            Mathf.FloorToInt(
                player.position.z / chunkSize);

        HashSet<Vector2Int> neededChunks =
            new HashSet<Vector2Int>();

        for (int x = -viewDistance;
            x <= viewDistance;
            x++)
        {
            for (int z = -viewDistance;
                z <= viewDistance;
                z++)
            {
                Vector2Int coord =
                    new Vector2Int(
                        playerChunkX + x,
                        playerChunkZ + z);

                neededChunks.Add(coord);

                if (!spawnedChunks.ContainsKey(coord))
                {
                    SpawnChunk(coord);
                }
            }
        }

        RemoveFarChunks(neededChunks);
    }

    void SpawnChunk(Vector2Int coord)
    {
        int randomIndex =
            Random.Range(0, groundChunks.Count);

        GameObject prefab =
            groundChunks[randomIndex];

        Vector3 pos =
            new Vector3(
                coord.x * chunkSize + chunkSize / 2f,
                0,
                coord.y * chunkSize + chunkSize / 2f);

        GameObject chunk =
            MyPoolManager.Instance.Get(
                prefab,
                pos,
                Quaternion.identity);

        spawnedChunks.Add(coord, chunk);
    }

    void RemoveFarChunks(
        HashSet<Vector2Int> neededChunks)
    {
        List<Vector2Int> removeList =
            new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            if (!neededChunks.Contains(chunk.Key))
            {
                chunk.Value
                    .GetComponent<PoolObject>()
                    .Release();

                removeList.Add(chunk.Key);
            }
        }

        foreach (var coord in removeList)
        {
            spawnedChunks.Remove(coord);
        }
    }
}