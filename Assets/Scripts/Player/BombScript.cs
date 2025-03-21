using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private int damageAmount = 30;
    [SerializeField] private ScreenShake screenShake;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            //water splash
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //explosion prefab
            screenShake.start = true;
            other.gameObject.GetComponent<Health>().takeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
