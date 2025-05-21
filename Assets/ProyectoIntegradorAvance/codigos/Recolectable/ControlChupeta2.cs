using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChupeta2 : MonoBehaviour, IRecolectable
{
    private int energia = 15;
    public AudioClip sonido;
    public void Eliminar()
    {
        Eventos.AumentarContador_chupetas();
        Destroy(gameObject);
    }

    public void IncrementarEnergia()
    {
        AudioSource.PlayClipAtPoint(sonido,transform.position);
       print($"Incrementar energia en {energia}");
    }
}
