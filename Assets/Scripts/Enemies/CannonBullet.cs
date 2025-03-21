using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damageAmount = 10;

    void Start()
    {
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
            other.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);
        }
    }
}
