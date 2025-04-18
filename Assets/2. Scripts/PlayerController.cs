using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed, JumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, WhatIsGround);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        FlipCharacter();
    }
    private void FixedUpdate()
    {
        Movement();
        Jump();
    }
    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHeight);
        }
    }
    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(velX * speed, velY);

        if (rb.linearVelocity.x != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    public void FlipCharacter()
    {
        if (rb.linearVelocity.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
