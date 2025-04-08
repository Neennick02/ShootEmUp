using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField] public float movementSpeed = 50f;
    Rigidbody rb;

    //variables voor verschillende fire rates
    [SerializeField] private float normalBulletFireRate = 0.7f;
    [SerializeField] private float normalBombFireRate = 1f;
    public float currentBulletRate;
    public float currentBombRate;
    private float newBulletRate;
    private float newBombRate;

    float bulletTimer = 0;
    float extraBulletTimer = 0f;
    private float bombTimer = 0f;
    private float fireRateUpTimer = 0f;
    private bool startFirerateUpTimer = false;

    private float verticalSpeed;
    private float horizontalSpeed;
    [SerializeField] GameObject gunSoundPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bombSpawnPoint;
    [SerializeField] private Transform extraCannon;
    [SerializeField] public bool extraCannonActivated = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private Vector3 targetAngle;
    private Vector3 startAngle;
    private Vector3 endAngle = new Vector3(0, 0, 0);
    private GameManager gameManager;

    public float bulletDamage = 15f;
    public float bombDamage = 30f;
    private bool damageUp = false;
    private float damageTimer = 0f;
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

    void MovePlayer() //verplaatst speler
    {
         verticalSpeed = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
         horizontalSpeed = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1) * Time.deltaTime * (movementSpeed * 3);
        rb.AddRelativeForce(horizontalSpeed, verticalSpeed,0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 25), Mathf.Clamp(transform.position.y, 16, 42), transform.position.z);
    }

    void RotatePlayer() //draait speler om te kunnen richten
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

    void Shoot() //schiet kogels
    {
        bulletTimer += Time.deltaTime;
        extraBulletTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(bulletTimer > currentBulletRate)
            {
                Instantiate(gunSoundPrefab, transform.position, Quaternion.identity);
                Instantiate(smokePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletTimer = 0;
            }
            if(extraCannonActivated && (extraBulletTimer > currentBulletRate))
            {
                Instantiate(gunSoundPrefab, transform.position, Quaternion.identity);
                Instantiate(smokePrefab, extraCannon.position, extraCannon.rotation);
                Instantiate(bulletPrefab, extraCannon.position, extraCannon.rotation);
                extraBulletTimer = 0;
            }
        }
    }

    public void FireRateUp() //vuursnelheid gaat omhoog
    {
        startFirerateUpTimer = true;
        fireRateUpTimer = 0f;
    }

    public void DamageUp()//damage gaat omhoog
    {
        damageUp = true;
        bulletDamage = bulletDamage * 2;
        bombDamage = bombDamage * 2;
    }

    void CheckForPowerUp()//controleerd of powerup opgepakt wordt en start timer
    {
        if (startFirerateUpTimer)
        {
            currentBulletRate = newBulletRate;
            currentBombRate = newBombRate;
        }
        fireRateUpTimer += Time.deltaTime;
        if(fireRateUpTimer > 10)
        {
            currentBombRate = normalBombFireRate;
            currentBulletRate = normalBulletFireRate;
            fireRateUpTimer = 0;
            startFirerateUpTimer = false;
        }

        if (damageUp)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer > 10)
            {
                bulletDamage = 15f;
                bombDamage = 30f;
                damageTimer = 0f;
                damageUp = false;
            }
        }
    }

    void DropBomb()//laat bommen vallen
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

    private void OnTriggerEnter(Collider other)//pakt scrap op
    {
        if (other.gameObject.CompareTag("Scrap"))
        {
            Destroy(other.gameObject);
            gameManager.scrapCounter++;
        }
    }
}
