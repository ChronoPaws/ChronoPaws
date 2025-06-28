using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerMovement movement;
    private PlayerSoundController soundController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        soundController = GetComponent<PlayerSoundController>();
    }

    public void TryJump()
    {
        // Solo salta si está en el suelo
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            soundController?.playSaltar();
            Debug.Log("Saltando");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            movement.groundCheck.position,
            movement.groundCheckRadius,
            movement.groundLayer  // ✅ Corrección aquí: antes decía whatIsGround
        );
    }
}
