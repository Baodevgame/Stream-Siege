using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement movement;
    private PlayerTargetFinder targetFinder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
        targetFinder = GetComponent<PlayerTargetFinder>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        Transform enemy = targetFinder.Target;

        if (enemy != null)
        {
            Vector3 lookDir = enemy.position - transform.position;
            lookDir.y = 0;

            if (lookDir.sqrMagnitude > 0.01f)
            {
                rb.MoveRotation(Quaternion.LookRotation(lookDir));
            }
        }
        else
        {
            Vector3 move = movement.MoveDirection;

            if (move.sqrMagnitude > 0.01f)
            {
                rb.MoveRotation(Quaternion.LookRotation(move));
            }
        }
    }
}