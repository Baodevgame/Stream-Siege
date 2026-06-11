using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]private Image hpFill;

    public void UpdateHp(float currentHp,float maxHp)
    {
        hpFill.fillAmount = currentHp / maxHp;
    }
}