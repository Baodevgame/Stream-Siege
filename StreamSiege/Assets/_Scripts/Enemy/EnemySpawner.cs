using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField]private List<EnemyGroupData> enemyGroups;

    [Header("Spawn")]
    [SerializeField]private Transform player;

    [SerializeField]private float spawnRadius = 20f;

    [SerializeField]private float normalSpawnRate = 2f;

    private Dictionary<EnemyType, List<GameObject>> enemyDict = new Dictionary<EnemyType, List<GameObject>>();

    //private bool hasFinishedSpawn;
    //public bool HasFinishedSpawn => hasFinishedSpawn;

    private void Awake()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
                player = p.transform;
            else
                Debug.LogError("Player not found! Make sure tag = Player");
        }
    }

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        foreach (var group in enemyGroups)
        {
            enemyDict[group.type] = group.prefabs;

            foreach (var prefab in group.prefabs)
            {
                MyPoolManager.Instance.CreatePool(prefab,20,200);
            }
        }

        StartCoroutine(SpawnNormalLoop());
    }

    // =====================================================
    // NORMAL SPAWN
    // =====================================================

    private IEnumerator SpawnNormalLoop()
    {
        while (true)
        {
            SpawnEnemy(EnemyType.Normal);

            yield return new WaitForSeconds(normalSpawnRate);
        }
    }

    // =====================================================
    // SPAWN
    // =====================================================

    public void SpawnEnemy(EnemyType type)
    {
        if (!enemyDict.ContainsKey(type))
            return;

        List<GameObject> list = enemyDict[type];

        if (list.Count == 0)
            return;

        int randomIndex = Random.Range(0, list.Count);

        GameObject prefab = list[randomIndex];

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 spawnPos = player.position + new Vector3(randomCircle.x,0f,randomCircle.y);

        MyPoolManager.Instance.Get(prefab,spawnPos,Quaternion.identity);
        //hasFinishedSpawn = true;
    }

    // =====================================================
    // DONATE EVENTS
    // =====================================================

    public void SpawnElite()
    {
        SpawnEnemy(EnemyType.Elite);
    }

    public void SpawnLeader()
    {
        SpawnEnemy(EnemyType.Leader);
    }

    public void SpawnBoss()
    {
        SpawnEnemy(EnemyType.Boss);
    }
}