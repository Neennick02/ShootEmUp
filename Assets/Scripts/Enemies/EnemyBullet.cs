using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    private ScreenShake screenShake;
    Rigidbody rb;

    public int damageAmount = 10;
    void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * bulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, 3f);
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
            screenShake.start = true;
            other.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);
        }
    }
}
