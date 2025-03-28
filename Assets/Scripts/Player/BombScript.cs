using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private int damageAmount = 30;
    [SerializeField] private int scoreAmount = 25;
    private ScreenShake screenShake;
    private GameManager gameManager;
    [SerializeField] private bool useGravity = true;
    private Rigidbody rb;
    private float powerupTimer = 0f;
    private bool timerStarted = false;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckForPowerup();
    }

    private void FixedUpdate()
    {
        rb.useGravity = false;
        if (useGravity)
        {
            rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            //water splash
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //explosion prefab
            screenShake.start = true;
            other.gameObject.GetComponent<Health>().takeDamage(damageAmount);
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
            Destroy(gameObject);
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
            if(powerupTimer > 5)
            {
                damageAmount = 30;
                powerupTimer = 0;
                timerStarted = false;
            }
        }
    }
}
