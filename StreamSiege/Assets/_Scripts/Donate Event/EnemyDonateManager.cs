using UnityEngine;

public class EnemyDonateManager : MonoBehaviour
{
    public EnemySpawner spawner;

    [Header("Thresholds")]
    public int eliteThreshold = 100;
    public int leaderThreshold = 1000;
    public int bossThreshold = 10000;

    private int eliteProgress;
    private int leaderProgress;
    private int bossProgress;

    public void AddDonate(int value)
    {
        switch (value)
        {
            case 1:
            case 20:
                eliteProgress += value;
                break;

            case 100:
                leaderProgress += value;
                break;

            case 999:
                bossProgress += value;
                break;
        }

        CheckSpawn();
    }

    private void CheckSpawn()
    {
        // ELITE
        while (eliteProgress >= eliteThreshold)
        {
            eliteProgress -= eliteThreshold;
            spawner.SpawnElite();
        }

        // LEADER
        while (leaderProgress >= leaderThreshold)
        {
            leaderProgress -= leaderThreshold;
            spawner.SpawnLeader();
        }

        // BOSS
        while (bossProgress >= bossThreshold)
        {
            bossProgress -= bossThreshold;
            spawner.SpawnBoss();
        }
    }
    public int GetEliteProgress()
    {
        return eliteProgress;
    }

    public int GetLeaderProgress()
    {
        return leaderProgress;
    }

    public int GetBossProgress()
    {
        return bossProgress;
    }
}