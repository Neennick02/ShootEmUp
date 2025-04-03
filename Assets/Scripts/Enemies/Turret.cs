using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject gunSoundPrefab;
    [SerializeField] private float interval = 1f;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private GameObject smokePrefab;
    [Header("DO NOT TOUCH")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private bool inRange = false;
    private bool isShooting = false;
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if(transform.position.x < 30f && !isShooting)
        {
            inRange = true;
            StartCoroutine(Shoot());
            isShooting = true;
        }
    }


    IEnumerator Shoot()
    {
        if (inRange)
        {
            yield return new WaitForSeconds(startDelay);
            while (true)
            {
                Instantiate(gunSoundPrefab, transform.position, Quaternion.identity);
                Instantiate(smokePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
