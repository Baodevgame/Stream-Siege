using UnityEngine;

public class LiveEventManager : MonoBehaviour
{
    public SpawnManager spawnManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnEnemyDonate(5);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            OnEnemyDonate(20);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            OnEnemyDonate(100);
    }
    public void OnEnemyDonate(int amount)
    {
        Debug.Log("Enemy donate: " + amount);

        if (spawnManager != null)
        {
            spawnManager.SpawnByAmount(amount);
        }
    }
}