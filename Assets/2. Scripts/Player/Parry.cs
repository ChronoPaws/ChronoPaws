using UnityEngine;
using System.Collections;
public class Parry : MonoBehaviour
{
    public float parryWindow = 0.3f; // Tiempo donde el parry est� activo
    private bool isParrying = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(PerformParry());
        }
    }

    IEnumerator PerformParry()
    {
        isParrying = true;
        anim.SetTrigger("Parry"); // Animaci�n futura
        yield return new WaitForSeconds(parryWindow);
        isParrying = false;
    }

    public bool IsParrying()
    {
        return isParrying;
    }
}
