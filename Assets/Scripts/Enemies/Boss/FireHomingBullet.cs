using UnityEngine;

public class FireHomingBullet : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    private BossScript bossScript;
    float timer = 0f;

    private void Start()
    {
        bossScript = FindFirstObjectByType<BossScript>();
    }

    private void Update()
    {
        if (bossScript.stopped) //checkt of de boss gestopt is met bewegen
        {
            timer += Time.deltaTime;
            if (timer > fireRate) //vuurt homing bullets af
            {
                Instantiate(bullet, firePoint.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }
}
