using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;//---
    //public Image healthImage;
    private bool isInmune = false;
    private bool isDead = false;
    public float KnockbackForceX;
    public float KnockbackForceY;

    private Rigidbody2D rb;
    private Animator anim;

    //public PlayerSoundController PlayerSoundController;//---

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
        //healthImage.fillAmount = health / maxHealth;
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
                isDead = true;
                anim.SetTrigger("Die");
                GetComponent<PlayerController>().SetDead();
                StartCoroutine(DelayedGameOver()); //  Ejecutamos una coroutine
            }

        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        yield return new WaitForSeconds(0.5f);
        isInmune = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(1f); //  Delay de 2 segundos

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    public void InstantKill(Transform attacker)
    {
        if (isDead) return;
        health = 0;
        anim.SetTrigger("Die");
        isDead = true;
        GetComponent<PlayerController>().SetDead();
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
}



