using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float rotationSpeed = 50f;
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
        RotatePlayer();
        Shoot();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float vecticalSpeed = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1) * Time.deltaTime * movementSpeed;
        float horizontalSpeed = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1) * Time.deltaTime * movementSpeed;
        rb.AddRelativeForce(horizontalSpeed, 0, vecticalSpeed, ForceMode.Acceleration);
    }

    void RotatePlayer()
    {
        //zorgt dat je de player kan draaien
        float rotationInput = Input.GetAxisRaw("Horizontal");
        float rotationAmount = rotationInput * Time.deltaTime * rotationSpeed;
        rb.rotation *= Quaternion.AngleAxis(rotationAmount, Vector3.up);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("FIRE!");
            fireTimer += Time.deltaTime;
            if(fireTimer > fireRate)
            {
                Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                fireTimer = 0;
            }
        }
    }
}
