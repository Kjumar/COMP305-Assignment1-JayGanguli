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
    [SerializeField] private GameObject attackObject;

    private bool isDead = false;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isAttacking = false;
    private float attackTimer = 0f;

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
            if (!isAttacking)
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
                    anim.SetBool("isJumping", true);
                }

                if (Input.GetAxis("Fire1") > 0)
                {
                    anim.SetTrigger("attack");
                    isAttacking = true;
                    attackTimer = 0.2f; // this is just short of the amount of time the attack animation takes
                    attackObject.SetActive(true);
                }
                else
                {
                    anim.ResetTrigger("attack");
                    attackObject.SetActive(false);
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    isAttacking = false;
                }
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            anim.SetTrigger("die");
            gameOverScreen.SetActive(true);
            rb.simulated = false;
        }
    }
}
