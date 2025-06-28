using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    private bool canDash = true;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private Animator anim;
    private TrailRenderer tr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    public void TryDash()
    {
        if (canDash && !isDashing)
            StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        anim.SetBool("Dash", true);
        // reproducir sonido dash

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0f);

        if (tr != null)
            tr.emitting = true;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        if (tr != null)
            tr.emitting = false;

        anim.SetBool("Dash", false);

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
