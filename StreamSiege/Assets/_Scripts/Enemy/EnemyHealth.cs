using UnityEngine;
using UnityEngine.Pool;

public class EnemyHealth : MonoBehaviour
{
    private PoolObject poolObject;
    [SerializeField]private EnemyData enemyData;

    private float currentHp;

    private void Awake()
    {
        poolObject = GetComponent<PoolObject>();
    }
    private void OnEnable()
    {
        currentHp = enemyData.hp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        Debug.Log($"{name} HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        poolObject.ReturnToPool();
    }
}