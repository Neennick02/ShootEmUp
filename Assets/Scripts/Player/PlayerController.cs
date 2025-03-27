using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField] float movementSpeed = 50f;
    Rigidbody rb;

    //variables voor verschillende fire rates
    [SerializeField] private float normalBulletFireRate = 1f;
    [SerializeField] private float normalBombFireRate = 1f;
    private float currentBulletRate;
    private float currentBombRate;
    private float newBulletRate;
    private float newBombRate;


    private float bulletTimer = 0f;
    private float bombTimer = 0f;
    private float fireRateUpTimer = 0f;
    private bool startFirerateUpTimer = false;

    private float verticalSpeed;
    private float horizontalSpeed;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bombSpawnPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Vector3 targetAngle;
    private Vector3 startAngle;
    private Vector3 endAngle = new Vector3(0, 0, 0);
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindFirstObjectByType<GameManager>();
        startAngle = transform.eulerAngles;

        newBulletRate = normalBulletFireRate / 2;
        newBombRate = normalBombFireRate / 2;
        currentBulletRate = normalBulletFireRate;
        currentBombRate = normalBombFireRate;
        
    }

    private void Update()
    {
        Shoot();
        DropBomb();
        RotatePlayer();
        CheckForPowerUp();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
         verticalSpeed = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
         horizontalSpeed = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
        rb.AddRelativeForce(horizontalSpeed, verticalSpeed,0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 25), Mathf.Clamp(transform.position.y, 16, 49), transform.position.z);
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

    void Shoot()
    {
        bulletTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(bulletTimer > currentBulletRate)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletTimer = 0;
            }
        }
    }

    public void FireRateUp()
    {
        startFirerateUpTimer = true;
        fireRateUpTimer = 0f;
    }

    void CheckForPowerUp()
    {
        if (startFirerateUpTimer)
        {
            currentBulletRate = newBulletRate;
            currentBombRate = newBombRate;
        }
        fireRateUpTimer += Time.deltaTime;
        if(fireRateUpTimer > 5)
        {
            currentBombRate = normalBombFireRate;
            currentBulletRate = normalBulletFireRate;
            fireRateUpTimer = 0;
            startFirerateUpTimer = false;
        }
    }

    void DropBomb()
    {
        bombTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (bombTimer > currentBombRate)
            {
                Instantiate(bombPrefab, bombSpawnPoint.position, bulletSpawnPoint.rotation);
                bombTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Scrap"))
        {
            Destroy(other.gameObject);
            gameManager.scrapCounter++;
        }
    }
}
