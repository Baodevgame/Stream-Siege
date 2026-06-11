using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public float hp = 100;
    public float moveSpeed = 3f;

    [Header("Attack")]
    public AttackType attackType;
    public float damage = 10;
    public float attackRange = 2;
    public float attackCooldown = 1;

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
}