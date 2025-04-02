using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private GameObject explosion;
    [Header("Change amount of range for random speed")]
    [SerializeField] float randomRange = 5;
    private ScreenShake screenShake;
    Rigidbody rb;

    public int damageAmount = 10;
    void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * RandomizeBullet();
    }

    void Update()
    {
        Destroy(gameObject, 4f);
    }

    float RandomizeBullet()
    {
        float randomSpeed = Random.Range(bulletSpeed - bulletSpeed / randomRange, bulletSpeed + bulletSpeed / randomRange);
        return randomSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            //splash effect
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //explosie
            Instantiate(explosion, transform.position, Quaternion.identity);
            screenShake.start = true;
            other.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);
        }
    }
}
