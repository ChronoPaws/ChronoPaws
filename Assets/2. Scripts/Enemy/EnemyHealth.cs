using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyController))]
public class EnemyHealth : MonoBehaviour
{
    // Componentes
    private Enemy enemy;
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;
    private EnemyController controller;

    // Estados
    private bool isDead = false;
    private bool isTakingDamage = false;

    public float GetHealth() => enemy.healthPoints;
    public bool IsDead() => isDead;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        controller = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Weapon") && !isTakingDamage)
        {
            StartCoroutine(DamageRoutine(collision.transform));
        }

        if (collision.CompareTag("Player"))
        {
            var parry = collision.GetComponent<PlayerParry>();
            if (parry != null && parry.IsParrying() && controller != null && controller.isStriking)
            {
                StartCoroutine(StunnedByParry());
            }
        }
    }

    IEnumerator StunnedByParry()
    {
        anim.SetTrigger("ParryStunned");
        controller.enabled = false;
        yield return new WaitForSeconds(2f);
        controller.enabled = true;
    }

    IEnumerator DamageRoutine(Transform attacker)
    {
        isTakingDamage = true;
        enemy.healthPoints -= 2f;
        anim.SetTrigger("Hit");

        Vector2 knockDirection = (transform.position - attacker.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDirection * enemy.knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f);

        if (enemy.healthPoints <= 0)
        {
            Die();
        }

        isTakingDamage = false;
    }

    private void Die()
    {
        isDead = true;

        anim.SetBool("Revive", false); // 🔁 Importante: asegúrate de dejarlo en false al morir
        anim.SetTrigger("Die");

        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        rb.simulated = false;

        col.enabled = false;
        controller.enabled = false;
    }

    public void SetHealth(float value)
    {
        enemy.healthPoints = value;

        // Si tiene vida pero aún está marcado como muerto, hay que revivirlo
        if (value > 0f && isDead)
        {
            Revive();
        }
    }

    public void SetDead(bool value)
    {
        if (value && !isDead)
        {
            Die(); // si está vivo pero ahora debe estar muerto
        }
        else if (!value && isDead)
        {
            Revive(); // si está muerto pero ahora debe estar vivo
        }
    }

    private void Revive()
    {
        isDead = false;

        anim.SetBool("Revive", true);
        rb.isKinematic = false;
        rb.simulated = true;

        // Empujón hacia abajo para que caiga si está flotando
        rb.linearVelocity = new Vector2(0, -1f);

        col.enabled = true;
        controller.enabled = true;
    }

}
