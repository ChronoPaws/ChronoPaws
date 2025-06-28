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

        if (collision.CompareTag("Player"))
        {
            PlayerParry parry = collision.GetComponent<PlayerParry>();
            EnemyController controller = GetComponent<EnemyController>();

            if (parry != null && parry.IsParrying() && controller != null && controller.isStriking)
            {
                StartCoroutine(StunnedByParry());
                return;
            }
        }
    }

    IEnumerator StunnedByParry()
    {
        anim.SetTrigger("ParryStunned");
        GetComponent<EnemyController>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<EnemyController>().enabled = true;
    }

    private IEnumerator DamageRoutine(Transform attacker)
    {
        isTakingDamage = true;
        enemy.healtPoints -= 2f;
        anim.SetTrigger("Hit");

        Vector2 knockDirection = (transform.position - attacker.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDirection * enemy.knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f);

        if (enemy.healtPoints <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        isTakingDamage = false;
    }
}

