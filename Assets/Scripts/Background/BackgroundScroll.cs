using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float scollSpeed = 1f;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce (new Vector3 (scollSpeed, 0, 0));
    }
}
