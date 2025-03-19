using UnityEngine;

public class MoveUpDownAndLeft : MonoBehaviour
{
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float upDownTime = 5f;
    
    float timer = 0f;

    private Rigidbody rb;
    private Vector3 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void Update()
    {
        MoveObject();
    }

    void MoveObject() // beweegt object van rechts naar links en up en neer
    {
        //beweeg naar links
        float moveX = -xSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        //zorgt dat object op en neer beweegt
        timer += Time.deltaTime;
        float moveY = Mathf.Sin(timer / upDownTime * Mathf.PI * 2) * yRange;
        transform.position = new Vector3(transform.position.x, startPos.y + moveY, transform.position.z);
    }
}
