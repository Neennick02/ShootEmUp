using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private float xSpeed = 5f;
    private float slowSpeed;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float upDownTime = 5f;

    [SerializeField] Vector3 stopTarget;
    [SerializeField] float lerpTime = 3f;
    float currentLerpTime;
    

    private Vector3 startPos;
    private bool stopped = false;

    Rigidbody rb;
    float timer = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        slowSpeed = xSpeed / 2;
    }

    private void Update()
    {
        CheckPos();
        Move();
    }

    void CheckPos()
    {
        if(transform.position.x < stopTarget.x)
        {
            stopped = true;
        }
    }

    private void Move()
    {
        //beweeg naar links
            if (stopped)
            {
            Debug.Log("dsa0");
            //code die er voor zorgt dat speed lerpt naar 0
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
