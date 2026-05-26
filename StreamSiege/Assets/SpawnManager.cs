using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyNormal;
    public GameObject enemyElite;
    public GameObject enemyBoss;

    public void SpawnByAmount(int amount)
    {
        Vector3 spawnPos = GetRandomPos();

        if (amount < 10)
        {
            Instantiate(enemyNormal, spawnPos, Quaternion.identity);
        }
        else if (amount < 50)
        {
            Instantiate(enemyElite, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyBoss, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPos()
    {
        Vector2 circle = Random.insideUnitCircle * 8f;
        return new Vector3(circle.x, 0f, circle.y);
    }
}