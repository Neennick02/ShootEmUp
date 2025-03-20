using System.Collections;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Config")]  
    [SerializeField] private float interval = 1f;
    [SerializeField] private float startDelay = 5f;
    [Header("DO NOT TOUCH")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private float timer = 0f;
    private void Start()
    {
        StartCoroutine(Shoot());
    }



    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            yield return new WaitForSeconds(interval);
        }
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
