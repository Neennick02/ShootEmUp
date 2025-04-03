using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth;

    [SerializeField] private HealthBar healthBar;
    public bool upgradeBought = false;
    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (upgradeBought)
        {
            health = maxHealth;
        }
    }

    public void SetHealth(int healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.SetHealth((int)health);
    }
}
