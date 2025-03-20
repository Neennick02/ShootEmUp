using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth, maxHealth;

    [SerializeField] private HealthBarUI healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    public void SetHealth(float healthChange)
    {
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);
    }
}
