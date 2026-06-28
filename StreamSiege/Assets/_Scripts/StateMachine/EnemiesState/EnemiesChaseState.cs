using UnityEngine;

public class EnemiesChaseState : IState
{
    private Enemy enemy;

    public EnemiesChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.Animator.SetBool("IsRunning", true);
    }

    public void Update()
    {
        if (enemy.Target == null)
            return;

        Vector3 dir = enemy.Target.position - enemy.transform.position;

        dir.y = 0;

        float distance = dir.magnitude;

        if (distance <= enemy.Data.attackRange)
        {
            enemy.StateMachine.ChangeState(new EnemiesAttackState(enemy));

            return;
        }

        dir.Normalize();

        enemy.RB.MovePosition(enemy.RB.position +dir *enemy.Data.moveSpeed *Time.fixedDeltaTime);

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion rot = Quaternion.LookRotation(dir);

            enemy.RB.MoveRotation(rot);
        }
    }

    public void Exit()
    {
        enemy.Animator.SetBool("IsRunning", false);
    }
}
