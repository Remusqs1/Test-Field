using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Serialize Fields
    [SerializeField] public LayerMask groundMask;
    #endregion

    #region Private variables
    Rigidbody2D playerRigidbody;
    Animator animator;
    CapsuleCollider2D playerBoxCollider;
    bool facingRight = true;
    float originalSpeed;
    float currentSpeed = 3f; // the Current speed of the object
    bool isDoubleJumping = false;
    #endregion

    #region public variables
    public float jumpForce = 6.5f;
    public float acceleration = 0f; // how much you want object to accelerate 
    public float maxSpeed = 7f; // maximum speed the object can reach
    public static PlayerController sharedInstance;
    public bool isTeleported = false;
    #endregion

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this; 
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerBoxCollider = GetComponent<CapsuleCollider2D>();
        originalSpeed = currentSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isTouchingTheGround() || isDoubleJumping)
            {
                Jump();
                isDoubleJumping = !isDoubleJumping;
            }
        }
        if (Input.GetButton("Horizontal"))
        {
            Move();
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
        else
        {
            currentSpeed -= (acceleration * acceleration) * Time.deltaTime;
            if (currentSpeed < originalSpeed)
            {
                currentSpeed = originalSpeed;
            }
        }
        animator.SetBool("isJumping", !isTouchingTheGround());
    }

    private void FixedUpdate()
    {
        if (playerRigidbody.velocity.x == 0) animator.SetBool("isRunning", false);
    }

    void Jump()
    {
        animator.SetBool("isJumping", true);
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Move()
    {
        /*
        Vector3 horizontalMomentum = Input.GetAxisRaw("Horizontal") * Vector3.right * currentSpeed;
        playerRigidbody.velocity = new Vector2(horizontalMomentum.x, playerRigidbody.velocity.y);
        */

        playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentSpeed, playerRigidbody.velocity.y);


        if (playerRigidbody.velocity.x != 0) animator.SetBool("isRunning", true);
        else
        {
            animator.SetBool("isRunning", false);

        }
        if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight || Input.GetAxisRaw("Horizontal") < 0 && facingRight) Flip();
    }

    bool isTouchingTheGround()
    {
        var extraHeight = 0.1f;
        RaycastHit2D raycast = Physics2D.Raycast(playerBoxCollider.bounds.center, Vector2.down, playerBoxCollider.bounds.extents.y + extraHeight, groundMask);
        Color raycastColor = raycast.collider != null ? Color.green : Color.red;
        Debug.DrawRay(playerBoxCollider.bounds.center, Vector2.down * (playerBoxCollider.bounds.extents.y + extraHeight), raycastColor);
        return raycast.collider != null;
    }

    void Flip()
    {
        Vector3 currectScale = gameObject.transform.localScale;
        currectScale.x *= -1;
        gameObject.transform.localScale = currectScale;
        facingRight = !facingRight;
    }

}