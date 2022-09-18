using System.Collections;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    private Animator playerAnimator;
    public float animationTime;

    // Start is called before the first frame update
    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        playerAnimator.SetBool("isHitted", true);
        yield return new WaitForSeconds(animationTime);
        playerAnimator.SetBool("isHitted", false);
    }

}