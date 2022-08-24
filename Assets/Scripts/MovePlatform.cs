using UnityEngine;

enum MovingDirection
{
    Horizontal,
    Vertical
}

public class MovePlatform : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] MovingDirection movingDirection;

    private bool looped = false;
    private bool goingForward = true;

    public float deltaDistance;
    public float speed;
    public bool automatic = false;

    private void Awake()
    {
        startPosition = gameObject.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (automatic) looped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(looped) Loop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var collider = GameObject.Find("Player").GetComponent<PlayerController>();
            collider.transform.SetParent(transform);

            Loop();
            looped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var collider = GameObject.Find("Player").GetComponent<PlayerController>();
            collider.transform.SetParent(null);
        }

    }

    void Loop()
    {
        Vector3 targetPosition = new Vector3();

        if(movingDirection == MovingDirection.Vertical) targetPosition = new Vector3(startPosition.x, goingForward ? startPosition.y + deltaDistance : startPosition.y, startPosition.z);
        else targetPosition = new Vector3(goingForward ? startPosition.x + deltaDistance : startPosition.x, startPosition.y, startPosition.z);

        if (gameObject.transform.position == targetPosition)
        {
            goingForward = !goingForward;
        }

        gameObject.transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

}
