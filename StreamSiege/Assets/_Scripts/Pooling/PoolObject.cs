using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Pool pool;

    public void Init(Pool pool)
    {
        this.pool = pool;
    }

    public void ReturnToPool()
    {
        pool?.Return(gameObject);
    }
}