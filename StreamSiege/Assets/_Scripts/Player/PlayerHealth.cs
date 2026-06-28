using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerSkillController skill;
    [SerializeField] private float maxHp = 100;

    private float currentHp;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDie;

    private void Start()
    {
        currentHp = maxHp;

        OnHealthChanged?.Invoke(currentHp, maxHp);
    }

    public void TakeDamage(float damage)
    {
        if (skill != null && skill.IsShieldActive())
        {
            Debug.Log("Blocked by Shield!");
            return;
        }

        currentHp -= damage;

        OnHealthChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");

        OnDie?.Invoke();
    }
}