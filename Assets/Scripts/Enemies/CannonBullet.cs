using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damageAmount = 10;
    private ScreenShake screenShake;
    void Start()
    {
        screenShake = FindFirstObjectByType<ScreenShake>();
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.up * bulletSpeed, ForceMode.Impulse);
    }

    void Update()
    {
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
            other.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);
        }
    }
}
