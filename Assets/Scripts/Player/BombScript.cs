using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private int damageAmount = 30;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            //water splash
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //explosion prefab
            collision.gameObject.GetComponent<Health>().takeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
