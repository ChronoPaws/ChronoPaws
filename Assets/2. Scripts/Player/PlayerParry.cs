using UnityEngine;
using System.Collections;

public class PlayerParry : MonoBehaviour
{
    public float parryWindow = 0.3f; // Tiempo donde el parry está activo
    private bool isParrying = false;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Este método lo llamará PlayerController
    public void DoParry()
    {
        if (!isParrying)
        {
            StartCoroutine(PerformParry());
            Debug.Log("Parry activado");
        }
    }

    private IEnumerator PerformParry()
    {
        isParrying = true;
        anim.SetTrigger("Parry"); // Animación
        yield return new WaitForSeconds(parryWindow);
        isParrying = false;
    }

    public bool IsParrying()
    {
        return isParrying;
    }
}
