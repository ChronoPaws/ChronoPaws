using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    [Header("Attack Settings")]
    public AudioClip swordSwingClip;
    public float attackDuration = 0.5f; // <- Aquí defines la duración del ataque (debe coincidir con el clip)
    public float attackCooldown = 0.3f;

    private bool isAttacking = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DoAttack()
    {
        if (isAttacking) return;

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetBool("Attack", true);

        if (swordSwingClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(swordSwingClip);
        }

        Debug.Log("Realizando ataque");

        yield return new WaitForSeconds(attackDuration);

        anim.SetBool("Attack", false);
        Debug.Log("Animación de ataque terminada");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        Debug.Log("Listo para atacar nuevamente");
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
