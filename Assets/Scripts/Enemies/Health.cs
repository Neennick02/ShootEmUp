using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] private GameObject scrapPrefab;
    private GameManager gameManager;
    public float currentHealth;
    private BossHealthBar BossHealthBar;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        BossHealthBar = FindFirstObjectByType<BossHealthBar>();

        gameManager.enemies.Add(gameObject);
        currentHealth = maxHealth;
    }

    void Update()
    {
        Die();
        CheckPostion();
        BossHealth();
    }

    public void takeDamage(int amount) //neemt damage
    {
        currentHealth -= amount;
    }

    void Die() //verwijderd object uit de List<>
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

    void CheckPostion() //verwijderd object als hij het scherm verlaat
    {
        if (transform.position.x < -40)
        {
            gameManager.score -= 20;
            gameManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void BossHealth() //vuld de healthbar van de boss
    {
        if (this.gameObject.CompareTag("Boss"))
        {
            BossHealthBar.SetHealth(currentHealth, maxHealth);
        }
    }
}
