using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float interval = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private float timer = 0f;
    private void Start()
    {
        
    }

    private void Update()
    {
        ShootTurret();
    }

    void ShootTurret()
    {
        timer += Time.deltaTime;
        if(timer > interval)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timer = 0f;
        }
    }

}
