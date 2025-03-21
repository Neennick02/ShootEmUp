using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField] float movementSpeed = 50f;
    Rigidbody rb;

    [SerializeField] private float bulletFireRate = 1f;
    [SerializeField] private float bombFireRate = 1f;
    private float bulletTimer = 0f;
    private float bombTimer = 0f;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bombSpawnPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Shoot();
        DropBomb();


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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 25), Mathf.Clamp(transform.position.y, -5, 25), transform.position.z);
        
    }

    void Shoot()
    {
        bulletTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(bulletTimer > bulletFireRate)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletTimer = 0;
            }
        }
    }

    void DropBomb()
    {
        bombTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (bombTimer > bombFireRate)
            {
                Instantiate(bombPrefab, bombSpawnPoint.position, bulletSpawnPoint.rotation);
                bombTimer = 0f;
            }
        }
    }
}
