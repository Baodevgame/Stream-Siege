using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    public EnemyData Data => enemyData;

    public Transform Target { get; private set; }

    public Rigidbody RB { get; private set; }

    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }

    private IAttack attack;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();

        Animator = GetComponent<Animator>();

        StateMachine = new StateMachine();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Target = player.transform;
        }

        switch (enemyData.attackType)
        {
            case AttackType.Melee:
                attack = new MeleeAttack(this);
                break;

            case AttackType.Ranged:
                attack = new RangeAttack(this);
                break;
        }
    }

    private void Start()
    {
        StateMachine.ChangeState(new EnemiesChaseState(this));
    }

    private void FixedUpdate()
    {
        StateMachine.Update();
    }

    public void Attack()
    {
        attack.Attack();
    }
}