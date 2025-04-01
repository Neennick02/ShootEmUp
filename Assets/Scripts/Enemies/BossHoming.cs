using UnityEngine;

public class BossHoming : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = .03f;

    private Rigidbody rb;

    private Health health;

    GameManager game;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        game = FindFirstObjectByType<GameManager>();
    }

    private void LateUpdate()
    {
        Vector3 direction = target.position - rb.position;
        direction.Normalize();
        // rotate rocket to player
        Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
