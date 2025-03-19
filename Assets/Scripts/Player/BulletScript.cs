using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    public int playerDamage = 10;
    private int scoreAmount = 10;
    [SerializeField] private float bulletSpeed = 50f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name);
            //damage Enemy 
            other.gameObject.GetComponent<Health>().takeDamage(playerDamage);
            gameManager.score += scoreAmount;
            Destroy(gameObject);
        }
    }
}
