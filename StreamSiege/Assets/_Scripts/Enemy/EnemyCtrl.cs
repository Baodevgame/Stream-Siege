using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [Header("Data")]
    [SerializeField]
    private EnemyData enemyData;

    private Transform targetPlayer;

    private Rigidbody rb;

    private Animator animator;

    private float attackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            targetPlayer = player.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (targetPlayer == null)
            return;

        Vector3 dir = targetPlayer.position - transform.position;

        dir.y = 0f;

        float distance = dir.magnitude;

        if (distance > enemyData.attackRange)
        {
            dir.Normalize();

            rb.MovePosition(rb.position +dir *enemyData.moveSpeed *Time.fixedDeltaTime);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            Attack();
        }

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            rb.MoveRotation(targetRotation);
        }
    }

    private void Attack()
    {
        attackTimer += Time.fixedDeltaTime;

        if (attackTimer < enemyData.attackCooldown)
            return;

        attackTimer = 0;

        animator.SetTrigger("Attack");

        switch (enemyData.attackType)
        {
            case AttackType.Melee:
                MeleeAttack();
                break;

            case AttackType.Ranged:
                RangedAttack();
                break;
        }
    }

    private void MeleeAttack()
    {
        Debug.Log($"{name} Melee Attack");

        PlayerHealth hp = targetPlayer.GetComponent<PlayerHealth>();

        if (hp != null)
        {
            hp.TakeDamage(enemyData.damage);
        }
    }

    private void RangedAttack()
    {
        if (enemyData.projectilePrefab == null)
        {
            Debug.LogWarning($"{name} missing Projectile Prefab");
            return;
        }

        GameObject bullet = Instantiate(enemyData.projectilePrefab,transform.position + Vector3.up,Quaternion.identity);

        Projectile projectile = bullet.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Init(targetPlayer,enemyData.damage,enemyData.projectileSpeed);
        }
    }
}