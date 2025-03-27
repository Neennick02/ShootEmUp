using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Config")]  
    [SerializeField] private float interval = 1f;
    [SerializeField] private float startDelay = 5f;
    [Header("DO NOT TOUCH")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private bool inRange = false;
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if(transform.position.x < 30f)
        {
            inRange = true;
        }
    }


    IEnumerator Shoot()
    {
        if (inRange)
        {
            yield return new WaitForSeconds(startDelay);
            while (true)
            {
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                yield return new WaitForSeconds(interval);
            }
        }
    }
        



}
