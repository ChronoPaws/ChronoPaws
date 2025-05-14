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

    private bool RecibiendoDamage;
    private bool isDead = false;

    Animator anim;
    Parry parry;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        parry = GetComponent<Parry>();
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
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Parry activado");
        }
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

    public void Movement()
    {
        velX = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) velX = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) velX = 1;

        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);
        anim.SetBool("Run", velX != 0);
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHeight);
        }
    }

    public void FlipCharacter()
    {
        if (velX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetDead()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Die");
    }
}

