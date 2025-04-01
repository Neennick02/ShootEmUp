using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damageAmount = 10;
    [Header("Change amount of range for random speed")]
    [SerializeField] private int randomRange = 5;
    [SerializeField] private GameObject explosion;
    private ScreenShake screenShake;
    void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.up * RandomizeBullet(), ForceMode.Impulse); ;
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            //splash effect
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            screenShake.start = true;
            Instantiate(explosion, transform.position, Quaternion.identity);    
            other.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);
        }
    }
}
