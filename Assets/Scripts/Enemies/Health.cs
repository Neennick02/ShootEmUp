using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] private GameObject scrapPrefab;
    private GameManager gameManager;
    public float currentHealth;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.enemies.Add(gameObject);
        currentHealth = maxHealth;
    }

    void Update()
    {
        Die();
        CheckPostion();
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
    }

    void Die()
    {
        if(currentHealth <= 0)
        {
            Instantiate(scrapPrefab, transform.position, Quaternion.identity);
            gameManager.enemies.Remove(gameObject);
            if (this.gameObject.CompareTag("Boss"))
            {
                gameManager.bossBeaten = true;
            }
            Destroy(gameObject);
        }
    }

    void CheckPostion()
    {
        if (transform.position.x < -40)
        {
            gameManager.score -= 20;
            gameManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
