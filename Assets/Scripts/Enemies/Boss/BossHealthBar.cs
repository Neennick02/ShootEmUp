using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;

    //vult de healthbar van de boss
    public void SetHealth(float currentHealth, float maxHealth)
    {
        float fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        healthFillImage.fillAmount = fillAmount;
    }
}
