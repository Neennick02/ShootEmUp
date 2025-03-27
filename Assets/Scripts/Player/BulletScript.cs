using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    public int playerDamage = 10;
    [SerializeField]  private int scoreAmount = 10;
    [SerializeField] private float bulletSpeed = 50f;
    private ScreenShake screenShake;
    private GameManager gameManager;

    private void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
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
            other.gameObject.GetComponent<Health>().takeDamage(playerDamage);
            
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
            other.gameObject.GetComponent<Health>().takeDamage(playerDamage);
            screenShake.start = true;
            gameManager.score += 25;
            Destroy (gameObject);
        }
    }
}
