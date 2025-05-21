using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigo : MonoBehaviour, IMatable
{

    public float vida;
    public GameObject dañobala;
    
    private float dañototal;
    private float dañoantes= 0;


    public void hacerDaño()
    {
        

        dañototal = dañobala.GetComponent<Bullet>().daño + dañoantes;
        print($"estas haciendo daño de {dañototal}");
        if (vida <= dañototal)
        {
            Destroy(gameObject);
        }
        else
        {
            dañoantes = dañototal;
        }


    }
    private void Start()
    {
        print($"estas haciendo daño de {dañobala.GetComponent<Bullet>().daño}");
    }
}

