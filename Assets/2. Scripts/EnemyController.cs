using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float attackRadius = 1.0f; // Nuevo: distancia para atacar
    public float speed = 2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private bool facingRight = true;
    private bool isAttacking = false; // Para que no ataque varias veces a la vez

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRadius && !isAttacking)
        {
            Attack();
            movement = Vector2.zero; // Se queda quieto al atacar
        }
        else if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        anim.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        // Puedes volver a permitir atacar después de un tiempo
        Invoke(nameof(ResetAttack), 1.0f); // 1 segundo de "cooldown"
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
