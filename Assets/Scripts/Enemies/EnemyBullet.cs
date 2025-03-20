using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    Rigidbody rb;

    public int damageAmount = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * bulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //damage player 
            collision.gameObject.GetComponent<PlayerHealth>().SetHealth(-damageAmount);
            Destroy(gameObject);

        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            //splash effect
            Destroy(gameObject);
        }
    }
}
