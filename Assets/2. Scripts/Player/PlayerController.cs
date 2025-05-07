using System.Collections;
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

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, WhatIsGround);

        anim.SetBool("Jump", !isGrounded);
        anim.SetBool("RecibiendoDamage", RecibiendoDamage);

        FlipCharacter();
        HandleInputs();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void HandleInputs()
    {
        if (isDashing) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Ataque especial");
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
        if (isDashing) return;

        velX = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            velX = 1;
        }

        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);

        anim.SetBool("Run", velX != 0);
    }

    public void Jump()
    {
        if (isDashing) return;

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHeight);
            Debug.Log("Saltando");
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        anim.SetBool("Dash", true); // ✅ Activar animación

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        tr.emitting= true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;

        rb.gravityScale = originalGravity;
        isDashing = false;

        anim.SetBool("Dash", false); // ✅ Desactivar animación

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
