using UnityEngine;
using UnityEngine.UI;

public class DonationUI : MonoBehaviour
{
    [Header("Managers")]
    public EnemyDonateManager enemyProgress;
    public PlayerDonateManager playerProgress;
    public PlayerSkillController skill;

    #region Enemy

    [Header("Elite UI")]
    public Slider eliteSlider;
    public Text eliteText;

    [Header("Leader UI")]
    public Slider leaderSlider;
    public Text leaderText;

    [Header("Boss UI")]
    public Slider bossSlider;
    public Text bossText;

    #endregion

    #region Player Skills

    [Header("Speed UI")]
    public Slider speedSlider;
    public Text speedText;
    public Text speedChargeText;

    [Header("Shield UI")]
    public Slider shieldSlider;
    public Text shieldText;
    public Text shieldChargeText;

    [Header("Sword UI")]
    public Slider swordSlider;
    public Text swordText;
    public Text swordChargeText;

    [Header("Boom UI")]
    public Slider boomSlider;
    public Text boomText;
    public Text boomChargeText;

    #endregion

    private void Update()
    {
        UpdateElite();
        UpdateLeader();
        UpdateBoss();

        UpdateSpeed();
        UpdateShield();
        UpdateSword();
        UpdateBoom();
    }

    #region Enemy

    private void UpdateElite()
    {
        eliteSlider.maxValue = enemyProgress.eliteThreshold;
        eliteSlider.value = enemyProgress.GetEliteProgress();

        eliteText.text =enemyProgress.GetEliteProgress() +" / " +enemyProgress.eliteThreshold;
    }

    private void UpdateLeader()
    {
        leaderSlider.maxValue = enemyProgress.leaderThreshold;
        leaderSlider.value = enemyProgress.GetLeaderProgress();

        leaderText.text =enemyProgress.GetLeaderProgress() +" / " +enemyProgress.leaderThreshold;
    }

    private void UpdateBoss()
    {
        bossSlider.maxValue = enemyProgress.bossThreshold;
        bossSlider.value = enemyProgress.GetBossProgress();

        bossText.text =enemyProgress.GetBossProgress() +" / " +enemyProgress.bossThreshold;
    }

    #endregion

    #region Skills

    private void UpdateSpeed()
    {
        speedSlider.maxValue = playerProgress.speedThreshold;
        speedSlider.value = playerProgress.GetSpeedProgress();

        speedText.text =playerProgress.GetSpeedProgress() +" / " +playerProgress.speedThreshold;

        speedChargeText.text ="x" + skill.GetSpeedCharges();
    }

    private void UpdateShield()
    {
        shieldSlider.maxValue = playerProgress.shieldThreshold;
        shieldSlider.value = playerProgress.GetShieldProgress();

        shieldText.text =playerProgress.GetShieldProgress() +" / " +playerProgress.shieldThreshold;

        shieldChargeText.text ="x" + skill.GetShieldCharges();
    }

    private void UpdateSword()
    {
        swordSlider.maxValue = playerProgress.swordThreshold;
        swordSlider.value = playerProgress.GetSwordProgress();

        swordText.text =playerProgress.GetSwordProgress() +" / " +playerProgress.swordThreshold;

        swordChargeText.text ="x" + skill.GetSwordCharges();
    }

    private void UpdateBoom()
    {
        boomSlider.maxValue = playerProgress.boomThreshold;
        boomSlider.value = playerProgress.GetBoomProgress();

        boomText.text =playerProgress.GetBoomProgress() +" / " +playerProgress.boomThreshold;

        boomChargeText.text ="x" + skill.GetBoomCharges();
    }

    #endregion
}