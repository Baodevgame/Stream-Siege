using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Player player;

    public bool IsRunning { get; private set; }

    public Vector3 MoveDirection { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        MoveDirection = new Vector3(moveX, 0f, moveZ);

        IsRunning = MoveDirection.sqrMagnitude > 0.01f;

        float speed = moveSpeed;

        if (player.Skill != null && player.Skill.IsSpeedActive())
        {
            speed *= player.Skill.GetSpeedMultiplier();
        }

        player.RB.MovePosition(player.RB.position + MoveDirection * speed * Time.fixedDeltaTime);
    }
}