using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public Image healthImage;
    private bool isInmune = false;
    private bool isDead = false;
    public float KnockbackForceX;
    public float KnockbackForceY;

    private Rigidbody2D rb;
    private Animator anim;

    public PlayerSoundController PlayerSoundController;

    public GameOverManager gameOverManager;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (gameOverManager == null)
        {
            gameOverManager = FindFirstObjectByType<GameOverManager>();
        }
    }

    void Update()
    {
        if (healthImage != null)
            healthImage.fillAmount = health / maxHealth;

        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(float damage, Transform attacker)
    {
        if (!isInmune && !isDead)
        {
            health -= damage;
            anim.SetTrigger("Damage");

            StartCoroutine(Inmunity());

            Vector2 knockDir = (transform.position - attacker.position).normalized;
            rb.AddForce(new Vector2(knockDir.x * KnockbackForceX, KnockbackForceY), ForceMode2D.Force);

            if (health <= 0)
            {
                Die();  // llamamos a nuestro nuevo método
                StartCoroutine(DelayedGameOver());
            }
        }
    }

    public void InstantKill(Transform attacker)
    {
        if (isDead) return;
        health = 0;
        Die();
        StartCoroutine(DelayedGameOver());
    }

    public void Heal(float amount)
    {
        if (!isDead && health < maxHealth)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }
    }

    public float defaultHealAmount = 10f;

    public void DoHealth()
    {
        if (!isDead && health < maxHealth)
        {
            health += defaultHealAmount;
            health = Mathf.Clamp(health, 0, maxHealth);

            PlayerSoundController?.playCuracion();  // aquí corregido

            Debug.Log($"Jugador se curó {defaultHealAmount}, vida actual: {health}");
        }
    }



    // ✅ Nuevo método que centraliza la lógica de morir
    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Detenemos movimiento
        rb.linearVelocity = Vector2.zero;

        // Lanzamos animación de muerte
        anim.SetTrigger("Die");

        // Reproducir sonido de muerte
        PlayerSoundController?.playMuerte();

        Debug.Log("Personaje ha muerto");
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        yield return new WaitForSeconds(0.5f);
        isInmune = false;
    }

    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(1f);

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
