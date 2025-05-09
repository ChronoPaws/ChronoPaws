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

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        healthImage.fillAmount = health / maxHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune && !isDead)
        {
            health -= collision.GetComponent <Enemy>().damageToGive;
            anim.SetTrigger("Damage");
            StartCoroutine(Inmunity());

            if(collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-KnockbackForceX, KnockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(KnockbackForceX, KnockbackForceY), ForceMode2D.Force);
            }
            if (health <= 0)
            {
                isDead = true;
                anim.SetTrigger("Die");
                // Podés llamar Game Over o bloquear más lógica
                Debug.Log("Comiste piso");
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
}
