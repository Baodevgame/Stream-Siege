using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image hpFill;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHp;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHp;
    }

    private void UpdateHp(float currentHp, float maxHp)
    {
        hpFill.fillAmount = currentHp / maxHp;
    }
}