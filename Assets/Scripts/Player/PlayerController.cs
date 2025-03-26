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
    private float verticalSpeed;
    private float horizontalSpeed;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bombSpawnPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Vector3 targetAngle;
    private Vector3 startAngle;
    private Vector3 endAngle = new Vector3(0, 0, 0);
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startAngle = transform.eulerAngles;
    }

    private void Update()
    {
        Shoot();
        DropBomb();
        RotatePlayer();
    }

    void FixedUpdate()
    {
        ClampMovement();
        MovePlayer();
    }

    void MovePlayer()
    {
         verticalSpeed = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
         horizontalSpeed = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
        rb.AddRelativeForce(horizontalSpeed, verticalSpeed,0);
    }

    void RotatePlayer()
    {
        if(verticalSpeed > 0)
        {
            targetAngle = new Vector3(0, 0, 30);
        }
        if(verticalSpeed < 0)
        {
            targetAngle = new Vector3(0, 0, -30);
        }
        if(verticalSpeed < 0 || verticalSpeed > 0)
        {
            startAngle = new Vector3(
            Mathf.LerpAngle(startAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(startAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(startAngle.z, targetAngle.z, Time.deltaTime));
            transform.eulerAngles = startAngle;
        }

        if(verticalSpeed == 0)
        {
            startAngle = new Vector3(
            Mathf.LerpAngle(startAngle.x, endAngle.x, Time.deltaTime),
            Mathf.LerpAngle(startAngle.y, endAngle.y, Time.deltaTime),
            Mathf.LerpAngle(startAngle.z, endAngle.z, Time.deltaTime));  
            transform.eulerAngles = startAngle;
        }
        
    }

    void ClampMovement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 25), Mathf.Clamp(transform.position.y, 16, 49), transform.position.z);
        
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
