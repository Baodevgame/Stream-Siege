using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PoolObject poolObject;
    private Rigidbody rb;

    private void Awake()
    {
        poolObject = GetComponent<PoolObject>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (poolObject == null)
        {
            poolObject =GetComponent<PoolObject>();
        }

        rb.linearVelocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        rb.Sleep();
        rb.WakeUp();

        CancelInvoke();

        Invoke(nameof(ReturnBullet), 5f);
    }

    private void ReturnBullet()
    {
        poolObject.ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(20);
        }

        ReturnBullet();
    }
}