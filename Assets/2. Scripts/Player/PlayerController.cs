﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed, JumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;

    public AudioClip swordSwingClip;
    public AudioClip dashSoundClip;

    private AudioSource audioSource;

    private bool RecibiendoDamage;
    private bool isDead = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    Animator anim;
    Parry parry;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        parry = GetComponent<Parry>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, WhatIsGround);

        anim.SetBool("Jump", !isGrounded);
        anim.SetBool("RecibiendoDamage", RecibiendoDamage);

        FlipCharacter();
        HandleInputs();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Movement();
        Jump();
    }

    void HandleInputs()
    {
        if (isDead || isDashing) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("Attack", true);
            PlaySwordSwingSound();
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Parry activado");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash activado");
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Ataque especial");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Curarse");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacción con el entorno");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Mostrar previsualización de mapa");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Mapa abierto");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Inventario abierto");
        }
    }

    public void Movement()
    {
        if (isDashing) return;

        velX = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) velX = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) velX = 1;

        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);

        anim.SetBool("Run", velX != 0);
    }

    public void Jump()
    {
        if (isDashing) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHeight);
            Debug.Log("Saltando");
        }
    }

    public void FlipCharacter()
    {
        if (velX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (velX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void RecibeDamage(Vector2 direccion, int cantDamage)
    {
        if (!RecibiendoDamage)
        {
            RecibiendoDamage = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote, ForceMode2D.Impulse);
        }
    }

    public void DesactiveDamage()
    {
        RecibiendoDamage = false;
    }

    public void SetDead()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Die");
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        anim.SetBool("Dash", true);
        PlayDashSound();

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        if (tr != null)
            tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        if (tr != null)
            tr.emitting = false;

        rb.gravityScale = originalGravity;
        isDashing = false;

        anim.SetBool("Dash", false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void PlaySwordSwingSound()
    {
        if (swordSwingClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(swordSwingClip);
        }
    }

    public void PlayDashSound()
    {
        if (dashSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(dashSoundClip);
        }
    }
}
