using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private int damageAmount = 30;
    private ScreenShake screenShake;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        screenShake = FindFirstObjectByType<ScreenShake>();
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
}
