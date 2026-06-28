using UnityEngine;

public class EnemiesAttackState : IState
{
    private Enemy enemy;

    private float timer;

    public EnemiesAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        timer = enemy.Data.attackCooldown;
    }

    public void Update()
    {
        if (enemy.Target == null)
            return;

        Vector3 dir = enemy.Target.position -enemy.transform.position;

        dir.y = 0;

        float distance = dir.magnitude;

        if (distance > enemy.Data.attackRange)
        {
            enemy.StateMachine.ChangeState(new EnemiesChaseState(enemy));

            return;
        }

        timer += Time.deltaTime;

        if (timer >= enemy.Data.attackCooldown)
        {
            timer = 0;

            enemy.Animator.SetTrigger("Attack");

            enemy.Attack();
        }
    }

    public void Exit()
    {

    }
}
