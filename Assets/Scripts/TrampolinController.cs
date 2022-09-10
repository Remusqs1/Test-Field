using UnityEngine;

public class TrampolinController : MonoBehaviour
{
    Animator animator;
    public float impulseForce = 10;
    [SerializeField] bool inverse = false;
    private int directionForce;
    [SerializeField] MovingDirection movingDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        directionForce = inverse ? 1 : -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO Bug - Si se aproximas al trampolin desde un costado el impulso es mayor al que debería
        if (collision.tag == "Player")
        {
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (movingDirection == MovingDirection.Vertical)
            {
                Vector2 force = Vector2.up * impulseForce * -directionForce;
                playerRigidBody.AddForce(force, ForceMode2D.Impulse);
                playerRigidBody.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 force = Vector2.right * impulseForce * directionForce;
                playerRigidBody.AddForce(force, ForceMode2D.Impulse);
                
                
               //TODO Bug - Trampolin no rebota si el jugador sigue presionando tecla de movimiento en dirección al trampolin
                Debug.Log("end speed x: " + playerRigidBody.velocity.x);
                Debug.Log(force);
                Debug.Log("initial speed x: " + playerRigidBody.velocity.x);

            }
            animator.SetBool("isActive", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("StopAnimation", 1f);
        }
    }

    void StopAnimation()
    {
        animator.SetBool("isActive", false);
    }
}
