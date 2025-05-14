using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public Transform player;

    [Header("Movimiento")]
    public float speed = 2.0f;
    public float detectionRadius = 5.0f;
    public float stopDistance = 1.5f;

    [Header("Ataque")]
    public float attackCooldown = 2.0f;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isAttacking = false;
    public bool isStriking = false;

    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            if (distance > stopDistance)
            {
                // Movimiento hacia el jugador
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
                anim.SetFloat("Speed", Mathf.Abs(direction.x));

                if (direction.x > 0 && !facingRight) Flip();
                else if (direction.x < 0 && facingRight) Flip();
            }
            else
            {
                // Está cerca: detenerse y atacar si el cooldown lo permite
                rb.linearVelocity = Vector2.zero;
                anim.SetFloat("Speed", 0);

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    StartAttack();
                }
            }
        }
        else
        {
            // Fuera del radio de detección
            rb.linearVelocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");
    }

    public void StartStrike()
    {
        isStriking = true;
    }

    public void DealDamage()
    {
        if (player == null) return;

        float hitDistance = 1.2f;
        if (Vector2.Distance(transform.position, player.position) <= hitDistance)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            Parry parry = player.GetComponent<Parry>();

            if (parry != null && parry.IsParrying())
            {
                Debug.Log("Ataque parryeado");
                return;
            }

            if (ph != null)
            {
                ph.TakeDamage(GetComponent<Enemy>().damageToGive, transform);
            }
        }
    }

    public void EndStrike()
    {
        isStriking = false;
    }

    public void EndAttack()
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
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
