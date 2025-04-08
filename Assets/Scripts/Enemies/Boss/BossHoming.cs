using UnityEngine;

public class BossHoming : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] private GameObject explosionPrefab;
    Transform target;
    public float speed = 5f;
    public float rotateSpeed = .03f;
    int bullethealth = 2;
    

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
        RotateTowardsPlayer(360f);
    }

    private void FixedUpdate()
    {
        RotateTowardsPlayer(rotateSpeed);

        Vector3 direction = target.position - rb.position;
        direction.z = 0f;
        direction.Normalize();

        rb.linearVelocity = direction * speed;

    }

    private void RotateTowardsPlayer(float rotationSpeed)
    {
        Vector3 direction = target.position - rb.position;
        direction.z = 0f;
        direction.Normalize();


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed);

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
        // check if gameobject got hit by bullet
        if (other.CompareTag("PlayerProjectile"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            bullethealth--;
            if(bullethealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
