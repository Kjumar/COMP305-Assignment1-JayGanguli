using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitClip;

    private Rigidbody2D rb;
    private Animator anim;
    private CinemachineImpulseSource impulse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        impulse = GetComponent<CinemachineImpulseSource>();
        impulse.enabled = false;

        audioSource = GameObject.Find("EnemySFXSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            anim.SetTrigger("hit");
            rb.simulated = false;

            impulse.enabled = true;
            impulse.GenerateImpulse();

            audioSource.PlayOneShot(hitClip);

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
