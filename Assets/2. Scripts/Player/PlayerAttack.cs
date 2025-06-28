using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    [Header("Attack Settings")]
    public AudioClip swordSwingClip;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DoAttack()
    {
        // Activa la animación de ataque
        anim.SetTrigger("Attack");

        // Reproduce sonido de espada
        if (swordSwingClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(swordSwingClip);
        }

        Debug.Log("Realizando ataque");
    }
}
