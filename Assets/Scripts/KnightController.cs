using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 700;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Misc")]
    public GameObject gameOverScreen;

    private bool isDead = false;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isGrounded = GroundCheck();

        if (!isDead)
        {
            if (isGrounded)
            {
                anim.SetBool("isJumping", false);

                if (Input.GetAxis("Jump") > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0f, jumpForce));
                    isGrounded = false;

                    anim.SetFloat("ySpeed", rb.velocity.y);
                    anim.SetBool("isJumping", true);
                }
            }
            else
            {
                anim.SetFloat("ySpeed", rb.velocity.y);
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            // rb.velocity = new Vector2(0f, 0f);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            isDead = true;
            anim.SetTrigger("die");
            gameOverScreen.SetActive(true);
            rb.simulated = false;
            return;
        }

        if (collision.CompareTag("TextZone"))
        {
            collision.GetComponent<TextZoneBehaviour>().ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TextZone"))
        {
            collision.GetComponent<TextZoneBehaviour>().HideText();
        }
    }
}
