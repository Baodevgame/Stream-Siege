using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [Header("Speed Boost")]
    [SerializeField] private float speedDuration = 5f;
    [SerializeField] private float speedMultiplier = 1.5f;

    private float speedTimer;
    private bool speedActive;

    [Header("Enemy Shield")]
    [SerializeField] private float shieldDuration = 10f;

    private float shieldTimer;
    private bool shieldActive;

    [Header("Blade Storm")]
    [SerializeField] private float swordDuration = 10f;

    private float swordTimer;
    private bool swordActive;

    [Header("Charges")]
    [SerializeField] private int speedCharges;
    [SerializeField] private int shieldCharges;
    [SerializeField] private int swordCharges;
    [SerializeField] private int boomCharges;
    //===============VFX===============
    [Header("Enemy Shield VFX")]
    [SerializeField] private GameObject shieldVFX;

    [Header("Blade Storm VFX")]
    [SerializeField] private GameObject swordVFX;

    public float SpeedMultiplier => speedMultiplier;

    private void Start()
    {
        if (shieldVFX != null)
            shieldVFX.SetActive(false);
        if (swordVFX != null)
            swordVFX.SetActive(false);
    }

    private void Update()
    {
        UpdateBuffs();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseSpeed();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseShield();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseSword();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            UseBoom();
    }

    #region Add Charges

    public void AddSpeedCharge()
    {
        speedCharges++;
        Debug.Log($"Speed Charge +1 ({speedCharges})");
    }

    public void AddShieldCharge()
    {
        shieldCharges++;
        Debug.Log($"Shield Charge +1 ({shieldCharges})");
    }

    public void AddSwordCharge()
    {
        swordCharges++;
        Debug.Log($"Sword Charge +1 ({swordCharges})");
    }

    public void AddBoomCharge()
    {
        boomCharges++;
        Debug.Log($"Boom Charge +1 ({boomCharges})");
    }

    #endregion

    #region Use Skills

    private void UseSpeed()
    {
        if (speedCharges <= 0)
        {
            Debug.Log("No Speed Charges");
            return;
        }

        speedCharges--;

        speedActive = true;
        speedTimer = speedDuration;

        Debug.Log($"Speed Used. Remaining: {speedCharges}");
    }

    private void UseShield()
    {
        if (shieldCharges <= 0)
        {
            Debug.Log("No Shield Charges");
            return;
        }

        shieldCharges--;

        shieldActive = true;
        shieldTimer = shieldDuration;

        if (shieldVFX != null)
            shieldVFX.SetActive(true);

        Debug.Log($"Shield Used. Remaining: {shieldCharges}");
    }

    private void UseSword()
    {
        if (swordCharges <= 0)
        {
            Debug.Log("No Sword Charges");
            return;
        }

        swordCharges--;

        swordActive = true;
        swordTimer = swordDuration;

        if (swordVFX != null)
            swordVFX.SetActive(true);

        Debug.Log($"Sword Used. Remaining: {swordCharges}");
    }

    private void UseBoom()
    {
        if (boomCharges <= 0)
        {
            Debug.Log("No Boom Charges");
            return;
        }

        boomCharges--;

        Debug.Log($"Boom Used. Remaining: {boomCharges}");

        // TODO:
        // Spawn Boom
    }

    #endregion

    #region Buff Update

    private void UpdateBuffs()
    {
        if (speedActive)
        {
            speedTimer -= Time.deltaTime;

            if (speedTimer <= 0f)
            {
                speedActive = false;
            }
        }

        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                shieldActive = false;

                if (shieldVFX != null)
                    shieldVFX.SetActive(false);
            }
        }

        if(swordActive)
        {
            swordTimer -= Time.deltaTime;
            if (swordTimer <= 0f)
            {
                swordActive = false;
                if (swordVFX != null)
                    swordVFX.SetActive(false);
            }
        }
    }

    #endregion

    #region Getters

    public bool IsSpeedActive()
    {
        return speedActive;
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }

    public int GetSpeedCharges()
    {
        return speedCharges;
    }

    public int GetShieldCharges()
    {
        return shieldCharges;
    }

    public int GetSwordCharges()
    {
        return swordCharges;
    }

    public int GetBoomCharges()
    {
        return boomCharges;
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    #endregion
}