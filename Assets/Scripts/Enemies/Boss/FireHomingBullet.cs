using UnityEngine;

public class FireHomingBullet : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint0;
    [SerializeField] private Transform firePoint1;
    [SerializeField] GameObject gunSoundPrefab;
    [SerializeField] Health health;
    private Boss bossScript;
    float timer = 0f;

    private void Start()
    {
        bossScript = FindFirstObjectByType<Boss>();
    }

    private void Update()
    {
        if (bossScript.stopped) //checkt of de boss gestopt is met bewegen
        {
            timer += Time.deltaTime;
            if (timer > fireRate) //vuurt homing bullets af
            {
                Instantiate(gunSoundPrefab, firePoint1.position, Quaternion.identity);
                Instantiate(bullet, firePoint0.position, Quaternion.identity);
                timer = 0f;
            }
        }
        NextPhase();
    }

    void NextPhase()
    {
        if(health.currentHealth < 600)
        {
            fireRate = 1;
        }
        else if(health.currentHealth < 300)
        {
            fireRate = 0.5f;
        }
    }
}
