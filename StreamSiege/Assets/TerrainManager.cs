using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Chunk Prefabs")]
    public List<GameObject> groundChunks;

    [Header("Chunk Settings")]
    public float chunkSize = 100f;

    [Tooltip("Despawn Chunk")]
    public int removeDistance = 3;

    // =========================================================
    // DATA
    // =========================================================

    private readonly Dictionary<Vector2Int, GameObject> spawnedChunks =new Dictionary<Vector2Int, GameObject>();

    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        Vector2Int startChunk = GetChunkCoord(player.position);

        SpawnChunk(startChunk);

        SpawnNeighborChunks(startChunk);
    }

    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        Vector2Int currentChunk = GetChunkCoord(player.position);

        CheckHalfChunkSpawn(currentChunk);

        RemoveFarChunks(currentChunk);
    }

    // =========================================================
    // GET CHUNK COORD
    // =========================================================

    private Vector2Int GetChunkCoord(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / chunkSize);

        int z = Mathf.FloorToInt(pos.z / chunkSize);

        return new Vector2Int(x, z);
    }

    // =========================================================
    // CHECK HALF CHUNK SPAWN
    // =========================================================

    private void CheckHalfChunkSpawn(Vector2Int currentChunk)
    {
        Vector3 chunkCenter = new Vector3(currentChunk.x * chunkSize + chunkSize * 0.5f,0f,currentChunk.y * chunkSize + chunkSize * 0.5f);

        float localX = player.position.x - chunkCenter.x;

        float localZ = player.position.z - chunkCenter.z;

        // =====================================================
        // RIGHT
        // =====================================================

        if (localX > 0)
        {
            SpawnChunk(currentChunk + Vector2Int.right);
        }

        // =====================================================
        // LEFT
        // =====================================================

        if (localX < 0)
        {
            SpawnChunk(currentChunk + Vector2Int.left);
        }

        // =====================================================
        // UP
        // =====================================================

        if (localZ > 0)
        {
            SpawnChunk(currentChunk + Vector2Int.up);
        }

        // =====================================================
        // DOWN
        // =====================================================

        if (localZ < 0)
        {
            SpawnChunk(currentChunk + Vector2Int.down);
        }

        // =====================================================
        // DIAGONALS
        // =====================================================

        if (localX > 0 && localZ > 0)
        {
            SpawnChunk(currentChunk + new Vector2Int(1, 1));
        }

        if (localX < 0 && localZ > 0)
        {
            SpawnChunk(currentChunk + new Vector2Int(-1, 1));
        }

        if (localX > 0 && localZ < 0)
        {
            SpawnChunk(currentChunk + new Vector2Int(1, -1));
        }

        if (localX < 0 && localZ < 0)
        {
            SpawnChunk(currentChunk + new Vector2Int(-1, -1));
        }
    }

    // =========================================================
    // SPAWN NEIGHBORS
    // =========================================================

    private void SpawnNeighborChunks(Vector2Int center)
    {
        SpawnChunk(center + Vector2Int.right);
        SpawnChunk(center + Vector2Int.left);
        SpawnChunk(center + Vector2Int.up);
        SpawnChunk(center + Vector2Int.down);

        SpawnChunk(center + new Vector2Int(1, 1));
        SpawnChunk(center + new Vector2Int(-1, 1));
        SpawnChunk(center + new Vector2Int(1, -1));
        SpawnChunk(center + new Vector2Int(-1, -1));
    }

    // =========================================================
    // SPAWN CHUNK
    // =========================================================

    private void SpawnChunk(Vector2Int coord)
    {
        if (spawnedChunks.ContainsKey(coord))
            return;

        if (groundChunks.Count == 0)
        {
            Debug.LogError("No Ground Chunks");
            return;
        }

        int randomIndex = Random.Range(0, groundChunks.Count);

        GameObject prefab = groundChunks[randomIndex];

        Vector3 spawnPos = new Vector3(coord.x * chunkSize + chunkSize * 0.5f,0f,coord.y * chunkSize + chunkSize * 0.5f);

        GameObject chunk = MyPoolManager.Instance.Get(prefab,spawnPos,Quaternion.identity);

        spawnedChunks.Add(coord, chunk);

        //navMesh Runtime
        //needRebuildNavMesh = true;
    }

    // =========================================================
    // REMOVE FAR CHUNKS
    // =========================================================

    private void RemoveFarChunks(Vector2Int currentChunk)
    {
        List<Vector2Int> removeList = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            float distance = Vector2Int.Distance(currentChunk,chunk.Key);

            if (distance > removeDistance)
            {
                PoolObject po = chunk.Value.GetComponent<PoolObject>();

                if (po != null)
                {
                    po.ReturnToPool();
                }

                removeList.Add(chunk.Key);
            }
        }

        foreach (var coord in removeList)
        {
            spawnedChunks.Remove(coord);
        }
    }

    // =========================================================
    // DEBUG
    // =========================================================

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (var chunk in spawnedChunks)
        {
            Vector3 pos = new Vector3(chunk.Key.x * chunkSize + chunkSize * 0.5f,0f,chunk.Key.y * chunkSize + chunkSize * 0.5f);

            Gizmos.DrawWireCube(pos,new Vector3(chunkSize, 1f, chunkSize));
        }
    }
}