using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField] float movementSpeed = 50f;
    Rigidbody rb;

    [SerializeField] private float fireRate = 1f;
    private float fireTimer = 0f;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private GameObject Bullet;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Shoot();
        
    }

    void FixedUpdate()
    {
        ClampMovement();
        MovePlayer();
    }

    void MovePlayer()
    {
        float vecticalSpeed = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
        float horizontalSpeed = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
        rb.AddRelativeForce(horizontalSpeed, vecticalSpeed,0);
    }

    void ClampMovement()
    {
        currentPosition = transform.position;
        if(currentPosition.y > 25)
        {
            currentPosition.y = 25;
        }
        else if(currentPosition.y < -5)
        {
            currentPosition.y = -5;
        }
        
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(fireTimer > fireRate)
            {
                Debug.Log("FIRE!");
                Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                fireTimer = 0;
            }
        }
    }
}
