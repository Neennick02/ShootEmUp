using UnityEngine;

public class BobbingUpDown : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float bobbingAmplitude = 0.5f;  //how much the boat goes up
    [SerializeField] private float bobbingSpeed = 1f;        //how fast the boat moves up
    [Header("Tilting Settings")]
    [SerializeField] private float tiltAngle = 15f;          //how much the boat rotates

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        float bobOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
        float tiltOffset = Mathf.Sin(Time.time * bobbingSpeed) * tiltAngle;

        // Apply vertical movement
        transform.position = new Vector3(startPosition.x, startPosition.y + bobOffset, startPosition.z);

        // Apply tilting (rotation around X-axis)
        transform.rotation = startRotation * Quaternion.Euler(tiltOffset, 0f, 0f);
    }
}
