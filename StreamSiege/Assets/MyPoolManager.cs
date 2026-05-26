using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private MyPool pool;

    public void SetPool(MyPool pool)
    {
        this.pool = pool;
    }

    public void Release()
    {
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

public class MyPool
{
    private Stack<GameObject> stack =
        new Stack<GameObject>();

    private GameObject prefab;

    private Transform root;

    public MyPool(GameObject prefab, Transform root)
    {
        this.prefab = prefab;
        this.root = root;
    }

    public GameObject Get()
    {
        GameObject obj;

        while (stack.Count > 0)
        {
            obj = stack.Pop();

            if (obj != null)
            {
                obj.SetActive(true);

                return obj;
            }
        }

        obj = GameObject.Instantiate(prefab, root);

        PoolObject poolObj =
            obj.GetComponent<PoolObject>();

        if (poolObj == null)
        {
            poolObj = obj.AddComponent<PoolObject>();
        }

        poolObj.SetPool(this);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        stack.Push(obj);
    }
}

public class MyPoolManager : MonoBehaviour
{
    public static MyPoolManager Instance;

    private Dictionary<GameObject, MyPool> pools =
        new Dictionary<GameObject, MyPool>();

    private Transform poolRoot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        poolRoot = new GameObject("PoolRoot").transform;
    }

    public GameObject Get(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools.Add(
                prefab,
                new MyPool(prefab, poolRoot));
        }

        return pools[prefab].Get();
    }

    public GameObject Get(
        GameObject prefab,
        Vector3 position)
    {
        GameObject obj = Get(prefab);

        obj.transform.position = position;

        return obj;
    }

    public GameObject Get(
        GameObject prefab,
        Vector3 position,
        Quaternion rotation)
    {
        GameObject obj = Get(prefab);

        obj.transform.SetPositionAndRotation(
            position,
            rotation);

        return obj;
    }

    public T Get<T>(T prefab)
        where T : Component
    {
        GameObject obj = Get(prefab.gameObject);

        return obj.GetComponent<T>();
    }

    public T Get<T>(
        T prefab,
        Vector3 position)
        where T : Component
    {
        GameObject obj =
            Get(prefab.gameObject, position);

        return obj.GetComponent<T>();
    }

    public T Get<T>(
        T prefab,
        Vector3 position,
        Quaternion rotation)
        where T : Component
    {
        GameObject obj =
            Get(prefab.gameObject,
            position,
            rotation);

        return obj.GetComponent<T>();
    }
}