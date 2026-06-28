using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float bulletSpeed = 10f;

    private float fireTimer;

    private PlayerTargetFinder targetFinder;

    private void Awake()
    {
        targetFinder = GetComponent<PlayerTargetFinder>();
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (targetFinder.Target == null)
            return;

        if (fireTimer <= 0f)
        {
            Shoot();

            fireTimer = fireRate;
        }
    }

    private void Shoot()
    {
        Transform enemy = targetFinder.Target;

        Vector3 dir = (enemy.position - transform.position).normalized;

        GameObject bullet = MyPoolManager.Instance.Get(bulletPrefab,transform.position + dir,Quaternion.LookRotation(dir));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = dir * bulletSpeed;
    }
}