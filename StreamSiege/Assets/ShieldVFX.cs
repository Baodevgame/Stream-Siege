using UnityEngine;

public class ShieldVFX : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float heightOffset = 1f;
    [SerializeField] private float rotateSpeed = 200f;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + Vector3.up * heightOffset;
        }

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}