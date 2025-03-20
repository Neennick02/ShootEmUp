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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
