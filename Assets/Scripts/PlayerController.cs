using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Serialize Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    #region Private variables
    private float horizontal;
    private bool isFacingRight = true;
    Animator animator;
    #endregion

    #region Public variables
    public float speed = 6f;
    public float jumpingPower = 7.5f;
    public bool isTeleported = false;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //TODO Bug, if change of horizontal button is too fast, player stops and don't start to move till button down again
        if (Input.GetButtonDown("Horizontal"))
        {
            Move();
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            Stop();
        }

        Flip();
        animator.SetBool("isJumping", !IsGrounded());
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x == 0) animator.SetBool("isRunning", false);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Jump()
    {
        animator.SetBool("isJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (rb.velocity.x != 0) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }
    void Stop()
    {
        if (rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(horizontal * 0.05f, rb.velocity.y);
        }
        animator.SetBool("isRunning", false);
    }

}