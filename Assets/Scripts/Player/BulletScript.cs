using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    private PlayerController playerController;
    [SerializeField] private GameObject explosionPrefab, splashPrefab;
    [SerializeField]  private int scoreAmount = 10;
    [SerializeField] private float bulletSpeed = 50f;
    private ScreenShake screenShake;
    private GameManager gameManager;

    private void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        playerController = FindAnyObjectByType<PlayerController>();
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
        OutOfBounds();
    }

    private void OutOfBounds() //zorgt dat speler niet object buiten beeld kan raken
    {
        if(transform.position.x > 36)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) //checkt welk object geraakt wordt
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //damage Enemy 
            other.gameObject.GetComponent<Health>().takeDamage((int)playerController.bulletDamage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            screenShake.start = true;
            gameManager.score += scoreAmount;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("HomingBullet"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.score += 15;
        }

        else if (other.gameObject.CompareTag("Scrap"))
        {
            Destroy(other.gameObject);
            gameManager.scrapCounter++;
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Health>().takeDamage((int)playerController.bulletDamage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            screenShake.start = true;
            gameManager.score += 25;
            Destroy (gameObject);
        }
        if (other.gameObject.CompareTag("Water"))
        {
            Instantiate(splashPrefab, transform.position, Quaternion.identity);
        }
    }
}
