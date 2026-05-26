using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private Transform enemy;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("Shooting")]
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootRange = 10f;

    [SerializeField]
    private float fireRate = 0.25f;

    [SerializeField]
    private float bulletSpeed = 20f;

    private float fireTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        FindNearestEnemy();

        fireTimer -= Time.deltaTime;

        if (enemy != null &&
            fireTimer <= 0f)
        {
            PlayerShooting();

            fireTimer = fireRate;
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);

        rb.MovePosition(rb.position +move * moveSpeed * Time.fixedDeltaTime);
    }

    private void FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,shootRange);

        float closestDistance =Mathf.Infinity;

        enemy = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(transform.position,hit.transform.position);

                if (dist < closestDistance)
                {
                    closestDistance = dist;

                    enemy = hit.transform;
                }
            }
        }
    }

    private void PlayerShooting()
    {
        Vector3 dir =(enemy.position -transform.position).normalized;

        GameObject bullet = MyPoolManager.Instance.Get(bulletPrefab,transform.position + dir,Quaternion.LookRotation(dir));

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.linearVelocity = dir * bulletSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position,shootRange);
    }
}