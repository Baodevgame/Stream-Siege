using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerSkillController skill;

    [SerializeField]private float maxHp = 100;

    [SerializeField]private PlayerHealthUI hpUI;

    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;

        hpUI.UpdateHp(currentHp,maxHp);
    }

    public void TakeDamage(float damage)
    {
        if (skill != null && skill.IsShieldActive())
        {
            Debug.Log("Blocked by Shield!");
            return;
        }

        currentHp -= damage;

        hpUI.UpdateHp(currentHp,maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }
}