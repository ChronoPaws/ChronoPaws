using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChupeta : MonoBehaviour, IRecolectable
{
    private int energia = 10;
    //public AudioClip sonido;
    public void Eliminar()
    {
        Eventos.AumentarContador_chupetas();
        Destroy(gameObject);
    }

    public void IncrementarEnergia()
    {
        //AudioSource.PlayClipAtPoint(sonido,transform.position,1);
        print($"Incrementar energia en {energia}");
    }
}
