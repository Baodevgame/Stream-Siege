using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviour
{
    public static MyPoolManager Instance;

    private readonly Dictionary<GameObject, Pool> pools = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool(GameObject prefab, int preload = 10, int maxSize = 200)
    {
        if (pools.ContainsKey(prefab))
            return;

        pools[prefab] = new Pool(prefab, preload, maxSize);
    }

    public GameObject Get(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
            CreatePool(prefab);

        return pools[prefab].Get();
    }

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Get(prefab);
        obj.transform.SetPositionAndRotation(position, rotation);
        return obj;
    }

    public T Get<T>(T prefab) where T : Component
    {
        return Get(prefab.gameObject).GetComponent<T>();
    }

    public T Get<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Get(prefab.gameObject, position, rotation).GetComponent<T>();
    }

    public void PrintDebug()
    {
        foreach (var pool in pools)
        {
            Debug.Log($"{pool.Key.name} | Active: {pool.Value.ActiveCount} | Inactive: {pool.Value.InactiveCount} | Peak: {pool.Value.PeakCount}");
        }
    }
}