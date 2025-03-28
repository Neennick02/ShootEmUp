using UnityEngine;

public class BombScript : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private int scoreAmount = 25;
    private ScreenShake screenShake;
    private GameManager gameManager;
    [SerializeField] private bool useGravity = true;
    private Rigidbody rb;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerController = FindFirstObjectByType<PlayerController>();
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
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
            other.gameObject.GetComponent<Health>().takeDamage((int)playerController.bombDamage);
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
            other.gameObject.GetComponent<Health>().takeDamage((int)playerController.bombDamage);
            screenShake.start = true;
            gameManager.score += 25;
            Destroy(gameObject);
        }
    }
}
