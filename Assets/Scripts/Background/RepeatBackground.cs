using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 target;
    private bool isSpawned = false;
    [SerializeField] private GameObject waterObject;
    [SerializeField] private float offset;
    void Start()
    {
        
        
    }

    void Update()
    {
        if(transform.position.x <= 0 && !isSpawned)
        {
            target = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
            Instantiate(waterObject, target, transform.rotation);
            isSpawned = true;
        }
        if(transform.position.x < -400)
        {
            Destroy(gameObject);
        }
    }
}
