using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CinemachineImpulseSource impulse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        impulse = GetComponent<CinemachineImpulseSource>();
        impulse.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            anim.SetTrigger("hit");
            rb.simulated = false;

            impulse.enabled = true;
            impulse.GenerateImpulse();

            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("attack");
        }
    }
}
