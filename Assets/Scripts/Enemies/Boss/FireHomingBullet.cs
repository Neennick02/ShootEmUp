using UnityEngine;

public class FireHomingBullet : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] GameObject gunSoundPrefab;
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
                Instantiate(gunSoundPrefab, firePoint.position, Quaternion.identity);
                Instantiate(bullet, firePoint.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }
}
