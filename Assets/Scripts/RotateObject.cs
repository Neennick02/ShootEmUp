using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject target;
    [SerializeField] private bool rotateAroundCenter = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (rotateAroundCenter)
        {
            transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(target.transform.position, Vector3.right, 20 * Time.deltaTime);
        }
    }
}
