using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float destroyDelay = 3f;
    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
