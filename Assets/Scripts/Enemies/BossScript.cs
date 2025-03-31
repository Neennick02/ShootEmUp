using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float upDownTime = 5f;

    [SerializeField] Vector3 target;
    private Vector3 startPos;
    private bool stopped = false;

    Rigidbody rb;
    float timer = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void Update()
    {
        CheckPos();
        Move();
    }

    void CheckPos()
    {
        if(transform.position.x < target.x)
        {
            stopped = true;
        }
    }

    private void Move()
    {
        if (!stopped)
        {//beweeg naar links
            float moveX = -xSpeed * Time.deltaTime;
            transform.Translate(moveX, 0, 0);
        }
        //zorgt dat object op en neer beweegt
        timer += Time.deltaTime;
        float moveY = Mathf.Sin(timer / upDownTime * Mathf.PI * 2) * yRange;
        transform.position = new Vector3(transform.position.x, startPos.y + moveY, transform.position.z);
    }
}
