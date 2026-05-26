using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        CancelInvoke();

        Invoke(nameof(DisableBullet), 5f);
    }

    private void DisableBullet()
    {
        GetComponent<PoolObject>().Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DisableBullet();
        }
    }
}