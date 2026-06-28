using UnityEngine;

public class PlayerTargetFinder : MonoBehaviour
{
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float scanInterval = 0.2f;

    private float scanTimer;

    public Transform Target { get; private set; }

    private void Update()
    {
        scanTimer -= Time.deltaTime;

        if (scanTimer <= 0f)
        {
            scanTimer = scanInterval;
            FindNearestEnemy();
        }
    }

    private void FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, shootRange);

        float closestDistance = Mathf.Infinity;
        Target = null;

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Enemy"))
                continue;

            float distance = Vector3.Distance(transform.position, hit.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                Target = hit.transform;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}