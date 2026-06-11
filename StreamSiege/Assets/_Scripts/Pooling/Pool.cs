using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public int ActiveCount => activeObjects.Count;
    public int InactiveCount => stack.Count;

    public int PeakCount { get; private set; }

    private readonly Stack<GameObject> stack = new();
    private readonly HashSet<GameObject> pooledObjects = new();
    private readonly HashSet<GameObject> activeObjects = new();

    private readonly GameObject prefab;
    private readonly int maxSize;

    public Pool(GameObject prefab, int preload, int maxSize)
    {
        this.prefab = prefab;
        this.maxSize = maxSize;

        Preload(preload);
    }

    private void Preload(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Create();
            stack.Push(obj);
            pooledObjects.Add(obj);
        }
    }

    private GameObject Create()
    {
        GameObject obj = Object.Instantiate(prefab);

        PoolObject po = obj.GetComponent<PoolObject>();
        if (po == null)
            po = obj.AddComponent<PoolObject>();

        po.Init(this);

        obj.SetActive(false);
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj;

        while (stack.Count > 0)
        {
            obj = stack.Pop();

            if (obj == null)
                continue;

            pooledObjects.Remove(obj);
            Activate(obj);
            return obj;
        }

        obj = Create();
        Activate(obj);
        return obj;
    }

    public void Return(GameObject obj)
    {
        if (obj == null) return;
        if (pooledObjects.Contains(obj)) return;

        activeObjects.Remove(obj);

        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnDespawn();

        if (stack.Count >= maxSize)
        {
            Object.Destroy(obj);
            return;
        }

        pooledObjects.Add(obj);
        stack.Push(obj);

        obj.SetActive(false);
    }

    private void Activate(GameObject obj)
    {
        activeObjects.Add(obj);

        if (activeObjects.Count > PeakCount)
            PeakCount = activeObjects.Count;

        obj.SetActive(true);

        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnSpawn();
    }
}