using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    Rigidbody rb;

    public int enemyDamage = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * bulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //damage player 
            other.gameObject.GetComponent<Health>().takeDamage(enemyDamage);
            Destroy(gameObject);

        }
    }
}
