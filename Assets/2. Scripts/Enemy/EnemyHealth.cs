using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour

{
    private Enemy enemy;
    private Animator anim;
    private Rigidbody2D rb;

    private bool isDead = false;
    private bool isTakingDamage = false;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isTakingDamage && !isDead)
        {
            StartCoroutine(DamageRoutine(collision.transform));
        }
    }

    private IEnumerator DamageRoutine(Transform attacker)
    {
        isTakingDamage = true;

        // Aplicar da�o
        enemy.healtPoints -= 2f;

        // Reproducir animaci�n de impacto
        anim.SetTrigger("Hit");

        // Calcular direcci�n de knockback
        Vector2 knockDirection = (transform.position - attacker.position).normalized;

        // Aplicar knockback seg�n la fuerza definida en Enemy.cs
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDirection * enemy.knockbackForce, ForceMode2D.Impulse);

        // Esperar un momento antes de permitir m�s da�o
        yield return new WaitForSeconds(0.3f);

        // Si muere, reproducir animaci�n de muerte y destruir
        if (enemy.healtPoints <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(1f); // Duraci�n de la animaci�n de muerte
            Destroy(gameObject);
        }

        isTakingDamage = false;
    }
}
