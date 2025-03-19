using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float bulletSpeed = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("dood");
        }
    }
}
