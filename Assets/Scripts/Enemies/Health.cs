using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Die();
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
    }

    void Die()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
