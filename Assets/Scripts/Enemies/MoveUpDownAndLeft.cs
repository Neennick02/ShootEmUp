using UnityEngine;

public class MoveUpDownAndLeft : MonoBehaviour
{
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float upDownTime = 5f;
    private GameManager gameManager;
    
    float timer = 0f;

    private Rigidbody rb;
    private Vector3 startPos;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void Update()
    {
        MoveObject();
        CheckPostion();
    }

    void CheckPostion()
    {
        if(transform.position.x < -40)
        {
            gameManager.score -= 20;
            gameManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
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
