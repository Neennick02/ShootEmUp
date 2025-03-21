using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health, maxHealth;

    [SerializeField] private HealthBarUI healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void SetHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.SetHealth(health);
    }
}
