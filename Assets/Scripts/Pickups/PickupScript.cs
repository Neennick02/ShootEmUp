using UnityEngine;

public abstract class PickupScript : MonoBehaviour
{
    protected GameManager gameManager;
    protected PlayerController playerController;
    protected PlayerHealth playerHealth;
    protected BulletScript bulletScript;
    protected BombScript bombScript;
    protected PowerUpText powerUpText;

    private float timeAlive = 10f;
    protected bool pickedUp = false;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerController = FindFirstObjectByType<PlayerController>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        bulletScript = FindFirstObjectByType<BulletScript>();
        bombScript = FindFirstObjectByType<BombScript>();
        powerUpText = FindFirstObjectByType<PowerUpText>();
    }

    public virtual void Activate()
    {
        //code voor pickups
    }

    public void DestroyObject()
    {
        //vernietigt item als tijd op is
        Destroy(gameObject, timeAlive);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerProjectile"))
        {
            Activate();
            //pickup sound
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        
    }
}
