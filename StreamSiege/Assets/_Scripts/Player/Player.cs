using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RB { get; private set; }

    public Animator Animator { get; private set; }

    public PlayerSkillController Skill { get; private set; }

    public PlayerTargetFinder TargetFinder { get; private set; }

    public PlayerMovement Movement { get; private set; }

    public PlayerShooter Shooter { get; private set; }

    public PlayerHealth Health { get; private set; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();

        Animator = GetComponent<Animator>();

        Skill = GetComponent<PlayerSkillController>();

        TargetFinder = GetComponent<PlayerTargetFinder>();

        Movement = GetComponent<PlayerMovement>();

        Shooter = GetComponent<PlayerShooter>();

        Health = GetComponent<PlayerHealth>();
    }
}