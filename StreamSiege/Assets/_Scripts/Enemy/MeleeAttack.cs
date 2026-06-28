using UnityEngine;

public class MeleeAttack : IAttack
{
    private Enemy enemy;

    public MeleeAttack(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Attack()
    {
        Debug.Log(enemy.name + " Melee Attack");

        PlayerHealth hp = enemy.Target.GetComponent<PlayerHealth>();

        if (hp != null)
        {
            hp.TakeDamage(enemy.Data.damage);
        }
    }
}
