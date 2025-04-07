using UnityEngine;

public class SpinObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private bool x = false, y = false, z = false;

    void Update()
    {
        if (y)
        {
            // draait rondom y as
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (x)
        {
            // draait rondom y as
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        if (z)
        {
            // draait rondom y as
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}
