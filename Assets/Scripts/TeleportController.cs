using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] GameObject targetGameObject;
    PlayerController playerController;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!playerController.isTeleported) Teleport();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("SetBool", 1f);
        }
    }

    private void Teleport()
    {
        playerController.isTeleported = true;
        playerController.transform.position = new Vector2(targetGameObject.GetComponent<Renderer>().bounds.center.x, targetGameObject.transform.position.y + offsetY);
    }

    private void SetBool()
    {
        playerController.isTeleported = false;
    }
}
