using UnityEngine;

public class PlayerDonateManager : MonoBehaviour
{
    public PlayerSkillController skillController;

    [Header("Thresholds")]
    public int speedThreshold = 10;
    public int shieldThreshold = 100;
    public int swordThreshold = 1000;
    public int boomThreshold = 10000;

    private int speedProgress;
    private int shieldProgress;
    private int swordProgress;
    private int boomProgress;

    public void AddDonate(int value)
    {
        switch (value)
        {
            case 1:
                speedProgress += value;
                break;

            case 20:
                shieldProgress += value;
                break;

            case 100:
                swordProgress += value;
                break;

            case 999:
                boomProgress += value;
                break;
        }

        CheckSkillUnlock();
    }

    private void CheckSkillUnlock()
    {
        while (speedProgress >= speedThreshold)
        {
            speedProgress -= speedThreshold;
            skillController.AddSpeedCharge();
        }

        while (shieldProgress >= shieldThreshold)
        {
            shieldProgress -= shieldThreshold;
            skillController.AddShieldCharge();
        }

        while (swordProgress >= swordThreshold)
        {
            swordProgress -= swordThreshold;
            skillController.AddSwordCharge();
        }

        while (boomProgress >= boomThreshold)
        {
            boomProgress -= boomThreshold;
            skillController.AddBoomCharge();
        }
    }

    public int GetSpeedProgress() => speedProgress;
    public int GetShieldProgress() => shieldProgress;
    public int GetSwordProgress() => swordProgress;
    public int GetBoomProgress() => boomProgress;
}