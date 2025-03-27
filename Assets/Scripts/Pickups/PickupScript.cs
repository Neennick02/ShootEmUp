using UnityEngine;

public abstract class PickupScript : MonoBehaviour
{
    protected GameManager gameManager;
    protected PlayerController playerController;
    protected PlayerHealth playerHealth;

    private float timeAlive = 10f;
    protected bool pickedUp = false;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerController = FindFirstObjectByType<PlayerController>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
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
}
