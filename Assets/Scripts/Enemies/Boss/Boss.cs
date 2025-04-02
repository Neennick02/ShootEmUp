using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float upDownTime = 5f;

    [SerializeField] Vector3 stopTarget;
    [SerializeField] float lerpTime = 3f;
    float currentLerpTime;


    private Vector3 startPos;
    public bool stopped = false;

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
        if (transform.position.x < stopTarget.x)
        {
            stopped = true;
        }
    }

    private void Move()
    {
        //beweeg naar links
        if (stopped)
        {
            // Increase Lerp time
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Clamp01(t); // Ensure t stays between 0 and 1

            // Lerp the speed from xSpeed to 0
            float currentSpeed = Mathf.Lerp(xSpeed, 0, t);

            // Move the boss using the lerped speed
            float moveX = -currentSpeed * Time.deltaTime;
            transform.Translate(moveX, 0, 0);
        }
        else
        {
            float moveX = -xSpeed * Time.deltaTime;
            transform.Translate(moveX, 0, 0);
        }


        //zorgt dat object op en neer beweegt
        timer += Time.deltaTime;
        float moveY = Mathf.Sin(timer / upDownTime * Mathf.PI * 2) * yRange;
        transform.position = new Vector3(transform.position.x, startPos.y + moveY, transform.position.z);
    }
}
