using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    public int damageAmount = 10;
    [SerializeField]  private int scoreAmount = 10;
    [SerializeField] private float bulletSpeed = 50f;
    private ScreenShake screenShake;
    private GameManager gameManager;

    bool timerStarted = false;
    float powerupTimer = 0;
    private void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        CheckForPowerup();
        Destroy(gameObject, 2f);
        OutOfBounds();
    }

    private void OutOfBounds()
    {
        if(transform.position.x > 36)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //damage Enemy 
            other.gameObject.GetComponent<Health>().takeDamage(damageAmount);
            
            screenShake.start = true;
            gameManager.score += scoreAmount;
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Scrap"))
        {
            Destroy(other.gameObject);
            gameManager.scrapCounter++;
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Health>().takeDamage(damageAmount);
            screenShake.start = true;
            gameManager.score += 25;
            Destroy (gameObject);
        }
    }

    public void DamageUp()
    {
        timerStarted = true;
        damageAmount = damageAmount * 2;
    }

    void CheckForPowerup()
    {
        if (timerStarted)
        {

            powerupTimer += Time.deltaTime;
            if (powerupTimer > 5)
            {
                damageAmount = 10;
                powerupTimer = 0;
                timerStarted = false;
            }
        }
    }
}
