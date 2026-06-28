using UnityEngine;

public class RangeAttack : IAttack
{
    private Enemy enemy;

    public RangeAttack(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Attack()
    {
        if (enemy.Data.projectilePrefab == null)
            return;

        GameObject bullet = Object.Instantiate(enemy.Data.projectilePrefab,enemy.transform.position + Vector3.up,Quaternion.identity);

        Projectile projectile =bullet.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Init(enemy.Target,enemy.Data.damage,enemy.Data.projectileSpeed);
        }
    }
}