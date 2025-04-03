using UnityEngine;

public class BossHoming : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] private GameObject explosionPrefab;
    Transform target;
    public float speed = 5f;
    public float rotateSpeed = .03f;
    

    private Rigidbody rb;

    //private float carHealth = 3;

    GameManager gameManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        gameManager = FindFirstObjectByType<GameManager>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        if(target == null)
        {
            Debug.Log("no target found");
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = target.position - rb.position;
        direction.Normalize();
        Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

        rb.angularVelocity = -amountToRotate * rotateSpeed;

        rb.linearVelocity = transform.forward * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        // check if rocket hit player
        if (other.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            playerHealth.SetHealth(-damageAmount);
        }
        // check if car got hit by bullet
        if (other.CompareTag("PlayerProjectile"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
