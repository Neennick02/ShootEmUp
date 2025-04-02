using UnityEngine;

public class BossHoming : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
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
            Debug.Log("SDA");
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = target.position - rb.position;
        direction.Normalize();
        // rotate rocket to player
        Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

        // let car fly forward
        rb.angularVelocity = -amountToRotate * rotateSpeed;

        rb.linearVelocity = transform.forward * speed;
       /* //Delete car if no health
        if (carHealth <= 0)
        {
            gameManager.score += 10;
            Destroy(gameObject);
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        // check if rocket hit player
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerHealth.SetHealth(-damageAmount);
        }
        // check if car got hit by bullet
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
        }

    }
}
